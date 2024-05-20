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
    public class PatientFeedbacksController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PatientFeedbacks
        public ActionResult Index()
        {
            return View(db.PatientFeedbacks.ToList());
        }

        // GET: PatientFeedbacks/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientFeedback patientFeedback = db.PatientFeedbacks.Find(id);
            if (patientFeedback == null)
            {
                return HttpNotFound();
            }
            return View(patientFeedback);
        }

        // GET: PatientFeedbacks/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientFeedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "ID,Reveiw,PhoneNumber,Email,PatientName,PatientAddress,ConsultedDoctor,Environment,WardFacilities,ServiceOfAttendingDoctors,Billing,EnquiryService,AdmissionProcess,DischargeProcess,SujjestionsToImprove,CreatedDateTime")] PatientFeedback patientFeedback)
        {
            if (ModelState.IsValid)
            {
                patientFeedback.CreatedDateTime = DateTime.Now;
                db.PatientFeedbacks.Add(patientFeedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientFeedback);
        }

        // GET: PatientFeedbacks/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientFeedback patientFeedback = db.PatientFeedbacks.Find(id);
            if (patientFeedback == null)
            {
                return HttpNotFound();
            }
            return View(patientFeedback);
        }

        // POST: PatientFeedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,PatientName,PatientAddress,ConsultedDoctor,Environment,WardFacilities,ServiceOfAttendingDoctors,Billing,EnquiryService,AdmissionProcess,DischargeProcess,SujjestionsToImprove,CreatedDateTime")] PatientFeedback patientFeedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientFeedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patientFeedback);
        }

        // GET: PatientFeedbacks/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientFeedback patientFeedback = db.PatientFeedbacks.Find(id);
            if (patientFeedback == null)
            {
                return HttpNotFound();
            }
            return View(patientFeedback);
        }

        // POST: PatientFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            PatientFeedback patientFeedback = db.PatientFeedbacks.Find(id);
            db.PatientFeedbacks.Remove(patientFeedback);
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
