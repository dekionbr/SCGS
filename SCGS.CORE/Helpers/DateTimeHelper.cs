using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Helpers
{
    public static class DateTimeHelper
    {

        public static int DaysInYear(this DateTime date)
        {

            int days = 0;

            for (int i = 0; i <= 11; i++)
            {
                days += DateTime.DaysInMonth(date.Year, i);
            }

            return days;
        }

        public static int WeekInYear(this DateTime date, DateTime atual)
        {
            int week = date.DaysInYear() / 7;

            return week;
        }

        public static int WeekOfYear(this DateTime date)
        {
            var cal = new CultureInfo("pt-BR", false).Calendar;
            int week = cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            return week;
        }

        public static int SemesterOfYear(this DateTime date) {

            double result = date.Month / 6;
            int Semester = result > 0 && result < 1 ? 1 : 
                                        (date.Month / 6) > 1 ? 2 : 1;

            return Semester;
        }
    }
}
