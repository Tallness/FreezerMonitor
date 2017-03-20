using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreezerMonitor.Data;
using System.Net;
using System.Net.Mime;
using System.Data.Entity;

namespace FreezerMonitor.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new FreezerContext())
            {
                var startDate = new DateTime(2017,1,18);
                var sensors = context.Sensors
                    .ToList();
                return View(new Models.SensorsViewModel { Sensors = sensors });
            }
        }

        
        public ActionResult Log(string id)
        {
            try
            {
                var startDate = DateTime.Parse("3/14/2017");
                using (var db = new FreezerContext())
                {
                    var readings = db.Readings
                        //.Include(r => r.Sensor)
                        .Where(r => r.Sensor.SensorNumber == id && r.Time >= startDate)
                        .Select(r => new
                        {
                            time = r.Time.ToString(),
                            temperature = r.Temperature
                        })
                        .ToList();
                    return Json(readings, JsonRequestBehavior.AllowGet);
                }
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