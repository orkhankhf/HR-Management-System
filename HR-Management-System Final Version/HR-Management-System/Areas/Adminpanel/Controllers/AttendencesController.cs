using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HR_Management_System.Models;
using HR_Management_System.ViewModels.Adminpanel;

namespace HR_Management_System.Areas.Adminpanel.Controllers
{
    public class AttendencesController : Controller
    {
        private HRM_databaseEntities db = new HRM_databaseEntities();

        // GET: Adminpanel/Attendences
        public ActionResult Index()
        {
            if (!AdminpanelMethods.CheckAdminLogin())
            {
                return RedirectToAction("Index", "Login");
            }
            var attendences = db.Employees.Include(a => a.Attendences);
            return View(attendences.ToList());
        }

        // GET: Adminpanel/Attendences/Details/5
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
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // GET: Adminpanel/Attendences/Create
        public ActionResult Create()
        {
            if (!AdminpanelMethods.CheckAdminLogin())
            {
                return RedirectToAction("Index", "Login");
            }
            List<Employee> employeers = db.Employees.ToList();
			List<Leave_type> leave_types = db.Leave_type.ToList();
            return View(new CreateAttendence { Employee = employeers, Leave_type = leave_types});
        }

        // POST: Adminpanel/Attendences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(List<int> leaveType, List<string> reasonText, List<int> empId)
        {
            if (!AdminpanelMethods.CheckAdminLogin())
            {
                return RedirectToAction("Index", "Login");
            }
            var all_employers = db.Employees.ToList();
            foreach (var item in all_employers)
            {
                Attendence new_attendence = new Attendence();
                new_attendence.atten_date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                new_attendence.atten_emp_id = item.id;
                new_attendence.atten_status = true;
                if (empId != null)
                {
                    for (int a = 0; a < leaveType.Count; a++)
                    {
                        if(item.id == empId[a])
                        {
                            new_attendence.atten_leave_type_id = leaveType[a];
                            new_attendence.atten_status = false;
                            new_attendence.atten_reason = reasonText[a];
                        }
                    }
                }
                db.Attendences.Add(new_attendence);
            }
            db.SaveChanges();

            List<Employee> employeers = db.Employees.ToList();
            List<Leave_type> leave_types = db.Leave_type.ToList();
            return View(new CreateAttendence { Employee = employeers, Leave_type = leave_types });
        }

        // GET: Adminpanel/Attendences/Edit/5
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
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.atten_emp_id = new SelectList(db.Employees, "id", "emp_fullname", attendence.atten_emp_id);
            ViewBag.atten_leave_type_id = new SelectList(db.Leave_type, "id", "type_name", attendence.atten_leave_type_id);
            return View(attendence);
        }

        // POST: Adminpanel/Attendences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,atten_emp_id,atten_status,atten_leave_type_id,atten_date,atten_reason")] Attendence attendence)
        {
            if (!AdminpanelMethods.CheckAdminLogin())
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(attendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.atten_emp_id = new SelectList(db.Employees, "id", "emp_fullname", attendence.atten_emp_id);
            ViewBag.atten_leave_type_id = new SelectList(db.Leave_type, "id", "type_name", attendence.atten_leave_type_id);
            return View(attendence);
        }

        // GET: Adminpanel/Attendences/Delete/5
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
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // POST: Adminpanel/Attendences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!AdminpanelMethods.CheckAdminLogin())
            {
                return RedirectToAction("Index", "Login");
            }
            Attendence attendence = db.Attendences.Find(id);
            db.Attendences.Remove(attendence);
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
