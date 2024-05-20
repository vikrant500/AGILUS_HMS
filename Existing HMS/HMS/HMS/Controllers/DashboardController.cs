using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Infrastructure;
using HMS.VeiwModels;

namespace HMS.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class DashboardController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        private readonly SendMessage sendMessage = new SendMessage();
        // GET: Dashboard
        public ActionResult Index()
        {
            Dashboard dashboard = new Dashboard();
            dashboard.NumOfDoctors = db.Doctors.ToList().Count();
            dashboard.NumOfPatients = db.Patients.ToList().Count();
            dashboard.AppointmentsAttended = db.Appointments.Where(x => x.Status != "Active").ToList().Count();
            dashboard.AppointmentsPending = db.Appointments.Where(x => x.Status == "Active").ToList().Count();
            dashboard.UpcommingAppointments = db.Appointments.OrderByDescending(x => x.AppointmentDate).AsNoTracking().Take(5).ToList();
            dashboard.Doctors = db.Doctors.AsNoTracking().ToList();
            dashboard.NewPatients = db.Patients.OrderByDescending(x => x.DateCreated).AsNoTracking().Take(5).ToList();
            foreach (var item in dashboard.NewPatients)
            {
                item.CareofGuardian = db.Centers.Find(item.CenterID).Name;
            }
            

            return View(dashboard);
        }

        // GET: Dashboard/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dashboard dashboard = db.Dashboards.Find(id);
            if (dashboard == null)
            {
                return HttpNotFound();
            }
            return View(dashboard);
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult GetLabels()
        {
            var data = new ChartData();
            var reslabel1 = new List<string>();
            var resdata1 = new List<int>();
            var reslabel2 = new List<string>();
            var resdata2 = new List<int>();
            var date = DateTime.Now;
            for (var i = 0; i < 4; i++)
            {
                date = DateTime.Today.AddMonths(-i);
                var from = date.AddDays(-(date.Day - 1));
                var to = from.AddMonths(1).AddDays(-1);
                
                reslabel1.Add(date.ToString("MMM"));
                resdata1.Add(db.Patients.Where(x => x.DateCreated > from && x.DateCreated < to).AsNoTracking().ToList().Count());
                
            }
            for (var i = 0; i < 4; i++)
            {
                date = DateTime.Today.AddMonths(-i);
                var from = date.AddDays(-(date.Day - 1));
                var to = from.AddMonths(1).AddDays(-1);

                reslabel2.Add(date.ToString("MMM"));
                resdata2.Add(db.Appointments.Where(x => x.CreatedDateTime > from && x.CreatedDateTime < to).AsNoTracking().ToList().Count());

            }
            
            resdata2.Reverse();
            reslabel2.Reverse();
            resdata1.Reverse();
            reslabel1.Reverse();
            //var count1 = 0;
            //foreach (var item in resdata1)
            //{
            //    if (item == 0)
            //    {
            //        count1 = count1 + 1;
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            //var count2 = 0;
            //foreach (var item in resdata2)
            //{
            //    if (item == 0)
            //    {
            //        count2 = count2 + 1;
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            //resdata1.RemoveRange(0, count1);
            //reslabel1.RemoveRange(0, count1);
            //resdata2.RemoveRange(0, count2);
            //reslabel2.RemoveRange(0, count2);
            data.Label1 = reslabel2;
            data.Num1 = resdata2;
            data.Label2 = reslabel1;
            data.Num2 = resdata1;
            int divider= data.Num1[data.Num1.Count() - 2];
            if (divider == 0)
            {
                divider = 1;
            }
            data.AppointmentPercentage = ((data.Num1[data.Num1.Count()-1]-data.Num1[data.Num1.Count()-2])/divider)*100;
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }
        // POST: Dashboard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "ID,NumOfDoctors,NumOfPatients,AppointmentsAttended,AppointmentsPending")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                db.Dashboards.Add(dashboard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dashboard);
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dashboard dashboard = db.Dashboards.Find(id);
            if (dashboard == null)
            {
                return HttpNotFound();
            }
            return View(dashboard);
        }

        // POST: Dashboard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,NumOfDoctors,NumOfPatients,AppointmentsAttended,AppointmentsPending")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dashboard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dashboard);
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dashboard dashboard = db.Dashboards.Find(id);
            if (dashboard == null)
            {
                return HttpNotFound();
            }
            return View(dashboard);
        }

        // POST: Dashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            Dashboard dashboard = db.Dashboards.Find(id);
            db.Dashboards.Remove(dashboard);
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
public class ChartData
{
    public List<string> Label1 { get; set; }
    public List<string> Label2 { get; set; }
    public List<int> Num1 { get; set; }
    public List<int> Num2 { get; set; }
    public int AppointmentPercentage { get; set; }
}