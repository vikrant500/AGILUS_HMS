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
using PagedList;

namespace HMS.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class PatientsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        private SendMessage sendMessage = new SendMessage();

        // GET: Patients
        public ActionResult Index()
        {

            List<PatientListModel> patientListViewModels = new List<PatientListModel>();
            patientListViewModels = (from patient in db.Patients
                                     where patient.Status.ToLower() == "active"
                                     select
                                     new PatientListModel
                                     {
                                         ID = patient.ID,
                                         FirstName = patient.FirstName,
                                         LastName = patient.LastName,
                                         MiddleName=patient.MiddleName,
                                         Age = patient.Age,
                                         CallingNumber = patient.CallingNumber,
                                         Address = patient.Address,
                                         RegisteredOn=patient.DateCreated
                                     }).OrderByDescending(x => x.RegisteredOn).ToList();

            //int pageSize = 10;
            //int pageNumber = (page ?? 1);
            //return View(patientListViewModels.ToPagedList(pageNumber, pageSize));
            return View(patientListViewModels);
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            PatientViewModel patientViewModel = new PatientViewModel();
            FillCentersDropDown(patientViewModel);
            return View(patientViewModel);
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        
        public ActionResult Create(PatientViewModel patient)
        {
            patient.DateCreated = DateTime.Now;
            patient.LastModified = DateTime.Now;
            patient.Status = "Active";
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                var smsmsg = "";
                if (db.Messages.Where(x => x.Name == "RegistrationMessage").FirstOrDefault() != null)
                { smsmsg = db.Messages.Where(x => x.Name == "RegistrationMessage").FirstOrDefault().MessageText.Replace("[patientname]", patient.Salutation +" "+ patient.FirstName).Replace("[UHID]",patient.ID.ToString()); }
                else { smsmsg = "Dear Patient thank you for registering with Sharanya Health Care"; }

                sendMessage.SMS(patient.CallingNumber,smsmsg);
                return RedirectToAction("Index");
            }
              FillCentersDropDown(patient);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            PatientViewModel patientViewModel = new PatientViewModel { 
            AadhaarID=patient.AadhaarID,
            Address=patient.Address,
            Age=patient.Age,
            CallingNumber=patient.CallingNumber,
            CareofGuardian=patient.CareofGuardian,
            CenterID=patient.CenterID,
            City=patient.City,
            DateOfBirth=patient.DateOfBirth,
            EmailID=patient.EmailID,
            FirstName=patient.FirstName,
            Gender=patient.Gender,
            GuardianName=patient.GuardianName,
            ID=patient.ID,
            LastName=patient.LastName,
            MarialStatus=patient.MarialStatus,
            MiddleName=patient.MiddleName,
            Occupation=patient.Occupation,
            PatientReference=patient.PatientReference,
            PinCode=patient.PinCode,
            Salutation=patient.Salutation,
            Status=patient.Status,
            WhatsappNumber=patient.WhatsappNumber,
            DateCreated=patient.DateCreated
            };
            
            FillCentersDropDown(patientViewModel);
            
            return View(patientViewModel);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(PatientViewModel patientvm)
        {
            Patient patient = new Patient
            {
                AadhaarID=patientvm.AadhaarID,
                Address=patientvm.Address,
                Age=patientvm.Age,
                CallingNumber=patientvm.CallingNumber,
                CareofGuardian=patientvm.CareofGuardian,
                CenterID=patientvm.CenterID,
                City=patientvm.City,
                DateOfBirth=patientvm.DateOfBirth,
                EmailID=patientvm.EmailID,
                FirstName=patientvm.FirstName,
                Gender=patientvm.Gender,
                LastModified=DateTime.Now,
                GuardianName=patientvm.GuardianName,
                LastName=patientvm.LastName,
                MarialStatus=patientvm.MarialStatus,
                MiddleName=patientvm.MiddleName,
                Occupation=patientvm.Occupation,
                PatientReference=patientvm.PatientReference,
                PinCode=patientvm.PinCode,
                Salutation=patientvm.Salutation,
                Status=patientvm.Status,
                WhatsappNumber=patientvm.WhatsappNumber,
                ID=patientvm.ID,
                DateCreated=patientvm.DateCreated
            };
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            FillCentersDropDown(patientvm);
            return View(patientvm);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            patient.Status = "Inactive";
            db.Entry(patient).State = EntityState.Modified;
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


        private void FillCentersDropDown(PatientViewModel patientViewModel)
        {
            List<SelectListItem> centersList = (from c in db.Centers.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = $"{c.Name}",
                                                    Value = c.ID.ToString()
                                                }).ToList();
            centersList.Insert(0, new SelectListItem() { Text = "--Select Center--", Value = "" });
            List<SelectListItem> referalsList = (from c in db.Referals.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = $"{c.Name}",
                                                    Value = c.Name.ToString()
                                                }).ToList();
            referalsList.Insert(0, new SelectListItem() { Text = "--Select Referal--", Value = "" });
            patientViewModel.CentersList = centersList;
            patientViewModel.ReferencesList = referalsList;

        }
    }
}
