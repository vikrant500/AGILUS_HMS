using HMS.Infrastructure;
using HMS.Models;
using HMS.VeiwModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using System.Data.Entity;

namespace HMS.Controllers
{
    [Authorize(Roles = "Administrators,Customer")]
    public class ReportsController : Controller
    {
        private readonly AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Reports

        [Authorize(Roles = "Customer")]
        public ActionResult List()
        {
            string userName = User.Identity.Name;
            return View((from patient in db.Patients
                         join lt in db.LabTests on patient.ID equals lt.Patient.ID
                         join pr in db.PatientReports on lt.ID equals pr.LabTest.ID
                         where patient.CallingNumber == userName
                         select
                         new CustomerReportsListVm
                         {
                             FirstName = patient.FirstName,
                             LastName=patient.LastName,
                             TestDate = lt.TestDate,
                             TestName = lt.TestName,
                             FileName = pr.ReportName,
                             ReportID = pr.ID,
                             Downloaded=pr.Downloaded,
                             Status=pr.Status
                         }).Distinct().OrderByDescending(sort => sort.TestDate).ToList());
        }

        [HttpPost]
        public void UpdateStatus(long ID)
        {
            PatientReport patientReport = db.PatientReports.Where(x => x.ID == ID).FirstOrDefault();
            patientReport.Downloaded = true;
            patientReport.DateDownload = DateTime.Now;
            db.Entry(patientReport).State = EntityState.Modified;
            db.SaveChanges();
        }

        
    }
}