using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreezerMonitor.Data;

namespace FreezerMonitor.Web.Models
{
    public class SensorReadingsViewModel
    {
        public IList<IGrouping<String, TemperatureReading>> Sensors { get; set; }
    }
}