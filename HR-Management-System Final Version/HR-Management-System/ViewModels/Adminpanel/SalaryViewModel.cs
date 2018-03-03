using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HR_Management_System.Models;

namespace HR_Management_System.ViewModels.Adminpanel
{
    public class SalaryViewModel
    {
        public List<Employee> employee { get; set; }
        public List<Award> award { get; set; }
        public List<Leave_App> leave_app { get; set; }
    }
}