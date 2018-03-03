using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR_Management_System.Models;

namespace HR_Management_System.Areas.Adminpanel.Controllers
{
    public class LoginController : Controller
    {
		HRM_databaseEntities db = new HRM_databaseEntities();

		// GET: Adminpanel/Login
		public ActionResult Index()
		{
            Session.Remove("admin_email");
            Session.Remove("admin_password");
            return View();
		}

		[HttpPost]
		public ActionResult Index([Bind(Include = "admin_email, admin_password")] Admin admin)
		{
			Admin LoginedAdmin = db.Admins.SingleOrDefault(a => a.admin_email == admin.admin_email && a.admin_password == admin.admin_password);
			if (String.IsNullOrEmpty(admin.admin_email) || String.IsNullOrEmpty(admin.admin_password))
			{
				return Json(new { errorMessage = "<div class='alert alert-danger'><span class='fa fa-warning'></span> Email və ya şifrə daxil edilməyib!</div>", url = false }, JsonRequestBehavior.AllowGet);
			}
			else if (LoginedAdmin == null)
			{
				return Json(new { errorMessage = "<div class='alert alert-danger'><span class='fa fa-warning'></span>Məlumatlar düzgün daxil edilməyib!</div>", url = false }, JsonRequestBehavior.AllowGet);
			}
			Session["admin_email"] = LoginedAdmin.admin_email;
			Session["admin_password"] = LoginedAdmin.admin_password;
			return Json(new { errorMessage = false, url = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
		}
	}
}