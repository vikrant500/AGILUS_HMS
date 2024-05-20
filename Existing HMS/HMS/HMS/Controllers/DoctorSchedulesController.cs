using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Infrastructure;
using HMS.Models;

namespace HMS.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class DoctorSchedulesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: DoctorSchedules
        public ActionResult Index(string doctor, string department)
        {
            var search = db.DoctorSchedules.AsNoTracking().ToList();
            if (doctor != null && doctor != "")
            {
                search.RemoveAll(x => x.Doctor.Name != doctor);
            }
            if (department != null && department != "")
            {
                search.RemoveAll(x => x.Doctor.Department.Name!= department);
            }
            var search1 = new List<DoctorSchedule>();
            search1 = search;
            return View(search1);
        }

        // GET: DoctorSchedules/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorSchedule doctorSchedule = db.DoctorSchedules.Find(id);
            if (doctorSchedule == null)
            {
                return HttpNotFound();
            }
            return View(doctorSchedule);
        }

        // GET: DoctorSchedules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "ID,AvailableTime,Status")] DoctorSchedule doctorSchedule)
        {
            if (ModelState.IsValid)
            {
                db.DoctorSchedules.Add(doctorSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctorSchedule);
        }
        public ActionResult CreateDocSchedule(string doctorID,string availabledays,string from,string to,string message,string status)
        {
            DoctorSchedule entry = new DoctorSchedule();
            entry.Doctor = db.Doctors.Find(Convert.ToInt64(doctorID));
            entry.DaysAvailable = availabledays;
            entry.From = from;
            entry.To = to;
            entry.Message = message;
            entry.Status = status;
            db.DoctorSchedules.Add(entry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: DoctorSchedules/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorSchedule doctorSchedule = db.DoctorSchedules.Find(id);
            if (doctorSchedule == null)
            {
                return HttpNotFound();
            }
            return View(doctorSchedule);
        }

        // POST: DoctorSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,AvailableTime,Status")] DoctorSchedule doctorSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctorSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctorSchedule);
        }
        public ActionResult EditDocSchedule(string ID,string doctorID, string availabledays, string from, string to, string message, string status)
        {
            DoctorSchedule entry = db.DoctorSchedules.Find(Convert.ToInt64(ID));
            entry.Doctor = db.Doctors.Find(Convert.ToInt64(doctorID));
            entry.DaysAvailable = availabledays;
            entry.From = from;
            entry.To = to;
            entry.Message = message;
            entry.Status = status;
             db.Entry(entry).State = EntityState.Modified;
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: DoctorSchedules/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorSchedule doctorSchedule = db.DoctorSchedules.Find(id);
            if (doctorSchedule == null)
            {
                return HttpNotFound();
            }
            return View(doctorSchedule);
        }

        // POST: DoctorSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            DoctorSchedule doctorSchedule = db.DoctorSchedules.Find(id);
            db.DoctorSchedules.Remove(doctorSchedule);
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
    }
}
