namespace TollFeeCalculator
{
    public abstract class Vehicle
    {
        public string GetVehicleType()
        {
            return GetType().Name;
        }

        // Instead of having a list of enum and checking against the class if the vechicle type is toll free, it's more
        // logical to have it on the vehicle itself. Then we can also ensure that it is always implemented
        public abstract bool IsVechicleTollFree();
    }
}