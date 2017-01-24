using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreezerMonitor.Data;
using System.Net;
using System.Net.Mime;

namespace FreezerMonitor.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new FreezerContext("Data Source=C:\\temp\\freezer_temps.sqlite"))
            {
                var startDate = DateTime.Now.AddDays(-2);
                var sensors = context.TemperatureReadings
                    .Where(r => r.Timestamp >= startDate)
                    .OrderBy(r => r.SensorID)
                    .ThenBy(r => r.Timestamp)
                    .GroupBy(r => r.SensorID)
                    .ToList();
                return View(new Models.SensorReadingsViewModel { Sensors = sensors });
            }
        }

        
        public ActionResult Log(String id)
        {
            try
            {
                var f = File(String.Format("~/{0}",id), MediaTypeNames.Text.Plain);
                return f;
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}