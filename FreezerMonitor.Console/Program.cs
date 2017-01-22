using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerMonitor.Data;

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
                    .Take(40)
                    .GroupBy(r => r.SensorID);

                foreach (var sensor in sensors)
                {
                    Console.WriteLine("Sensor {0}", sensor.Key);
                    foreach (var reading in sensor)
                    {
                        Console.WriteLine("   Temp: {0}  --  {1:F2}", reading.Timestamp.ToLocalTime(), reading.Temperature);
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadLine();
        }
    }
}
