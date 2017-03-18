using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerMonitor.SqliteData;

namespace FreezerMonitor.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new FreezerContext("Data Source=C:\\temp\\freezer_temps.sqlite"))
            {
                var sensors = context.TemperatureReadings
                    .OrderByDescending(r => r.Timestamp)
                    //.Take(40)
                    .GroupBy(r => r.SensorID);

                foreach (var sensor in sensors)
                {
                    var f = String.Format(".\\{0}", sensor.Key);

                    Console.WriteLine("Sensor {0}", sensor.Key);
                    File.AppendAllText(f, "timestamp,temperature\n");
                    foreach (var reading in sensor.Where(r => r.Timestamp > DateTime.Today.AddDays(-4)))
                    {
                        //Console.WriteLine("   Temp: {0}  --  {1:F2}", reading.Timestamp.ToLocalTime(), reading.Temperature);
                        File.AppendAllText(f, String.Format("{0},{1}\n", reading.Timestamp.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), reading.Temperature));
                    }
                    Console.WriteLine("  Records written to: {0}", f);
                }
            }
            Console.ReadLine();
        }
    }
}
