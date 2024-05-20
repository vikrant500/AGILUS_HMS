using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Infrastructure;
using HMS.Models;

namespace HMS.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class StaffSalariesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: StaffSalaries
        public ActionResult Index(string staff = "", string from = "", string to = "", string department = "")

        {
            
            List<StaffSalary> search = new List<StaffSalary>();
            search = db.StaffSalaries.ToList();
            if (staff != "")
            {
                search.RemoveAll(x => !x.Staff.Name.Contains(staff));
            }
            if (from != "")
            {
                search.RemoveAll(x => x.PaidDate < Convert.ToDateTime(from));
            }
            if (to != "")
            {
                search.RemoveAll(x => x.PaidDate > Convert.ToDateTime(to).AddDays(1));
            }
            if (department != "")
            {
                search.RemoveAll(x => !x.Staff.Department.Name.Contains(department));
            }


            return View(search);
        }

        // GET: StaffSalaries/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffSalary staffSalary = db.StaffSalaries.Find(id);
            if (staffSalary == null)
            {
                return HttpNotFound();
            }
            return View(staffSalary);
        }
        public JsonResult GetStaffACData(string staffName)
        {
            List<StaffACResult> staffACResult = (from sta in db.Staffs
                                                   where sta.Name.Contains(staffName)
                                                   select
                                                   new StaffACResult
                                                   {
                                                       ID = sta.ID,
                                                       Name = sta.Name
                                                   }).ToList();

            StaffACResult[] staffs = new StaffACResult[staffACResult.Count];
            staffs = staffACResult.ToArray();
            return Json(staffs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStaffData(int ID)
        {
            StaffDetailsVm staffDetails = new StaffDetailsVm();
            var search = db.Staffs.Where(x => x.ID == ID).FirstOrDefault();
            staffDetails = new StaffDetailsVm
            {
                ID = search.ID,
                Name = search.Name,
                DepartmentName = search.Department.Name,
                Charges = search.Charges
            };
            return Json(staffDetails, JsonRequestBehavior.AllowGet);
        }

        // GET: StaffSalaries/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateStaSalary(string staffID, string amount)
        {
            var sta = db.Staffs.Find(Convert.ToInt64(staffID));
            StaffSalary stasal = new StaffSalary();
            stasal.Staff = sta;
            stasal.Amount = Convert.ToDecimal(amount);
            stasal.PaidDate = DateTime.Now;
            db.StaffSalaries.Add(stasal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // POST: StaffSalaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create(StaffSalary staffSalary)
        {
            if (ModelState.IsValid)
            {
                db.StaffSalaries.Add(staffSalary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffSalary);

        }

        // GET: StaffSalaries/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffSalary staffSalary = db.StaffSalaries.Find(id);
            if (staffSalary == null)
            {
                return HttpNotFound();
            }
            return View(staffSalary);
        }

        // POST: StaffSalaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,Amount,PaidDate")] StaffSalary staffSalary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffSalary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffSalary);
        }

        // GET: StaffSalaries/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffSalary staffSalary = db.StaffSalaries.Find(id);
            if (staffSalary == null)
            {
                return HttpNotFound();
            }
            return View(staffSalary);
        }

        // POST: StaffSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            StaffSalary staffSalary = db.StaffSalaries.Find(id);
            db.StaffSalaries.Remove(staffSalary);
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
public class StaffACResult
{
    public long ID { get; set; }
    public string Name { get; set; }

}

public class StaffDetailsVm
{
    public long ID { get; set; }
    public string Name { get; set; }
    public string DepartmentName { get; set; }
    public decimal Charges { get; set; }

}