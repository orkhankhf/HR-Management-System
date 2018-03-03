using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HR_Management_System
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Employees", action = "Login", id = UrlParameter.Optional },
				new[] { "HR_Management_System.Controllers" }
			);
			routes.MapRoute(
				"DefaultAdminpanel",
				"{controller}/{action}/{id}",
				new { area = "Adminpanel", controller = "Login", action = "Index", id = UrlParameter.Optional },
				null,
				new[] { "HR_Management_System.Areas.Adminpanel.Controllers" }
			).DataTokens.Add("area", "Adminpanel");
		}
    }
}
