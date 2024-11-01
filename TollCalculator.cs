using Toll;
using TollFeeCalculator;

// TollCalculator could implement an interface for GetTollFeeForVehicle in case different ways of counting the fee rates
// Have tried to utilize the SOLID principles when refactoring the code
public class TollCalculator
{
    private readonly ITollDate m_Tolldate;
    private readonly ICostPerTimeInterval m_CostPerTimeInterval;
    
    // Dependency injection
    // Makes classes more isolated, easier to test, reuse, maintain and narrow down the scope of a given class
    public TollCalculator(ITollDate tollDate, ICostPerTimeInterval costPerTimeInterval)
    {
        m_Tolldate = tollDate;
        m_CostPerTimeInterval = costPerTimeInterval;
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        var costCurrentHour = 0;

        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);

            long diffInMillies = (date.Ticks - intervalStart.Ticks) / 10000; // Fix bug here where milisecond was used instead of ticks
            long minutes = diffInMillies / 1000 / 60;

            // Within an hour then set new highest fee for the hour
            if (minutes <= 60)
            {
                if(costCurrentHour < nextFee)
                {
                    costCurrentHour = nextFee;
                }
            }
            else // If an hour has passed then the highest cost for the current previous lept hour
            {
                totalFee += costCurrentHour == 0 ? nextFee : costCurrentHour;
                costCurrentHour = 0;
                intervalStart = date;
            }
        }

        totalFee += costCurrentHour;

        // Highest fee for a given day is 60, so pick the lowest out of total accumulated total fee and 60
        return Math.Min(totalFee, 60);
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (vehicle.IsVechicleTollFree() || m_Tolldate.IsTollFreeDate(date))
        {
            return 0;
        }

        return m_CostPerTimeInterval.ReturnCostWithinTime(date);
    }


    // Keeping this enum in case of reference to which types are toll free
    // But this value should be set in each respective class implementing Vehicle
    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}