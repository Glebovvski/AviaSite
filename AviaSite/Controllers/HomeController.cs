using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AviaSite.Controllers
{
    public class HomeController : Controller
    {
        AviaEntities db = new AviaEntities();

        public ActionResult Index()
        {
            var schedule = db.Flights.ToList();
            return View(schedule);
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