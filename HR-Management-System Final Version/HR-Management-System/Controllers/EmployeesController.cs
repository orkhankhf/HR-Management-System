using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HR_Management_System.Models;
using HR_Management_System.ViewModels.Site;
using HR_Management_System.ViewModels.Adminpanel;

namespace HR_Management_System.Controllers
{
    public class EmployeesController : Controller
    {
        private HRM_databaseEntities db = new HRM_databaseEntities();

        // GET: Employees
        public ActionResult Login()
		{
			Session.Remove("employee_email");
			Session.Remove("employee_password");
			return View();
        }

		[HttpPost]
		public ActionResult Login([Bind(Include = "emp_email, emp_password")] Employee employee)
		{
			var LoginedEmployee = db.Employees.SingleOrDefault(e => e.emp_email == employee.emp_email && e.emp_password == employee.emp_password);
			if (String.IsNullOrEmpty(employee.emp_email) || String.IsNullOrEmpty(employee.emp_password))
			{
				return Json(new { errorMessage = "<div class='alert alert-danger'><span class='fa fa-warning'></span> Email və ya şifrə daxil edilməyib!</div>", url = false }, JsonRequestBehavior.AllowGet);
			}
			else if (LoginedEmployee == null)
			{
				return Json(new { errorMessage = "<div class='alert alert-danger'><span class='fa fa-warning'></span>Məlumatlar düzgün daxil edilməyib!</div>", url = false }, JsonRequestBehavior.AllowGet);
			}
			Session["employee_email"] = LoginedEmployee.emp_email;
			Session["employee_password"] = LoginedEmployee.emp_password;
			return Json(new { errorMessage = false, url = Url.Action("Account") }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Account()
		{
			if (!CheckEmployeeLogin.Check())
			{
				return RedirectToAction("Login");
			}
			string emp_email = Session["employee_email"].ToString();
			string emp_password = Session["employee_password"].ToString();
			Employee employee = db.Employees.Include(g=>g.Gender).Include(e=>e.Departament).Include(e=>e.Designation).Include(e=>e.Awards).Where(e => e.emp_email == emp_email && e.emp_password == emp_password).SingleOrDefault();
			var holiday = db.Holidays.ToList();
			CalcAtWork clc = new CalcAtWork(Convert.ToDateTime(employee.emp_date_of_joining));
			List<UserBirthdays> userBirthdays = db.Employees.Where(e => e.emp_dateof_birth.Value.Day > DateTime.Now.Day && e.emp_dateof_birth.Value.Month == DateTime.Now.Month || e.emp_dateof_birth.Value.Month == DateTime.Now.Month+1).Select(e => new UserBirthdays { Fullname = e.emp_fullname, ProfileImage = e.emp_photo, Birthdate = e.emp_dateof_birth.Value }).OrderByDescending(b=>b.Birthdate).ToList();
			var leave_type = db.Leave_type.ToList();
			var leave_status = db.Leave_status.ToList();
			return View(new EmployeeAccount { Employee = employee, Holiday = holiday , CalcAtWork = clc.GetWorkTime, UserBirthdays = userBirthdays, Leave_status = leave_status});
		}

		[HttpPost]
		public ActionResult ChangePassword(string new_password)
		{
			if (!CheckEmployeeLogin.Check())
			{
				return RedirectToAction("Login");
			}
			if (String.IsNullOrEmpty(new_password))
			{
				return Json(new { status = "error", msg = "Password cannot be empty!" }, JsonRequestBehavior.AllowGet);
			}
			string emp_email = Session["employee_email"].ToString();
			string emp_password = Session["employee_password"].ToString();
			var emp = db.Employees.SingleOrDefault(e => e.emp_email == emp_email && e.emp_password == emp_password);
			emp.emp_password = new_password;
			db.SaveChanges();
			return Json(new { status = "success", msg = "Password has changed. Please enter with new password." }, JsonRequestBehavior.AllowGet);
		}
		
		[HttpPost]
		public ActionResult CreateLeaveApp(DateTime leave_date, string leave_reason)
		{
			if (!CheckEmployeeLogin.Check())
			{
				return RedirectToAction("Login");
			}
			string emp_email = Session["employee_email"].ToString();
			string emp_password = Session["employee_password"].ToString();
			int emp_id = db.Employees.SingleOrDefault(e => e.emp_email == emp_email && e.emp_password == emp_password).id;
			int leave_status_id = db.Leave_status.SingleOrDefault(s => s.status_name == "Pending").id;
			Leave_App new_leaveApp = new Leave_App()
			{
				leave_emp_id = emp_id,
				leave_date = leave_date,
				leave_reason = leave_reason,
				leave_status_id = leave_status_id
			};
			string status = "";
			string msg = "";
			try
			{
				db.Leave_App.Add(new_leaveApp);
				db.SaveChanges();
				status = "success";
				msg = "Operation has successfully completed.";
			}
			catch
			{
				status = "error";
				msg = "Databazada Leave_status tablosunda mutleq Pending adli statusname olmalidir!";
			}
			return Json(new { status, msg }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult MyLeaves()
		{
			if (!CheckEmployeeLogin.Check())
			{
				return RedirectToAction("Login");
			}
			string emp_email = Session["employee_email"].ToString();
			string emp_password = Session["employee_password"].ToString();
			Employee employee = db.Employees.Include(g => g.Gender).Include(e => e.Departament).Include(e=>e.Leave_App).Include(e => e.Designation).Where(e => e.emp_email == emp_email && e.emp_password == emp_password).SingleOrDefault();
			CalcAtWork clc = new CalcAtWork(Convert.ToDateTime(employee.emp_date_of_joining));
			List<UserBirthdays> userBirthdays = db.Employees.Where(e => e.emp_dateof_birth.Value.Day > DateTime.Now.Day && e.emp_dateof_birth.Value.Month == DateTime.Now.Month || e.emp_dateof_birth.Value.Month == DateTime.Now.Month + 1).Select(e => new UserBirthdays { Fullname = e.emp_fullname, ProfileImage = e.emp_photo, Birthdate = e.emp_dateof_birth.Value }).OrderByDescending(b => b.Birthdate).ToList();
			var leave_status = db.Leave_status.ToList();
			return View(new EmployeeAccount { Employee = employee, CalcAtWork = clc.GetWorkTime, UserBirthdays = userBirthdays, Leave_status = leave_status });
		}
	}
}
