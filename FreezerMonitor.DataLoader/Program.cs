using FreezerMonitor.Data;
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
            using (var context = new FreezerContext())
            {
                var lastReadingDate = context.Readings.Max(r => r.Time);

                using (var sqliteContext = new SqliteFreezerContext("Data Source=C:\\temp\\freezer_temps.sqlite"))
                {
                    var rawReadings = sqliteContext.TemperatureReadings
                        .Where(r => r.Timestamp > lastReadingDate)
                        .OrderBy(r => r.Timestamp);
                    Console.WriteLine("{0} records found to import.", rawReadings.Count());

                    var sensors = context.Sensors.ToDictionary(s => s.SensorNumber, s => s.ID);
                    var recordCount = 0;
                    var writtenRecordCount = 0;
                    foreach (var rawReading in rawReadings)
                    {
                        context.Readings.Add(new Data.Entities.Reading()
                        {
                            SensorID = sensors[rawReading.SensorID],
                            Time = rawReading.Timestamp.ToUniversalTime(),
                            Temperature = rawReading.Temperature
                        });
                        recordCount++;

                        if (recordCount >=100)
                        {
                            context.SaveChanges();
                            writtenRecordCount += recordCount;
                            Console.WriteLine("{0} total records written.", writtenRecordCount);
                            recordCount = 0;
                        }
                    }
                    context.SaveChanges();
                }
            }

        }
    }
}
