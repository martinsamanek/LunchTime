using System;

namespace LunchTime.Helpers
{
    public class CalculateStartOfWeekHelper
    {
        public static DateTime StartOfWeek()
        {
            var diff = DateTime.Now.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }
            return DateTime.Now.AddDays(-1 * diff).Date;
        }
    }
}
