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
            var planes = db.Planes.ToList();
            var compamies = db.Companies.ToList();
            var schedule = db.Flights.ToList();
            return View(schedule);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SelectList planes = new SelectList(db.Planes.Where(x=>x.InFlight == false).Select(x=>x.PlaneNum));
            
            List<SelectListItem> planesList = new List<SelectListItem>(planes);
            Plane plane = db.Flights.Where(x => x.flight1 == id).Select(x=>x.Plane1).First();
            planesList.Add(new SelectListItem() { Text = plane.PlaneNum.ToString(), Value = plane.PlaneNum.ToString(), Selected = true });
            ViewBag.Planes = planesList;
            var flight = db.Flights.Where(x => x.flight1 == id).First();
            return View(flight);
        }

        [HttpPost]
        public ActionResult Edit(Flight flight)
        {
            foreach (var item in db.Planes)
            {
                if (!db.Flights.Select(x => x.Plane).Contains(item.PlaneNum))
                    item.InFlight = false;
            }
            db.Entry(flight).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var detail = db.Flights.Where(x => x.flight1 == id).First();
            return View(detail);
        }

        public ActionResult Delete(int id)
        {
            var flight = db.Flights.Where(x => x.flight1 == id).First();
            db.Entry(flight).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            SelectList planes = new SelectList(db.Planes.Where(x => x.InFlight == false).Select(x => x.PlaneNum));

            List<SelectListItem> planesList = new List<SelectListItem>(planes);
            ViewBag.Planes = planesList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Flight flight)
        {
            db.Entry(flight).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}