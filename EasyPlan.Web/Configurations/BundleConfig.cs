using System.Web.Optimization;

namespace EasyPlan.Web {
  public class BundleConfig {
    public static void RegisterBundles(BundleCollection bundles) {
	  bundles.Add(
		new ScriptBundle("~/Scripts/vendor")
            .Include("~/Scripts/handlebars-4.0.5.js")
            .Include("~/Scripts/underscore.js")
            .Include("~/Scripts/jquery-{version}.js")
            .Include("~/Scripts/q.js")
            .Include("~/Scripts/jquery-ui.js")
            .Include("~/Scripts/knockout-{version}.js")
            .Include("~/Scripts/knockout.mapping.js")
            .Include("~/Scripts/unmaskPasswordField.js")
        );

      bundles.Add(
        new StyleBundle("~/Content/css")
        .IncludeDirectory("~/Content/", "*.css")
        );
    }
  }
}