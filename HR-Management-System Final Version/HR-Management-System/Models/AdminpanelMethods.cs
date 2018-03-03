using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management_System.Models
{
	public class AdminpanelMethods
	{
		public static bool CheckAdminLogin()
		{
			if(HttpContext.Current.Session["admin_email"] != null && HttpContext.Current.Session["admin_password"] != null)
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