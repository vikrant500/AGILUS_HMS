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
    public class DoctorSalariesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: DoctorSalaries
        public ActionResult Index(string doctor = "", string from = "", string to = "", string department = "")

        {
            List<DoctorSalary> search = new List<DoctorSalary>();
             search = db.DoctorSalaries.ToList();
            if (doctor != "")
            {
                search.RemoveAll(x => !x.Doctor.Name.Contains(doctor));
            }
            if (from != "")
            {
                search.RemoveAll(x => x.PaidDate<Convert.ToDateTime(from));
            }
            if (to != "")
            {
                search.RemoveAll(x => x.PaidDate>Convert.ToDateTime(to).AddDays(1));
            }
            if (department != "")
            {
                search.RemoveAll(x => !x.Doctor.Department.Name.Contains(department));
            }
            

            return View(search);
        }

        // GET: DoctorSalaries/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorSalary doctorSalary = db.DoctorSalaries.Find(id);
            if (doctorSalary == null)
            {
                return HttpNotFound();
            }
            return View(doctorSalary);
        }
        public JsonResult GetDoctorACData(string doctorName)
        {
            List<DoctorACResult> doctorACResult = (from doc in db.Doctors
                                                     where doc.Name.Contains(doctorName)
                                                     select
                                                     new DoctorACResult
                                                     {
                                                         ID = doc.ID,
                                                         Name = doc.Name
                                                     }).ToList();

            DoctorACResult[] doctors = new DoctorACResult[doctorACResult.Count];
            doctors = doctorACResult.ToArray();
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDoctorData(int ID)
        {
            DoctorDetailsVm doctorDetails = new DoctorDetailsVm();
            var search = db.Doctors.Where(x => x.ID == ID).FirstOrDefault();
            doctorDetails = new DoctorDetailsVm
            {
                ID = search.ID,
                Name=search.Name,
                DepartmentName=search.Department.Name,
                Charges=search.Charges
            };
            return Json(doctorDetails, JsonRequestBehavior.AllowGet);
        }

        // GET: DoctorSalaries/Create
        public ActionResult Create()
        {
            return View();
        }
        
        public ActionResult CreateDocSalary(string doctorID,string amount)
        {
            var doc = db.Doctors.Find(Convert.ToInt64(doctorID));
            DoctorSalary docsal = new DoctorSalary();
            docsal.Doctor = doc;
            docsal.Amount = Convert.ToDecimal(amount);
            docsal.PaidDate = DateTime.Now;
            db.DoctorSalaries.Add(docsal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // POST: DoctorSalaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create(DoctorSalary doctorSalary)
        {
            if (ModelState.IsValid)
            {
                db.DoctorSalaries.Add(doctorSalary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctorSalary);

        }

        // GET: DoctorSalaries/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorSalary doctorSalary = db.DoctorSalaries.Find(id);
            if (doctorSalary == null)
            {
                return HttpNotFound();
            }
            return View(doctorSalary);
        }

        // POST: DoctorSalaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,Amount,PaidDate")] DoctorSalary doctorSalary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctorSalary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctorSalary);
        }

        // GET: DoctorSalaries/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorSalary doctorSalary = db.DoctorSalaries.Find(id);
            if (doctorSalary == null)
            {
                return HttpNotFound();
            }
            return View(doctorSalary);
        }

        // POST: DoctorSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            DoctorSalary doctorSalary = db.DoctorSalaries.Find(id);
            db.DoctorSalaries.Remove(doctorSalary);
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
public class DoctorACResult
{
    public long ID { get; set; }
    public string Name { get; set; }

}

public class DoctorDetailsVm
{
    public long ID { get; set; }
    public string Name { get; set; }
    public string DepartmentName { get; set; }
    public decimal Charges { get; set; }

}