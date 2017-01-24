using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerMonitor.Data
{
    public class FreezerContext : DbContext
    {
        public FreezerContext(String connectionString)
            : base(new SQLiteConnection() {ConnectionString = connectionString}, true)
        {
            Debug.WriteLine(connectionString);
        }

        public DbSet<TemperatureReading> TemperatureReadings { get; set; }
    }
}
