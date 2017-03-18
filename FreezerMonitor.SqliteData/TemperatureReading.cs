using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreezerMonitor.SqliteData
{
    [Table("temperatures")]
    public class TemperatureReading
    {
        [Key]
        [Column(Order = 1)]
        public DateTime Timestamp { get; set; }
        [Key]
        [Column(name: "sensor_id", Order = 2)]
        public String SensorID { get; set; }
        public Decimal Temperature { get; set; }
    }
}
