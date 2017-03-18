using FreezerMonitor.SqliteData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerMonitor.DataLoader
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var context = new SqliteFreezerContext("Data Source=C:\\temp\\freezer_temps.sqlite"))
            {
                var sensors = context.TemperatureReadings
                    .OrderByDescending(r => r.Timestamp)
                    //.Take(40)
                    .GroupBy(r => r.SensorID);
            }

            }
        }
}
