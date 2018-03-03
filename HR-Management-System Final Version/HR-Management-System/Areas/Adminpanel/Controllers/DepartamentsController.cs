using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HR_Management_System.Models;

namespace HR_Management_System.Areas.Adminpanel.Controllers
{
    public class DepartamentsController : Controller
    {
        private HRM_databaseEntities db = new HRM_databaseEntities();

		int b = 0;

		// GET: Adminpanel/Departaments
		public ActionResult Index()
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			return View(db.Departaments.ToList());
        }

        // GET: Adminpanel/Departaments/Details/5
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
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return HttpNotFound();
            }
            return View(departament);
        }

        // GET: Adminpanel/Departaments/Create
        public ActionResult Create()
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			return View();
        }

        // POST: Adminpanel/Departaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,depart_name")] Departament departament, List<string> designation)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			if (ModelState.IsValid)
            {
				Departament new_departament = new Departament()
				{
					depart_name = departament.depart_name
				};
				db.Departaments.Add(new_departament);
				for (int a = 0; a < designation.Count; a++)
				{
					Designation new_designation = new Designation()
					{
						desig_name = designation[a],
						depart_id = new_departament.id
					};
					db.Designations.Add(new_designation);
				}
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Content(departament.depart_name);
        }

        // GET: Adminpanel/Departaments/Edit/5
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
			db.Configuration.ProxyCreationEnabled = false;
			var designations = db.Designations.Where(d => d.depart_id == id).Select(d => new { id = d.id, name = d.desig_name}).ToList();
			return Json(designations, JsonRequestBehavior.AllowGet);
        }

		// POST: Adminpanel/Departaments/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        public ActionResult Edit(int deptId, string deptName, List<string> designation_id, List<string> designation)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			var departament = db.Departaments.Find(deptId);
			departament.depart_name = deptName;
			
			//bu for dongusu databazada var olan designationlari silmek ve editlemek ucundur
			if(designation_id != null && designation_id.Count != 0)
			{
				for (int a = 0; a < designation_id.Count; a++)
				{
					if (designation[a] != "")
					{
						int des_id = Convert.ToInt32(designation_id[a]);
						Designation single_designation = db.Designations.Where(d => d.id == des_id).SingleOrDefault();
						single_designation.desig_name = designation[a];
					}
					else
					{
						int des_id = Convert.ToInt32(designation_id[a]);
						db.Designations.Remove(db.Designations.Find(des_id));
					}
					b++;
				}
			}
			//bu for dongusu ise databazada var olmayan yeni elave olunacaq designationlari elave etmek ucundur
			for (int a = b; a < designation.Count; a++)
			{
				Designation new_designation = new Designation()
				{
					depart_id = deptId,
					desig_name = designation[a]
				};
				db.Designations.Add(new_designation);
			}
			db.SaveChanges();
            return RedirectToAction("Index", "Departaments");
        }

        // GET: Adminpanel/Departaments/Delete/5
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
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return HttpNotFound();
            }
            return View(departament);
        }

        // POST: Adminpanel/Departaments/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Departament departament = db.Departaments.Find(id);
			if (departament == null)
			{
				return HttpNotFound();
			}
			else
			{
				db.Departaments.Remove(departament);
				db.SaveChanges();
				return Json(new { success = "deleted" }, JsonRequestBehavior.AllowGet);
			}
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
