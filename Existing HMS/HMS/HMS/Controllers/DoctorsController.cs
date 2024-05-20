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
    public class DoctorsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Doctors
        public ActionResult Index()
        {
            return View(db.Doctors.ToList());
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            DoctorViewModel doctorViewModel = new DoctorViewModel();
            FillDepartmentDropDown(doctorViewModel);
            return View(doctorViewModel);
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create(DoctorViewModel doctor)
        {
            Doctor newDoctor = new Doctor();
            newDoctor.Name = doctor.DoctorName;
            newDoctor.Department = db.Departments.Where(x => x.ID == doctor.DepartmentID).FirstOrDefault();
            newDoctor.Address = doctor.Address;
            newDoctor.Age = doctor.Age;
            newDoctor.Charges = doctor.Charges;
            newDoctor.DateOfBirth = doctor.DateOfBirth;
            newDoctor.DateOfJoining = doctor.DateOfJoining;
            newDoctor.Email = doctor.Email;
            newDoctor.PaymentType = doctor.PaymentType;
            newDoctor.Phone = doctor.Phone;
            newDoctor.Qualification = doctor.Qualification;
            newDoctor.Speciality = doctor.Speciality;
            if (ModelState.IsValid)
            {
                db.Doctors.Add(newDoctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            DoctorViewModel newDoctor = new DoctorViewModel();
            newDoctor.ID = doctor.ID;
            newDoctor.DoctorName = doctor.Name;
            newDoctor.DepartmentID = (int)doctor.Department.ID;
            newDoctor.Address = doctor.Address;
            newDoctor.Age = doctor.Age;
            newDoctor.Charges = doctor.Charges;
            newDoctor.DateOfBirth = doctor.DateOfBirth;
            newDoctor.DateOfJoining = doctor.DateOfJoining;
            newDoctor.Email = doctor.Email;
            newDoctor.PaymentType = doctor.PaymentType;
            newDoctor.Phone = doctor.Phone;
            newDoctor.Qualification = doctor.Qualification;
            newDoctor.Speciality = doctor.Speciality;
            if (doctor == null)
            {
                return HttpNotFound();
            }
            FillDepartmentDropDown(newDoctor);
            return View(newDoctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit(DoctorViewModel doctor)
        {
            Doctor newDoctor = db.Doctors.Find(doctor.ID);
            newDoctor.Name = doctor.DoctorName;
            newDoctor.Department = db.Departments.Where(x => x.ID == doctor.DepartmentID).FirstOrDefault();
            newDoctor.Address = doctor.Address;
            newDoctor.Age = doctor.Age;
            newDoctor.Charges = doctor.Charges;
            newDoctor.DateOfBirth = doctor.DateOfBirth;
            newDoctor.DateOfJoining = doctor.DateOfJoining;
            newDoctor.Email = doctor.Email;
            newDoctor.PaymentType = doctor.PaymentType;
            newDoctor.Phone = doctor.Phone;
            newDoctor.Qualification = doctor.Qualification;
            newDoctor.Speciality = doctor.Speciality;
            
            if (ModelState.IsValid)
            {
                db.Entry(newDoctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
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

        private void FillDepartmentDropDown(DoctorViewModel doctorViewModel)
        {
            List<SelectListItem> stateList = (from s in db.Departments.AsEnumerable()
                                              select new SelectListItem
                                              {
                                                  Text = $"{s.Name}",
                                                  Value = s.ID.ToString()
                                              }).ToList();
            stateList.Insert(0, new SelectListItem() { Text = "--Select Department--", Value = "" });
            doctorViewModel.DepartmentList = stateList;
        }
    }
}
