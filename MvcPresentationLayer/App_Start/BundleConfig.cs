using System;
using System.Web.Optimization;

namespace MvcPresentationLayer.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            ScriptBundle thirdPartyScripts = new ScriptBundle("~/Scripts/ThirdParty");
            thirdPartyScripts.Include("~/Views/Home/Bundle/Edit.js");
            StyleBundle thirdPartyStyles = new StyleBundle("~/Styles/ThirdParty");
            thirdPartyStyles.Include("~/Views/Home/Bundle/Style.css");

            bundles.Add(thirdPartyScripts);
            bundles.Add(thirdPartyStyles);
            BundleTable.EnableOptimizations = true;
        }
    }
}
