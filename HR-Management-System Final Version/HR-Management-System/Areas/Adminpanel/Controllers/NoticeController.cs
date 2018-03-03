using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using HR_Management_System.Models;

namespace HR_Management_System.Areas.Adminpanel.Controllers
{
    public class NoticeController : Controller
    {
        HRM_databaseEntities db = new HRM_databaseEntities();

        // GET: Adminpanel/Notice
        public ActionResult Index()
        {
            var departaments = db.Departaments.ToList();
            return View(departaments);
        }
        [HttpPost]
        public ActionResult Index(string title, string description, int departament_id)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("orkhankhf@code.edu.az");
            var emps = db.Employees.Where(e => e.emp_dep_id == departament_id);
            foreach (var item in emps)
            {
                mail.To.Add(item.emp_email);
            }
            mail.Subject = title;
            mail.Body = description;
            mail.IsBodyHtml = false;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("orkhankhf@code.edu.az", "mypassword:)");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            return View();
        }
    }
}