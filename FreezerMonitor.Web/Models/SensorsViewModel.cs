using System.Collections.Generic;
using FreezerMonitor.Data.Entities;

namespace FreezerMonitor.Web.Models
{
    public class SensorsViewModel
    {
        public IList<Sensor> Sensors { get; set; }
    }
}