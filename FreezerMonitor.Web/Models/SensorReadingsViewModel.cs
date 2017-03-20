using System;
using System.Collections.Generic;
using System.Linq;
using FreezerMonitor.Data.Entities;

namespace FreezerMonitor.Web.Models
{
    public class SensorReadingsViewModel
    {
        public IList<IGrouping<int, Reading>> Sensors { get; set; }
    }
}