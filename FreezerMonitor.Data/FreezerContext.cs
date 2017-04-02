using System.Data.Entity;
using FreezerMonitor.Data.Entities;

namespace FreezerMonitor.Data
{
    public class FreezerContext : DbContext
    {
        public IDbSet<Sensor> Sensors { get; set; }
        public IDbSet<Reading> Readings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reading>().Property(x => x.Temperature).HasPrecision(7, 4);

            base.OnModelCreating(modelBuilder);
        }
    }
}
