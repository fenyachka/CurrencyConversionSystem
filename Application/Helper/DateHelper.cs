using System;

namespace Application.Helper
{
    public static class DateHelper
    {
        public static Tuple<DateTime, DateTime> GetDateRange(DateTime date)
        {
            var rangeDateFrom = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            var rangeDateTo = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            
            return Tuple.Create(rangeDateFrom, rangeDateTo);
        }
    }
}