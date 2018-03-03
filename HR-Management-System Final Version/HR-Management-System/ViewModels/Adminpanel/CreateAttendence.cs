using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HR_Management_System.Models;

namespace HR_Management_System.ViewModels.Adminpanel
{
	public class CreateAttendence
	{
		public List<Employee> Employee { get; set; }
		public List<Leave_type> Leave_type { get; set; }
	}
}