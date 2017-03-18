using System.Data.Entity;
using FreezerMonitor.Data.Entities;

namespace FreezerMonitor.Data
{
    public class FreezerContext : DbContext
    {
        public IDbSet<Sensor> Sensors { get; set; }
        public IDbSet<Reading> Readings { get; set; }
    }
}
