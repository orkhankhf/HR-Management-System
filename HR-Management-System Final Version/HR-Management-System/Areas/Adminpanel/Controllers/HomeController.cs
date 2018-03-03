using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR_Management_System.Models;

namespace HR_Management_System.Areas.Adminpanel.Controllers
{
    public class HomeController : Controller
    {
        // GET: Adminpanel/Home
		public ActionResult Index()
		{
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index","Login");
			}
			return View();
		}
	}
}