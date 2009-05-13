using System;
using System.Collections.Generic;
using System.Text;
using com.google.api.adwords.lib;
using com.google.api.adwords.v13;

namespace com.google.api.adwords.samples.both {
  class Test : SampleBase {
    public override string Description {
      get {
        return "The method or operation is not implemented.";
      }
    }

    public override void Run(AdWordsUser user) {
      CampaignService serv = (CampaignService) user.GetService(ApiServices.v13.CampaignService);
      Campaign mycampaign =  serv.getCampaign(11779);
    }
  }
}
