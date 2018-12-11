using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AviaSite;

namespace AviaSite.Controllers
{
    public class TicketsController : Controller
    {
        private AviaEntities db = new AviaEntities();

        // GET: Tickets
        public ActionResult Index()
        {
            ViewBag.FlightId = new SelectList(db.Flights, "flight1", "FlightDesc");
            var tickets = db.Tickets.Include(t => t.Flight);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.FlightId = new SelectList(db.Flights, "flight1", "FlightDesc");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketId,FlightId,SeatNumber")] Ticket ticket)
        {
            if (!db.Tickets.Any(x => x.SeatNumber == ticket.SeatNumber && x.FlightId == ticket.FlightId))
            {
                if (ModelState.IsValid)
                {
                    db.Tickets.Add(ticket);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else TempData["AlreadyExist"] = "This seat is already booked!";
            ViewBag.FlightId = new SelectList(db.Flights, "flight1", "FlightDesc", ticket.FlightId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlightId = new SelectList(db.Flights, "flight1", "From", ticket.FlightId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketId,FlightId,SeatNumber")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlightId = new SelectList(db.Flights, "flight1", "From", ticket.FlightId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.FlightId = new SelectList(db.Flights, "flight1", "FlightDesc");
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetFlight(int flightId, DateTime? date)
        {
            var res = new List<Ticket>();
            ViewBag.FlightId = new SelectList(db.Flights, "flight1", "FlightDesc");
            if (date == null)
                res = db.Tickets.Where(x => x.FlightId == flightId).ToList();
            else res = db.Tickets.Where(x => x.FlightId == flightId && x.Flight.Date==date).ToList();
            if (res.Count == 0)
                TempData["NoTickets"] = "There are no tickets bought for this flight";
            return View("Index", res);
        }

        public ActionResult GetReport(DateTime start, DateTime end)
        {
            ViewBag.FlightId = new SelectList(db.Flights, "flight1", "FlightDesc");
            return View("index", db.Tickets.Where(x => x.Flight.Date.Month >= start.Month && x.Flight.Date.Month <= end.Month).ToList());
        }
    }
}
