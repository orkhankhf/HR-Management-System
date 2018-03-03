using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HR_Management_System.Models;

namespace HR_Management_System.Areas.Adminpanel.Controllers
{
    public class EmployeesController : Controller
    {
        private HRM_databaseEntities db = new HRM_databaseEntities();
		public string emp_photo_name, emp_resume_name, emp_offer_letter_name, emp_joining_letter_name, emp_contact_and_argue_name, emp_ID_proof_name;

		// GET: Adminpanel/Employees
		public ActionResult Index()
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			var employees = db.Employees.Include(e => e.Departament).Include(e => e.Designation).Include(e => e.Gender).OrderByDescending(e=>e.id);
            return View(employees.ToList());
        }

        // GET: Adminpanel/Employees/Details/5
        public ActionResult Details(int? id)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Adminpanel/Employees/Create
        public ActionResult Create()
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name");
            ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name");
            ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name");
            return View();
        }
		
		// POST: Adminpanel/Employees/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        public ActionResult Create([Bind(Include = "emp_fullname,emp_fathername,emp_dateof_birth,emp_gender_id,emp_phone,emp_address,emp_email,emp_password,emp_dep_id,emp_desig_id,emp_date_of_joining,emp_exit_date,emp_work_status,emp_salary")] Employee employee, HttpPostedFileBase emp_photo, HttpPostedFileBase emp_resume, HttpPostedFileBase emp_offer_letter, HttpPostedFileBase emp_joining_letter, HttpPostedFileBase emp_contact_and_argue, HttpPostedFileBase emp_ID_proof)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name");
			ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name");
			ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name");

			Random rand = new Random();

            if (ModelState.IsValid)
            {
				if (emp_photo != null)
				{
					if (emp_photo.ContentLength < 100 || !emp_photo.ContentType.Contains("image"))
					{
						return View();
					}
				}
				if (emp_resume != null)
				{
					if (emp_resume.ContentLength < 100 || !emp_resume.ContentType.Contains("image"))
					{
						return View();
					}
				}
				if (emp_offer_letter != null)
				{
					if (emp_offer_letter.ContentLength < 100 || !emp_offer_letter.ContentType.Contains("image"))
					{
						return View();
					}
				}
				if (emp_joining_letter != null)
				{
					if (emp_joining_letter.ContentLength < 100 || !emp_joining_letter.ContentType.Contains("image"))
					{
						return View();
					}
				}
				if (emp_contact_and_argue != null)
				{
					if (emp_contact_and_argue.ContentLength < 100 || !emp_contact_and_argue.ContentType.Contains("image"))
					{
						return View();
					}
				}
				if (emp_ID_proof != null)
				{
					if (emp_ID_proof.ContentLength < 100 || !emp_ID_proof.ContentType.Contains("image"))
					{
						return View();
					}
				}
				if(emp_photo != null)
				{
					emp_photo_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_photo.FileName);
					var emp_photo_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_photo_name);
					emp_photo.SaveAs(emp_photo_path);
				}
				if (emp_resume != null)
				{
					emp_resume_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_resume.FileName);
					var emp_resume_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_resume_name);
					emp_resume.SaveAs(emp_resume_path);
				}
				if (emp_offer_letter != null)
				{
					emp_offer_letter_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_offer_letter.FileName);
					var emp_offer_letter_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_offer_letter_name);
					emp_offer_letter.SaveAs(emp_offer_letter_path);
				}
				if (emp_joining_letter != null)
				{
					emp_joining_letter_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_joining_letter.FileName);
					var emp_joining_letter_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_joining_letter_name);
					emp_joining_letter.SaveAs(emp_joining_letter_path);
				}
				if(emp_contact_and_argue != null)
				{
					emp_contact_and_argue_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_contact_and_argue.FileName);
					var emp_contact_and_argue_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_contact_and_argue_name);
					emp_contact_and_argue.SaveAs(emp_contact_and_argue_path);
				}
				if (emp_ID_proof != null)
				{
					emp_ID_proof_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_ID_proof.FileName);
					var emp_ID_proof_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_ID_proof_name);
					emp_ID_proof.SaveAs(emp_ID_proof_path);
				}
				

				employee.emp_work_status = true;
				employee.emp_photo = emp_photo_name;
				employee.emp_resume = emp_resume_name;
				employee.emp_offer_letter = emp_offer_letter_name;
				employee.emp_joining_letter = emp_joining_letter_name;
				employee.emp_contact_and_argue = emp_contact_and_argue_name;
				employee.emp_ID_proof = emp_ID_proof_name;
				employee.emp_dateof_birth = Convert.ToDateTime(employee.emp_dateof_birth);
				employee.emp_date_of_joining = Convert.ToDateTime(employee.emp_date_of_joining);
				db.Employees.Add(employee);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
            return View();
        }

        // GET: Adminpanel/Employees/Edit/5
        public ActionResult Edit(int? id)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name", employee.emp_dep_id);
            ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name", employee.emp_desig_id);
            ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name", employee.emp_gender_id);
            return View(employee);
        }

        // POST: Adminpanel/Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,emp_fullname,emp_fathername,emp_dateof_birth,emp_gender_id,emp_phone,emp_address,emp_email,emp_password,emp_dep_id,emp_desig_id,emp_date_of_joining,emp_exit_date,emp_work_status,emp_salary")] Employee employee, HttpPostedFileBase emp_photo, HttpPostedFileBase emp_resume, HttpPostedFileBase emp_offer_letter, HttpPostedFileBase emp_joining_letter, HttpPostedFileBase emp_contact_and_argue, HttpPostedFileBase emp_ID_proof)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			if (ModelState.IsValid)
            {
				var current_e = db.Employees.FirstOrDefault(e => e.id == employee.id);
				if (employee.emp_fullname != null)
				{
					current_e.emp_fullname = employee.emp_fullname;
				}
				if (employee.emp_fathername != null)
				{
					current_e.emp_fathername = employee.emp_fathername;
				}
				if (employee.emp_fathername != null)
				{
					current_e.emp_fathername = employee.emp_fathername;
				}
				if (employee.emp_dateof_birth != null)
				{
					current_e.emp_dateof_birth = employee.emp_dateof_birth;
				}
				if (employee.emp_gender_id != null)
				{
					current_e.emp_gender_id = employee.emp_gender_id;
				}
				if (employee.emp_phone != null)
				{
					current_e.emp_phone = employee.emp_phone;
				}
				if (employee.emp_address != null)
				{
					current_e.emp_address = employee.emp_address;
				}
				if (employee.emp_email != null)
				{
					current_e.emp_email = employee.emp_email;
				}
				if (employee.emp_password != null)
				{
					current_e.emp_password = employee.emp_password;
				}
				if (employee.emp_dep_id != null)
				{
					current_e.emp_dep_id = employee.emp_dep_id;
				}
				if (employee.emp_date_of_joining != null)
				{
					current_e.emp_date_of_joining = employee.emp_date_of_joining;
				}
				if (employee.emp_exit_date != null)
				{
					current_e.emp_exit_date = employee.emp_exit_date;
				}
				if (employee.emp_work_status != null)
				{
					current_e.emp_work_status = employee.emp_work_status;
				}
				if (employee.emp_salary != null)
				{
					current_e.emp_salary = employee.emp_salary;
				}
				Random rand = new Random();
                if (emp_photo != null)
                {
                    if (emp_photo.ContentLength < 100 || !emp_photo.ContentType.Contains("image"))
                    {
                        return View();
                    }
                }
				if (emp_resume != null)
				{
					if (emp_resume.ContentLength < 100 || !emp_resume.ContentType.Contains("image"))
					{
						return View();
					}
				}
				if (emp_offer_letter != null)
				{
					if (emp_offer_letter.ContentLength < 100 || !emp_offer_letter.ContentType.Contains("image"))
					{
						return View();
					}
				}
				if (emp_joining_letter != null)
				{
					if (emp_joining_letter.ContentLength < 100 || !emp_joining_letter.ContentType.Contains("image"))
					{
						return View();
					}
				}
				if (emp_contact_and_argue != null)
				{
					if (emp_contact_and_argue.ContentLength < 100 || !emp_contact_and_argue.ContentType.Contains("image"))
					{
						return View();
					}
				}
				if (emp_ID_proof != null)
				{
					if (emp_ID_proof.ContentLength < 100 || !emp_ID_proof.ContentType.Contains("image"))
					{
						return View();
					}
				}
				if (emp_photo != null)
				{
					emp_photo_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_photo.FileName);
					var emp_photo_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_photo_name);
					current_e.emp_photo = emp_photo_name;
					emp_photo.SaveAs(emp_photo_path);
				}
				if (emp_resume != null)
				{
					emp_resume_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_resume.FileName);
					var emp_resume_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_resume_name);
					current_e.emp_resume = emp_resume_name;
					emp_resume.SaveAs(emp_resume_path);
				}
				if (emp_offer_letter != null)
				{
					emp_offer_letter_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_offer_letter.FileName);
					var emp_offer_letter_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_offer_letter_name);
					current_e.emp_offer_letter = emp_offer_letter_name;
					emp_offer_letter.SaveAs(emp_offer_letter_path);
				}
				if (emp_joining_letter != null)
				{
					emp_joining_letter_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_joining_letter.FileName);
					var emp_joining_letter_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_joining_letter_name);
					current_e.emp_joining_letter = emp_joining_letter_name;
					emp_joining_letter.SaveAs(emp_joining_letter_path);
				}
				if (emp_contact_and_argue != null)
				{
					emp_contact_and_argue_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_contact_and_argue.FileName);
					var emp_contact_and_argue_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_contact_and_argue_name);
					current_e.emp_contact_and_argue = emp_contact_and_argue_name;
					emp_contact_and_argue.SaveAs(emp_contact_and_argue_path);
				}
				if (emp_ID_proof != null)
				{
					emp_ID_proof_name = rand.Next(11111, 99999).ToString() + (DateTime.Now.ToString("yyyyMMddHHmmss")) + Path.GetExtension(emp_ID_proof.FileName);
					var emp_ID_proof_path = Path.Combine(Server.MapPath("~/Areas/Adminpanel/Uploads/"), emp_ID_proof_name);
					current_e.emp_ID_proof = emp_ID_proof_name;
					emp_ID_proof.SaveAs(emp_ID_proof_path);
				}
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name", employee.emp_dep_id);
            ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name", employee.emp_desig_id);
            ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name", employee.emp_gender_id);
            return View(employee);
        }

        // GET: Adminpanel/Employees/Delete/5
        public ActionResult Delete(int? id)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

		// POST: Adminpanel/Employees/Delete/5
		[HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			Employee employee = db.Employees.Find(id);
			if (System.IO.File.Exists(@"~/Areas/Adminpanel/Uploads/" + employee.emp_photo))
			{
				System.IO.File.Delete(@"~/Areas/Adminpanel/Uploads/" + employee.emp_photo);
			}
			db.Employees.Remove(employee);
            db.SaveChanges();
            return Json(new { success = "deleted"});
        }
		[HttpPost]
		public ActionResult GetDesignationForDropDown(int id)
		{
			db.Configuration.ProxyCreationEnabled = false;
			var test = db.Designations.Where(d => d.depart_id == id).ToList();
			return Json(test, JsonRequestBehavior.AllowGet);
		}


		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
