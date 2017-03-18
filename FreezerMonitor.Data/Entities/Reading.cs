using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreezerMonitor.Data.Entities
{
    [Table("Readings", Schema = "Freezer")]
    public class Reading
    {
        public int      ID          { get; set; }
        public DateTime Time        { get; set; }
        public decimal  Temperature { get; set; }

        public int            SensorID { get; set; }
        public virtual Sensor Sensor   { get; set; }
    }
}
