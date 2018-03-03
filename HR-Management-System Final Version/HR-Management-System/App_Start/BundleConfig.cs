using System.Web;
using System.Web.Optimization;

namespace HR_Management_System
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery-unobtrusive").Include("~/Scripts/jquery.unobtrusive-ajax.js"));
		}
	}
}
