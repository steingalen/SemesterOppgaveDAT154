
using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Content/js/site.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/css/style.css"));
        }
    }
}
