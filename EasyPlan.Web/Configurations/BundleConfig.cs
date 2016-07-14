using System.Web.Optimization;

namespace EasyPlan.Web {
  public class BundleConfig {
    public static void RegisterBundles(BundleCollection bundles) {
	  bundles.Add(
		new ScriptBundle("~/Scripts/vendor")
            .Include("~/Scripts/jquery-{version}.js")
            .Include("~/Scripts/jquery-ui.js")
            .Include("~/Scripts/handlebars-{version}.js")
            .Include("~/Scripts/jquery.selectText.js")
            .Include("~/Scripts/jquery.popup.js")
            .Include("~/Scripts/knockout-{version}.js")
            .Include("~/Scripts/knockout.mapping.js")
            .IncludeDirectory("~/Scripts/knockoutBindings/", "*Binding.js")
            .IncludeDirectory("~/Scripts/knockoutExtenders/", "*Extender.js")
        );

      bundles.Add(
        new StyleBundle("~/Content/css")
        .IncludeDirectory("~/Content/", "*.css")
        );
    }
  }
}