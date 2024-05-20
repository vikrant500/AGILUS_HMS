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
    public class StaffsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Staffs
        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            StaffViewModel staffViewModel = new StaffViewModel();
            FillDepartmentDropDown(staffViewModel);
            return View(staffViewModel);
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create(StaffViewModel staff)
        {
            Staff newStaff = new Staff();
            newStaff.Name = staff.StaffName;
            newStaff.Department = db.Departments.Where(x => x.ID == staff.DepartmentID).FirstOrDefault();
            newStaff.Address = staff.Address;
            newStaff.Age = staff.Age;
            newStaff.Charges = staff.Charges;
            newStaff.DateOfBirth = staff.DateOfBirth;
            newStaff.DateOfJoining = staff.DateOfJoining;
            newStaff.Email = staff.Email;
            newStaff.PaymentType = staff.PaymentType;
            newStaff.Phone = staff.Phone;
            newStaff.Qualification = staff.Qualification;
            newStaff.Speciality = staff.Speciality;
            if (ModelState.IsValid)
            {
                db.Staffs.Add(newStaff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            StaffViewModel newStaff = new StaffViewModel();
            newStaff.ID = staff.ID;
            newStaff.StaffName = staff.Name;
            newStaff.DepartmentID = (int)staff.Department.ID;
            newStaff.Address = staff.Address;
            newStaff.Age = staff.Age;
            newStaff.Charges = staff.Charges;
            newStaff.DateOfBirth = staff.DateOfBirth;
            newStaff.DateOfJoining = staff.DateOfJoining;
            newStaff.Email = staff.Email;
            newStaff.PaymentType = staff.PaymentType;
            newStaff.Phone = staff.Phone;
            newStaff.Qualification = staff.Qualification;
            newStaff.Speciality = staff.Speciality;
            if (staff == null)
            {
                return HttpNotFound();
            }
            FillDepartmentDropDown(newStaff);
            return View(newStaff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit(StaffViewModel staff)
        {
            Staff newStaff = db.Staffs.Find(staff.ID);
            newStaff.Name = staff.StaffName;
            newStaff.Department = db.Departments.Where(x => x.ID == staff.DepartmentID).FirstOrDefault();
            newStaff.Address = staff.Address;
            newStaff.Age = staff.Age;
            newStaff.Charges = staff.Charges;
            newStaff.DateOfBirth = staff.DateOfBirth;
            newStaff.DateOfJoining = staff.DateOfJoining;
            newStaff.Email = staff.Email;
            newStaff.PaymentType = staff.PaymentType;
            newStaff.Phone = staff.Phone;
            newStaff.Qualification = staff.Qualification;
            newStaff.Speciality = staff.Speciality;

            if (ModelState.IsValid)
            {
                db.Entry(newStaff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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

        private void FillDepartmentDropDown(StaffViewModel staffViewModel)
        {
            List<SelectListItem> stateList = (from s in db.Departments.AsEnumerable()
                                              select new SelectListItem
                                              {
                                                  Text = $"{s.Name}",
                                                  Value = s.ID.ToString()
                                              }).ToList();
            stateList.Insert(0, new SelectListItem() { Text = "--Select Department--", Value = "" });
            staffViewModel.DepartmentList = stateList;
        }
    }
}
