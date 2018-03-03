using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management_System.ViewModels.Adminpanel
{
	public class CalcAtWork
	{
		public DateTime JoiningDate { get; set; }
		public CalcAtWork(DateTime _joiningDate)
		{
			this.JoiningDate = _joiningDate;
		}
		public string GetWorkTime
		{
			get
			{
				return CalcDate(JoiningDate);
			}
		}
		public string CalcDate(DateTime _date)
		{
			var year = 0;
			var month = 0;
			var day = 0;
			DateTime time = DateTime.Parse(_date.ToString());
			DateTime now = DateTime.Parse(DateTime.Now.ToString());
			if (time.Month > now.Month)
			{
				year = now.Year - time.Year - 1;
				if (time.Day > now.Day)
				{
					month = now.Month - time.Month + 12 - 1;
					if (now.Month == 01 || now.Month == 07 || now.Month == 05 || now.Month == 08 || now.Month == 10 || now.Month == 12)
					{
						day = now.Day - time.Day + 30;
					}
					else if (now.Month == 03)
					{
						day = now.Day - time.Day + 28;

					}
					else
					{
						day = now.Day - time.Day + 31;
					}
				}
				else
				{
					month = now.Month - time.Month + 12;
					day = now.Day - time.Day;
				}
			}
			else
			{
				year = now.Year - time.Year;
				if (time.Day > now.Day)
				{
					month = now.Month - time.Month - 1;
					if (now.Month == 01 || now.Month == 07 || now.Month == 05 || now.Month == 08 || now.Month == 10 || now.Month == 12)
					{
						day = now.Day - time.Day + 30;
					}
					else if (now.Month == 03)
					{
						day = now.Day - time.Day + 28;

					}
					else
					{
						day = now.Day - time.Day + 31;
					}
				}
				else
				{
					month = now.Month - time.Month;
					day = now.Day - time.Day;
				}
			}
			return year.ToString() + "year " + month.ToString() + "month " + day.ToString() + "day";
		}
	}
}