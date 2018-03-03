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
    public class LeaveAppController : Controller
    {
        private HRM_databaseEntities db = new HRM_databaseEntities();

        // GET: Adminpanel/LeaveApp
        public ActionResult Index()
        {
            var leave_App = db.Leave_App.Include(l => l.Employee).Include(l => l.Leave_status);
            return View(leave_App.ToList());
        }

        // GET: Adminpanel/LeaveApp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave_App leave_App = db.Leave_App.Find(id);
            if (leave_App == null)
            {
                return HttpNotFound();
            }
            return View(leave_App);
        }

        // GET: Adminpanel/LeaveApp/Create
        public ActionResult Create()
        {
            ViewBag.leave_emp_id = new SelectList(db.Employees, "id", "emp_fullname");
            ViewBag.leave_status_id = new SelectList(db.Leave_status, "id", "status_name");
            return View();
        }

        // POST: Adminpanel/LeaveApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,leave_emp_id,leave_date,leave_reason,leave_status_id")] Leave_App leave_App)
        {
            if (ModelState.IsValid)
            {
                db.Leave_App.Add(leave_App);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.leave_emp_id = new SelectList(db.Employees, "id", "emp_fullname", leave_App.leave_emp_id);
            ViewBag.leave_status_id = new SelectList(db.Leave_status, "id", "status_name", leave_App.leave_status_id);
            return View(leave_App);
        }

        // GET: Adminpanel/LeaveApp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave_App leave_App = db.Leave_App.Find(id);
            if (leave_App == null)
            {
                return HttpNotFound();
            }
            ViewBag.leave_emp_id = new SelectList(db.Employees, "id", "emp_fullname", leave_App.leave_emp_id);
            ViewBag.leave_status_id = new SelectList(db.Leave_status, "id", "status_name", leave_App.leave_status_id);
            return View(leave_App);
        }

        // POST: Adminpanel/LeaveApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int id, int status_id)
        {
			var change_status = db.Leave_App.Find(id);
			change_status.leave_status_id = status_id;
			db.SaveChanges();
			return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Adminpanel/LeaveApp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave_App leave_App = db.Leave_App.Find(id);
            if (leave_App == null)
            {
                return HttpNotFound();
            }
            return View(leave_App);
        }

        // POST: Adminpanel/LeaveApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leave_App leave_App = db.Leave_App.Find(id);
            db.Leave_App.Remove(leave_App);
            db.SaveChanges();
            return RedirectToAction("Index");
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
