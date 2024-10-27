using Toll;
using TollFeeCalculator;

// TollCalculator could implement an interface for GetTollFeeForVehicle in case different ways of counting the fee rates exists
public class TollCalculator
{
    private readonly ITollDate m_Tolldate;
    private readonly ICostPerTimeInterval m_CostPerTimeInterval;
    
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
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle); 

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMillies / 1000 / 60;

            // Within an hour then adjust the fee
            if (minutes <= 60)
            {
                if (totalFee > 0)
                { 
                    totalFee -= tempFee;
                }
           
                // Add the highest out of next fee or temporary temp fee
                totalFee += Math.Max(nextFee, tempFee);
            }
            else // If an hour has passed then add to the total
            {
                totalFee += nextFee;
            }
        }

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