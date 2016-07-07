using System.Web.Optimization;

namespace mySite.Web {
  public class BundleConfig {
    public static void RegisterBundles(BundleCollection bundles) {
	  bundles.Add(
		new ScriptBundle("~/Scripts/vendor")
            .Include("~/Scripts/underscore.js")
            .Include("~/Scripts/q.js")
            .Include("~/Scripts/jquery-{version}.js")
            .Include("~/Scripts/knockout-{version}.js")
            .Include("~/Scripts/knockout.mapping.js")
            .IncludeDirectory("~/Scripts/knockoutBindings/", "*Binding.js")
        );

      bundles.Add(
        new StyleBundle("~/Content/css")
		  .Include("~/Content/durandal.css")
          .Include("~/Content/starterkit.css")
          .Include("~/Content/style.css")
        );
    }
  }
}