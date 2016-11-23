namespace DirectOrderTracker.Services
{
    public class Calculations
    {
        public static decimal MarginCalc(decimal? qty, decimal? price, decimal? cost, decimal? freight)
        {
            return ((price - cost) * qty - freight).HasValue ? ((price - cost ) * qty - freight).Value : 0;
        }

    }
}