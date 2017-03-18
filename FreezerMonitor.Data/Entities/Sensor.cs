using System.ComponentModel.DataAnnotations.Schema;

namespace FreezerMonitor.Data.Entities
{
    [Table("Sensors", Schema = "Freezer")]
    public class Sensor
    {
        public int    ID           { get; set; }
        public string SensorNumber { get; set; }
        public string Description  { get; set; }
    }
}
