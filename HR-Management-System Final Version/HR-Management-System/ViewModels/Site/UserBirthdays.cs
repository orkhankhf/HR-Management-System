using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management_System.ViewModels.Site
{
	public class UserBirthdays
	{
		public string Fullname { get; set; }
		public string ProfileImage { get; set; }
		public DateTime Birthdate { get; set; }
		public int DayOfBirthdate { get {
				return Birthdate.Day;
			}
		}
		public string BirthMonth { get
			{
				return FindMonth();
			}
		}
		public string FindMonth()
		{
			switch (Birthdate.Month)
			{
				case 1:
					return "Yanvar";
				case 2:
					return "Fevral";
				case 3:
					return "Mart";
				case 4:
					return "Aprel";
				case 5:
					return "May";
				case 6:
					return "Iyun";
				case 7:
					return "Iyul";
				case 8:
					return "Avqust";
				case 9:
					return "Sentyabr";
				case 10:
					return "Oktyabr";
				case 11:
					return "Noyabr";
				case 12:
					return "Dekabr";
				default:
					return DateTime.Now.Month.ToString();
			}
		}
	}
}