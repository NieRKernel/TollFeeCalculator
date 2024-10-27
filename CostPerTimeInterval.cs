namespace Toll
{
    public class TownPerTimeIntervalCost : ICostPerTimeInterval
    {
        public List<CostPerTimeInterval> TollFeesIntervals;

        public TownPerTimeIntervalCost()
        {
            // This could be received from some database for instance
            TollFeesIntervals = new List<CostPerTimeInterval>()
            {
                new CostPerTimeInterval(new TimeSpan(6,0,0), new TimeSpan(6,29,0), 8),
                new CostPerTimeInterval(new TimeSpan(6,30,0), new TimeSpan(6,59,0), 13),
                new CostPerTimeInterval(new TimeSpan(7,0,0), new TimeSpan(7,59,0), 18),
                new CostPerTimeInterval(new TimeSpan(8,0,0), new TimeSpan(8,29,0), 13),
                new CostPerTimeInterval(new TimeSpan(8,30,0), new TimeSpan(14,59,0), 8),
                new CostPerTimeInterval(new TimeSpan(15,0,0), new TimeSpan(15,29,0), 13),
                new CostPerTimeInterval(new TimeSpan(15,30,0), new TimeSpan(16,59,0), 18),
                new CostPerTimeInterval(new TimeSpan(17,0,0), new TimeSpan(17,59,0), 13),
                new CostPerTimeInterval(new TimeSpan(18,0,0), new TimeSpan(18,29,0), 13),
            };
        }

        public int ReturnCostWithinTime(DateTime date)
        {
            var foundTollFeeInterval = TollFeesIntervals.FirstOrDefault(x => date.TimeOfDay >= x.StartInterval && date.TimeOfDay <= x.EndInterval);

            return foundTollFeeInterval == null ? 0 : foundTollFeeInterval.CostSek;
        }

        public class CostPerTimeInterval
        {
            public TimeSpan StartInterval;
            public TimeSpan EndInterval;
            public int CostSek;

            public CostPerTimeInterval(TimeSpan startInterval, TimeSpan endInterval, int costSek)
            {
                StartInterval = startInterval;
                EndInterval = endInterval;
                CostSek = costSek;
            }
        }

    }
}
