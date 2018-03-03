using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management_System.ViewModels.Site
{
	public class CheckEmployeeLogin
	{
		public static bool Check()
		{
			if (HttpContext.Current.Session["employee_email"] != null && HttpContext.Current.Session["employee_password"] != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}