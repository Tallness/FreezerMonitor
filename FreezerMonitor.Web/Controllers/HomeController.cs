using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreezerMonitor.Data;
using System.Net;
using FreezerMonitor.Web.Models;
using Newtonsoft.Json;
using FreezerMonitor.Data.Entities;
using System.Net.Http;

namespace FreezerMonitor.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int days = 7)
        {
            return View();
        }

        [Route("dashboard")]
        public JsonResult DashboardData(int days = 7)
        {
            var startTime = DateTime.UtcNow.AddDays(days * -1);

            using (var db = new FreezerContext())
            {
                var query = db.Readings
                    .Where(r => r.Time >= startTime && r.Sensor.Description == "Freezer");

                var model = new DashboardViewModel()
                {
                    DaysInPeriod = days,
                    MaxReading = query.OrderByDescending(r => r.Temperature).FirstOrDefault(),
                    LastReading = query.OrderByDescending(r => r.Time).FirstOrDefault(),
                    MinutesAboveFreezing = query.Count(r => r.Temperature > 32.0m) * 5
                };
                return new JsonNetResult() { Data = model };
            }
        }

        [Route("Chart")]
        public ActionResult Chart()
        {
            using (var context = new FreezerContext())
            {
                var startDate = new DateTime(2017, 1, 18);
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
                            time = r.Time,
                            temperature = r.Temperature
                        })
                        .ToList();
                    return new JsonNetResult() { Data = readings };
                    //return Json(readings, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("readings/{sensorId}")]
        public HttpResponseMessage LogReading(string sensorId, DateTime time, decimal temperature)
        {
            using (var db = new FreezerContext())
            {
                var sensor = db.Sensors.SingleOrDefault(s => s.SensorNumber == sensorId);

                var newReading = new Reading()
                {
                    SensorID = sensor.ID,
                    Time = time,
                    Temperature = temperature
                };
                db.Readings.Add(newReading);
                try
                {
                    db.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
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

        public class JsonNetResult : JsonResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                    throw new ArgumentNullException("context");

                var response = context.HttpContext.Response;

                response.ContentType = !String.IsNullOrEmpty(ContentType)
                    ? ContentType
                    : "application/json";

                if (ContentEncoding != null)
                    response.ContentEncoding = ContentEncoding;

                // If you need special handling, you can call another form of SerializeObject below
                var serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented);
                response.Write(serializedObject);
            }
        }
    }
}