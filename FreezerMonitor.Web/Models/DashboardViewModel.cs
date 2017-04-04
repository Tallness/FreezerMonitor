namespace FreezerMonitor.Web.Models
{
    public class DashboardViewModel
    {
        public int DaysInPeriod { get; set; }
        public decimal MaxTemp { get; set; }
        public decimal LastTemp { get; set; }
        public int TemperatureTrend { get; set; }
        public int MinutesAboveFreezing { get; set; }
        public decimal PortionOfTimeAboveFreezing
        {
            get
            {
                return (decimal)MinutesAboveFreezing / (DaysInPeriod * 24m * 60m);
            }
        }
    }
}