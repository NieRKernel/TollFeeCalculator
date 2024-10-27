namespace Toll
{
    public class TollDates : ITollDate
    {
        private static bool IsWeekDay(DateTime date) => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        private static bool IsJulyMonth(DateTime date) => date.Month == 7;

        // This could read from a server or list and parse it into this format
        // This list should also be teste so there are no duplicate months and this could be done at startup
        public static Dictionary<int, List<MonthAndDays>> FreeDatesPerYear = new Dictionary<int, List<MonthAndDays>>
        {
            { 2013,
                new List<MonthAndDays>
                { 
                    new MonthAndDays(){ Month = 1, Days = new List<int> { 1 } },
                    new MonthAndDays(){ Month = 3, Days = new List<int> { 28, 29 } },
                    new MonthAndDays(){ Month = 4, Days = new List<int> { 1, 30 } },
                    new MonthAndDays(){ Month = 5, Days = new List<int> { 1, 8, 9 } },
                    new MonthAndDays(){ Month = 6, Days = new List<int> { 5, 6, 21 } },
                    new MonthAndDays(){ Month = 11, Days = new List<int> { 1 } },
                    new MonthAndDays(){ Month = 12, Days = new List<int> { 24, 25, 26, 31 } }
                }
        }};

        public bool IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            // Check if weekday, every weekday no matter the year is free
            // July is also free everymonth
            if (IsWeekDay(date) || IsJulyMonth(date))
            {
                return true;
            }

            var yearExisted = FreeDatesPerYear.TryGetValue(year, out var monthCalander);

            if (!yearExisted)
            {
                // Error log and say year calendar was not defined
                return false;
            }

            var isDayFreeToday = monthCalander.Any(x => (x.Month == month && x.Days.Count == 0) || (x.Month == month && x.Days.Contains(day)));

            return isDayFreeToday;
        }

    }

    public class MonthAndDays
    {
        public int Month { get; set; }
        public required List<int> Days { get; set; }
    }
}
