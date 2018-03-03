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
    public class AwardsController : Controller
    {
        private HRM_databaseEntities db = new HRM_databaseEntities();

        // GET: Adminpanel/Awards
        public ActionResult Index()
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			var awards = db.Awards.Include(a => a.Employee);
            return View(awards.ToList());
        }

        // GET: Adminpanel/Awards/Details/5
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
            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return HttpNotFound();
            }
            return View(award);
        }

        // GET: Adminpanel/Awards/Create
        public ActionResult Create()
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			ViewBag.award_emp_id = new SelectList(db.Employees, "id", "emp_fullname");
            return View();
        }

        // POST: Adminpanel/Awards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,award_emp_id,award_name,award_reason,award_cash_price,award_date")] Award award)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			if (ModelState.IsValid)
            {
                db.Awards.Add(award);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.award_emp_id = new SelectList(db.Employees, "id", "emp_fullname", award.award_emp_id);
            return View();
        }

        // GET: Adminpanel/Awards/Edit/5
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
            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return HttpNotFound();
            }
            ViewBag.award_emp_id = new SelectList(db.Employees, "id", "emp_fullname", award.award_emp_id);
            return View(award);
        }

        // POST: Adminpanel/Awards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,award_emp_id,award_name,award_reason,award_cash_price,award_date")] Award award)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			if (ModelState.IsValid)
            {
                db.Entry(award).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.award_emp_id = new SelectList(db.Employees, "id", "emp_fullname", award.award_emp_id);
            return View(award);
        }

        // GET: Adminpanel/Awards/Delete/5
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
            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return HttpNotFound();
            }
            return View(award);
        }

        // POST: Adminpanel/Awards/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			Award award = db.Awards.Find(id);
            db.Awards.Remove(award);
            db.SaveChanges();
            return Json(new { success = "deleted"}, JsonRequestBehavior.AllowGet);
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
