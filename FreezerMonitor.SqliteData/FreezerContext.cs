using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerMonitor.SqliteData
{
    public class SqliteFreezerContext : DbContext
    {
        public SqliteFreezerContext(String connectionString)
            : base(new SQLiteConnection() {ConnectionString = connectionString}, true)
        {
            Debug.WriteLine(connectionString);
        }

        public DbSet<TemperatureReading> TemperatureReadings { get; set; }
    }
}
