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
    public class HolidaysController : Controller
    {
        private HRM_databaseEntities db = new HRM_databaseEntities();

        // GET: Adminpanel/Holidays
        public ActionResult Index()
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			return View(db.Holidays.OrderByDescending(h=>h.id).ToList());
        }

        // GET: Adminpanel/Holidays/Details/5
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
            Holiday holiday = db.Holidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }
            return View(holiday);
        }

        // GET: Adminpanel/Holidays/Create
        public ActionResult Create()
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			return View();
        }

        // POST: Adminpanel/Holidays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,holiday_name,holiday_date")] Holiday holiday)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			if (ModelState.IsValid)
            {
                db.Holidays.Add(holiday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(holiday);
        }

        // GET: Adminpanel/Holidays/Edit/5
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
            Holiday holiday = db.Holidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }
            return Json(new { name = holiday.holiday_name, date = string.Format("{0:yyyy-MM-dd}", holiday.holiday_date) },JsonRequestBehavior.AllowGet);
        }

        // POST: Adminpanel/Holidays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,holiday_name,holiday_date")] Holiday holiday)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			if (ModelState.IsValid)
            {
                db.Entry(holiday).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(holiday);
        }

        // GET: Adminpanel/Holidays/Delete/5
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
            Holiday holiday = db.Holidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }
            return View(holiday);
        }

        // POST: Adminpanel/Holidays/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
			if (!AdminpanelMethods.CheckAdminLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			Holiday holiday = db.Holidays.Find(id);
			if (holiday == null)
			{
				return HttpNotFound();
			}
			else
			{
				db.Holidays.Remove(holiday);
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
