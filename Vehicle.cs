namespace TollFeeCalculator
{
    public abstract class Vehicle
    {
        public string GetVehicleType()
        {
            return GetType().Name;
        }
        public abstract bool IsVechicleTollFree();
    }
}