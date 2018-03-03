using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using HR_Management_System.Models;
using HR_Management_System.ViewModels.Adminpanel;

namespace HR_Management_System.Areas.Adminpanel.Controllers
{
    public class SalaryController : Controller
    {
        HRM_databaseEntities db = new HRM_databaseEntities();
        // GET: Adminpanel/Salary
        public ActionResult Index()
        {
            return View(new SalaryViewModel { employee = db.Employees.ToList(), award = db.Awards.ToList(), leave_app = db.Leave_App.ToList()});
        }
    }
}