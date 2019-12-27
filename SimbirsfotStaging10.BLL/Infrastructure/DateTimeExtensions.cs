using System;
using System.Collections.Generic;
using System.Text;

namespace SimbirsfotStaging10.BLL.Infrastructure
{
    public static class DateTimeExtensions
    {
        public static int CompareByDateMonthYear(this DateTime date1, DateTime date2)
            => (date1.Year, date1.Month, date1.Day).CompareTo((date2.Year, date2.Month, date2.Day));

    }
}
