using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HR_Management_System.Models;

namespace HR_Management_System.ViewModels.Site
{
	public class EmployeeAccount
	{
		public Employee Employee { get; set; }
		public List<Holiday> Holiday { get; set; }
		public List<Leave_status> Leave_status { get; set; }
		public string CalcAtWork { get; set; }
		public List<UserBirthdays> UserBirthdays { get; set; }
	}
}