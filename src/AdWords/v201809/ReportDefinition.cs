// Copyright 2018 Google LLC
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

#pragma warning disable 1591

namespace Google.Api.Ads.AdWords.v201809
{
    using Google.Api.Ads.AdWords.Util.Reports;
    using System.ComponentModel;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/cm/v201809")]
    public partial class ReportDefinition : IReportDefinition
    {
        private long idField;

        private bool idFieldSpecified;

        private Selector selectorField;

        private string reportNameField;

        private ReportDefinitionReportType reportTypeField;

        private bool reportTypeFieldSpecified;

        private bool hasAttachmentField;

        private bool hasAttachmentFieldSpecified;

        private ReportDefinitionDateRangeType dateRangeTypeField;

        private bool dateRangeTypeFieldSpecified;

        private DownloadFormat downloadFormatField;

        private bool downloadFormatFieldSpecified;

        private string creationTimeField;

        public long id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.idSpecified = true;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool idSpecified
        {
            get
            {
                return this.idFieldSpecified;
            }
            set
            {
                this.idFieldSpecified = value;
            }
        }

        public Selector selector
        {
            get
            {
                return this.selectorField;
            }
            set
            {
                this.selectorField = value;
            }
        }

        public string reportName
        {
            get
            {
                return this.reportNameField;
            }
            set
            {
                this.reportNameField = value;
            }
        }

        public ReportDefinitionReportType reportType
        {
            get
            {
                return this.reportTypeField;
            }
            set
            {
                this.reportTypeField = value;
                this.reportTypeSpecified = true;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool reportTypeSpecified
        {
            get
            {
                return this.reportTypeFieldSpecified;
            }
            set
            {
                this.reportTypeFieldSpecified = value;
            }
        }

        public bool hasAttachment
        {
            get
            {
                return this.hasAttachmentField;
            }
            set
            {
                this.hasAttachmentField = value;
                this.hasAttachmentSpecified = true;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool hasAttachmentSpecified
        {
            get
            {
                return this.hasAttachmentFieldSpecified;
            }
            set
            {
                this.hasAttachmentFieldSpecified = value;
            }
        }

        public ReportDefinitionDateRangeType dateRangeType
        {
            get
            {
                return this.dateRangeTypeField;
            }
            set
            {
                this.dateRangeTypeField = value;
                this.dateRangeTypeSpecified = true;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool dateRangeTypeSpecified
        {
            get
            {
                return this.dateRangeTypeFieldSpecified;
            }
            set
            {
                this.dateRangeTypeFieldSpecified = value;
            }
        }

        public DownloadFormat downloadFormat
        {
            get
            {
                return this.downloadFormatField;
            }
            set
            {
                this.downloadFormatField = value;
                this.downloadFormatSpecified = true;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool downloadFormatSpecified
        {
            get
            {
                return this.downloadFormatFieldSpecified;
            }
            set
            {
                this.downloadFormatFieldSpecified = value;
            }
        }

        public string creationTime
        {
            get
            {
                return this.creationTimeField;
            }
            set
            {
                this.creationTimeField = value;
            }
        }

        /// <summary>
        /// Gets the DURING clause for AWQL query.
        /// </summary>
        /// <returns>The DURING clause for AWQL query.</returns>
        public string GetDuringClause()
        {
            if (!dateRangeTypeFieldSpecified)
            {
                return "";
            }
            if (dateRangeType == ReportDefinitionDateRangeType.CUSTOM_DATE)
            {
                return string.Format("DURING {0}, {1}", this.selector.dateRange.min,
                    this.selector.dateRange.max);
            }
            else
            {
                return string.Format("DURING {0}", dateRangeType);
            }
        }

        /// <summary>
        /// Gets the FROM clause for AWQL query.
        /// </summary>
        /// <returns>The FROM clause for AWQL query.</returns>
        private string GetFromClause()
        {
            return string.Format("FROM {0}", reportType);
        }

        /// <summary>
        /// Converts this object into an AWQL query.
        /// </summary>
        /// <returns>The AWQL query.</returns>
        internal string ToQuery()
        {
            if (!reportTypeFieldSpecified)
            {
                throw new System.ApplicationException("Report type is not specified.");
            }
            string[] parts = new string[] {
        selector.GetSelectClause(),
        GetFromClause(),
        selector.GetWhereClause(),
        GetDuringClause()
      }.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            return string.Join<string>(" ", parts);
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ReportDefinition.DateRangeType", Namespace = "https://adwords.google.com/api/adwords/cm/v201809")]
    public enum ReportDefinitionDateRangeType
    {
        TODAY,
        YESTERDAY,
        LAST_7_DAYS,
        LAST_WEEK,
        LAST_BUSINESS_WEEK,
        THIS_MONTH,
        LAST_MONTH,
        ALL_TIME,
        CUSTOM_DATE,
        LAST_14_DAYS,
        LAST_30_DAYS,
        THIS_WEEK_SUN_TODAY,
        THIS_WEEK_MON_TODAY,
        LAST_WEEK_SUN_SAT
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/cm/v201809")]
    public enum DownloadFormat
    {
        CSVFOREXCEL,
        CSV,
        TSV,
        XML,
        GZIPPED_CSV,
        GZIPPED_XML
    }
}
