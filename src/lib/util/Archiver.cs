// Copyright 2009, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

using com.google.api.adwords.v13;

namespace com.google.api.adwords.lib.util {
  /// <summary>
  /// Handles the archiving and unarchiving of a ClientAccount object
  /// to an XML document.
  /// </summary>
  public class Archiver {
    /// <summary>
    /// Deserialize an account from an XML node.
    /// </summary>
    /// <param name="xAccount">The XML node that contains serialized data.</param>
    /// <returns>The deserialized ClientAccount object.</returns>
    public ClientAccount DeSerializeAccount(XmlElement xAccount) {
      ClientAccount account = new ClientAccount();
      try {
        XmlElement xAccountInfo = (XmlElement) xAccount.SelectSingleNode("accountInfo");
        if (xAccountInfo != null) {
          account.accountInfo = (AccountInfo) DeSerializeObject(xAccountInfo, typeof(AccountInfo));
        }
        if (xAccount.HasAttribute("email")) {
          account.email = xAccount.Attributes["email"].Value;
        }
        XmlNodeList xCampaignList = xAccount.SelectNodes("Campaigns/Campaign");
        foreach (XmlElement xCampiagn in xCampaignList) {
          CampaignEx campaignEx = new CampaignEx();
          campaignEx.campaign = (Campaign) DeSerializeObject(xCampiagn, typeof(Campaign));

          XmlNodeList xAdGroupList = xCampiagn.SelectNodes("AdGroups/AdGroup");

          foreach (XmlElement xAdGroup in xAdGroupList) {
            AdGroupEx adgroupEx = new AdGroupEx();
            adgroupEx.adgroup = (AdGroup) DeSerializeObject(xAdGroup, typeof(AdGroup));

            XmlNodeList xAdList = xAdGroup.SelectNodes("Ads/*");

            foreach (XmlElement xAd in xAdList) {
              string typeName = typeof(Ad).AssemblyQualifiedName.Replace(
                  "com.google.api.adwords.v13.Ad", "com.google.api.adwords.v13." + xAd.Name);
              Type adType = Type.GetType(typeName);
              adgroupEx.ads.Add((Ad) DeSerializeObject(xAd, adType));
            }

            XmlNodeList xAdCriterionList = xAdGroup.SelectNodes("Criteria/*");

            foreach (XmlElement xCriterion in xAdCriterionList) {
              string typeName = typeof(Criterion).AssemblyQualifiedName.Replace(
                  "com.google.api.adwords.v13.Criterion",
                  "com.google.api.adwords.v13." + xCriterion.Name);
              Type criterionType = Type.GetType(typeName);
              adgroupEx.criteria.Add((Criterion) DeSerializeObject(xCriterion, criterionType));
            }

            campaignEx.adGroups.Add(adgroupEx);
          }

          XmlNodeList xCampaignCriterionList = xCampiagn.SelectNodes("Criteria/*");

          foreach (XmlElement xCriterion in xCampaignCriterionList) {
            string typeName = typeof(Criterion).AssemblyQualifiedName.Replace(
                "com.google.api.adwords.v13.Criterion",
                "com.google.api.adwords.v13." + xCriterion.Name);
            Type criterionType = Type.GetType(typeName);
            campaignEx.criteria.Add((Criterion) DeSerializeObject(xCriterion, criterionType));
          }

          account.campaigns.Add(campaignEx);
        }
      } catch (Exception ex) {
        throw new ApplicationException("The format of XML was invalid.", ex);
      }
      return account;
    }

    /// <summary>
    /// A generic serialization function to serialize an Object as XML.
    /// </summary>
    /// <param name="accountNode">The XML node to which serialization
    /// happens.</param>
    /// <param name="client">The account details to be serialized.</param>
    public void SerializeAccount(XmlElement accountNode, ClientAccount client) {
      try {
        accountNode.SetAttribute("email", client.email);
        SerializeObject(accountNode, "accountInfo", client.accountInfo);
        XmlElement xCampaigns = accountNode.OwnerDocument.CreateElement("Campaigns");
        accountNode.AppendChild(xCampaigns);

        foreach (CampaignEx campaign in client.campaigns) {
          XmlElement xCampaign = SerializeObject(xCampaigns, "Campaign", campaign.campaign);
          XmlElement xAdGroups = accountNode.OwnerDocument.CreateElement("AdGroups");
          xCampaign.AppendChild(xAdGroups);

          foreach (AdGroupEx adgroup in campaign.adGroups) {
            XmlElement xAdGroup = SerializeObject(xAdGroups, "AdGroup", adgroup.adgroup);

            XmlElement xAdCriteria = accountNode.OwnerDocument.CreateElement("Criteria");
            xAdGroup.AppendChild(xAdCriteria);

            foreach (Criterion criterion in adgroup.criteria) {
              SerializeObject(xAdCriteria, criterion.GetType().Name, criterion);
            }

            XmlElement xAds = accountNode.OwnerDocument.CreateElement("Ads");
            xAdGroup.AppendChild(xAds);

            foreach (Ad ad in adgroup.ads) {
              SerializeObject(xAds, ad.GetType().Name, ad);
            }
          }

          XmlElement xCampaignCriteria = accountNode.OwnerDocument.CreateElement("Criteria");
          xCampaign.AppendChild(xCampaignCriteria);


          foreach (Criterion criterion in campaign.criteria) {
            SerializeObject(xCampaignCriteria, criterion.GetType().Name, criterion);
          }
        }
      } catch (Exception ex) {
        throw new ApplicationException("The account could not be downloaded.", ex);
      }
    }

    /// <summary>
    /// A generic serialization function, to serialize an Object as XML.
    /// </summary>
    /// <param name="parent">Parent of the node to which the serialization
    /// happens.</param>
    /// <param name="fieldName">The name to be given to the XML node when
    /// serializing <paramref name="o"/>.
    /// </param>
    /// <param name="obj">Object to be serialized.</param>
    /// <returns></returns>
    private XmlElement SerializeObject(XmlElement parent, string fieldName, object obj) {
      XmlDocument xDoc = parent.OwnerDocument;
      XmlElement xNode = xDoc.CreateElement(fieldName);

      if (obj == null) {
        xNode.SetAttribute("IsNull", "true");
      } else {
        if (obj is int || obj is double || obj is string || obj is long || obj is bool ||
            obj is Enum || obj is DateTime) {
          xNode.InnerText = obj.ToString();
        } else if (obj is Array) {
          if (obj is byte[]) {
            byte[] data = obj as byte[];
            xNode.SetAttribute("Base64", "true");
            xNode.InnerText = Convert.ToBase64String(data);
          } else {
            Array aryObject = obj as Array;
            foreach (object child in aryObject) {
              SerializeObject(xNode, child.GetType().Name, child);
            }
          }
        } else {
          FieldInfo[] fis = obj.GetType().GetFields();
          foreach (FieldInfo fi in fis) {
            SerializeObject(xNode, fi.Name, fi.GetValue(obj));
          }
        }
      }
      parent.AppendChild(xNode);
      return xNode;
    }

    /// <summary>
    /// A generic deserialization function to deserialize an Object from XML.
    /// </summary>
    /// <param name="node">Node from which deserialization happens.</param>
    /// <param name="type">Type of the deserialized node.</param>
    /// <returns>The deserialized object.</returns>
    private object DeSerializeObject(XmlElement node, Type type) {
      if (node == null || node.HasAttribute("IsNull")) {
        return null;
      }
      if (type == null) {
        return null;
      }
      if (type == typeof(int)) {
        return int.Parse(node.InnerText);
      } else if (type == typeof(double)) {
        return double.Parse(node.InnerText);
      } else if (type == typeof(string)) {
        return node.InnerText;
      } else if (type == typeof(long)) {
        return long.Parse(node.InnerText);
      } else if (type == typeof(bool)) {
        return bool.Parse(node.InnerText);
      } else if (type.IsEnum) {
        return Enum.Parse(type, node.InnerText);
      } else if (type == typeof(DateTime)) {
        return DateTime.Parse(node.InnerText);
      } else if (type.IsArray) {
        if (node.HasAttribute("Base64")) {
          return Convert.FromBase64String(node.InnerText);
        } else {
          string elementTypeName = type.AssemblyQualifiedName.Replace("[]", "");
          XmlNodeList arrayChlidNodes = node.SelectNodes("*");

          Type arrayElementType = Type.GetType(elementTypeName);
          Array array = Array.CreateInstance(arrayElementType, arrayChlidNodes.Count);

          for (int i = 0; i < arrayChlidNodes.Count; i++) {
            array.SetValue(DeSerializeObject((XmlElement) arrayChlidNodes[i], arrayElementType), i);
          }
          return array;
        }
      } else {
        object obj = Activator.CreateInstance(type);
        FieldInfo[] childFIs = obj.GetType().GetFields();
        foreach (FieldInfo childFI in childFIs) {
          XmlElement xChild = (XmlElement) node.SelectSingleNode(childFI.Name);
          if (xChild != null) {
            childFI.SetValue(obj, DeSerializeObject(xChild, childFI.FieldType));
          }
        }
        return obj;
      }
    }
  }
}
