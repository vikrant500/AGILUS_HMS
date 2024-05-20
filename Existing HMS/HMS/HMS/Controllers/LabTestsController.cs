using HMS.Infrastructure;
using HMS.Models;
using HMS.VeiwModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class LabTestsController : Controller
    {
        private readonly AppIdentityDbContext db = new AppIdentityDbContext();
        private SendMessage sendMessage = new SendMessage();
        // GET: LabTests
        public ActionResult Index()
        {
            return View((from patient in db.Patients
                         join lt in db.LabTests on patient.ID equals lt.Patient.ID
                         join pr in db.PatientReports on lt.ID equals pr.LabTest.ID into jts
                         from jtr in jts.DefaultIfEmpty()
                         select
                         new LabTestListVm
                         {
                             FirstName = patient.FirstName,
                             LastName = patient.LastName,
                             MobileNumber = patient.CallingNumber,
                             TestDate = lt.TestDate,
                             TestID = lt.ID,
                             Testname = lt.TestName,
                             Downloaded = jtr == null ? (bool?)null : jtr.Downloaded,
                             ReportUploadDate = jtr == null ? null : jtr.DateUploaded,
                             ReportDownloadDate = jtr == null ? null : jtr.DateDownload
                         }).Distinct().OrderByDescending(sort => sort.TestDate).ToList());
        }

        public ActionResult Add()
        {
            LabTestVm model = new LabTestVm();
            model.LabsList = GetDepartmentList();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(LabTestVm model)
        {
            LabTest labTest = new LabTest();
            labTest.Department = db.Departments.Where(x => x.ID == model.DepartmentID).FirstOrDefault();
            Patient patient = db.Patients.Where(x => x.ID == model.PatientID).FirstOrDefault();
            labTest.Patient = patient;
            labTest.TestDate = model.TestDate;
            labTest.TestName = model.TestName;
            labTest.Comment = model.Comment;

            db.LabTests.Add(labTest);
            model.LabsList = GetDepartmentList();
            string customerMobileNumber = patient.CallingNumber;
            string firstTwoChars = $"{patient.FirstName[0].ToString().ToUpper()}{patient.FirstName[1].ToString().ToLower()}";

            if (UserManager.FindByName(patient.CallingNumber) == null)
            {
                AppUser user = new AppUser { UserName = customerMobileNumber, Email = $"{customerMobileNumber}@gmail.com" };
                IdentityResult result = await UserManager.CreateAsync(user, $"{firstTwoChars}{customerMobileNumber}");
                if (result.Succeeded)
                {
                    AppUser userInfo = UserManager.FindByName(customerMobileNumber);
                    await UserManager.AddToRoleAsync(userInfo.Id, "Customer");
                    UserPatientMapping userPatientMapping = new UserPatientMapping();
                    userPatientMapping.Patient = patient;
                    userPatientMapping.UserID = userInfo.Id;
                    db.UserPatientMappings.Add(userPatientMapping);
                    db.SaveChanges();
                    var smsmsg = "";
                    Message smsMessage = db.Messages.Where(x => x.Name == "TestCollected").FirstOrDefault();
                    if (smsMessage != null)
                    {
                        smsmsg = smsMessage.MessageText.Replace("[patientname]", $"{patient.Salutation} {patient.FirstName} {patient.LastName}").Replace("[testname]", model.TestName).Replace("[labname]", labTest.Department.Name).Replace("[upassword]", $"{firstTwoChars}{customerMobileNumber}");
                        sendMessage.SMS(patient.CallingNumber, smsmsg);
                    }
                }
            }
            else
            {
                db.SaveChanges();
                AppUser userInfo = UserManager.FindByName(customerMobileNumber);
                UserPatientMapping userPatientMapping = db.UserPatientMappings.Where(x => x.UserID == userInfo.Id).FirstOrDefault();
                Patient patientInfo = db.Patients.Where(x => x.ID == userPatientMapping.Patient.ID).FirstOrDefault();
                string firstTwoCharsExistingCustomer = $"{patientInfo.FirstName[0].ToString().ToUpper()}{patientInfo.FirstName[1].ToString().ToLower()}";

                var smsmsg = "";
                Message smsMessage = db.Messages.Where(x => x.Name == "TestCollected").FirstOrDefault();
                if (smsMessage != null)
                {
                    smsmsg = smsMessage.MessageText.Replace("[patientname]", $"{patient.Salutation} {patient.FirstName}").Replace("[testname]", model.TestName).Replace("[labname]", labTest.Department.Name).Replace("[upassword]", $"{firstTwoCharsExistingCustomer}{customerMobileNumber}");
                    sendMessage.SMS(patient.CallingNumber, smsmsg);
                }
            }
            return RedirectToAction("Index");
        }



        [Authorize(Roles = "Administrators")]
        public ActionResult AddReport(long TestID)
        {
            TestReports model = new TestReports();
            LabTest labTest = db.LabTests.Where(x => x.ID == TestID).FirstOrDefault();
            Patient patient = db.Patients.Where(x => x.ID == labTest.Patient.ID).FirstOrDefault();
            model.MobileNumber = patient.CallingNumber;
            model.FirstName = $"{patient.Salutation} {patient.FirstName}";
            model.TestName = labTest.TestName;
            model.TestDate = labTest.TestDate;
            model.Files = db.PatientReports.Where(x => x.Patient.ID == patient.ID && x.LabTest.ID == TestID).Select(x => new { x.ID, x.ReportName }).ToDictionary(x => x.ID, y => y.ReportName);
            model.Statuses = GetStatusList();
            return View(model);
        }

        [Authorize(Roles = "Administrators")]
        [HttpPost]
        public ActionResult AddReport(TestReports model)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    AppUser userInfo = UserManager.FindByName(model.MobileNumber);
                    UserPatientMapping userPatientMapping = db.UserPatientMappings.Where(x => x.UserID == userInfo.Id).FirstOrDefault();
                    HttpPostedFileBase file = model.File;
                    string fileName = $"{model.MobileNumber}-{DateTime.Now.ToString("yyyyMMddhhmmss")}.pdf";
                    string path = Path.Combine(Server.MapPath(@"/TestReportsPDFs/"), fileName);
                    file.SaveAs(path);

                    Patient parentPatient = db.Patients.Where(x => x.ID == userPatientMapping.Patient.ID).FirstOrDefault();
                    LabTest labTest = db.LabTests.Where(x => x.ID == model.TestID).FirstOrDefault();

                    Patient patient = db.Patients.Where(x => x.ID == labTest.Patient.ID).FirstOrDefault();
                    PatientReport patientReport = new PatientReport();
                    patientReport.Patient = patient;
                    patientReport.LabTest = labTest;
                    patientReport.ReportName = fileName;
                    patientReport.Status = model.Status;
                    patientReport.DateUploaded = DateTime.Now;
                    string customerMobileNumber = patient.CallingNumber;
                    string firstTwoChars = $"{parentPatient.FirstName[0].ToString().ToUpper()}{parentPatient.FirstName[1].ToString().ToLower()}";

                    db.PatientReports.Add(patientReport);
                    db.SaveChanges();
                    var smsmsg = "";
                    Message smsMessage = db.Messages.Where(x => x.Name == "ReportUploaded").FirstOrDefault();
                    if (smsMessage != null)
                    {
                        smsmsg = smsMessage.MessageText.Replace("[patientname]", $"{model.FirstName}").Replace("[upassword]", $"{firstTwoChars}{customerMobileNumber}");
                        sendMessage.SMS(patient.CallingNumber, smsmsg);
                    }
                    scope.Complete();
                    return RedirectToAction("Index");
                }
                catch
                {
                    scope.Dispose();
                }

            }
            return RedirectToAction("AddReport", new { TestID = model.TestID });
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int Id)
        {
            PatientReport patientReport = new PatientReport();
            long testID = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {

                    patientReport = db.PatientReports.Where(x => x.ID == Id).FirstOrDefault();
                    testID = patientReport.LabTest.ID;
                    db.PatientReports.Remove(patientReport);
                    db.SaveChanges();

                    string path = Path.Combine(Server.MapPath(@"/TestReportsPDFs/"), patientReport.ReportName);
                    FileInfo fi = new FileInfo(path);
                    fi.Delete();
                    scope.Complete();
                }
                catch
                {
                    scope.Dispose();
                }
            }
            return RedirectToAction("AddReport", new { TestID = testID });
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult DeleteTest(long Id)
        {
            List<PatientReport> patientReports = new List<PatientReport>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    patientReports = db.PatientReports.Where(x => x.LabTest.ID == Id).ToList();
                    foreach (PatientReport item in patientReports)
                    {
                        string path = Path.Combine(Server.MapPath(@"/TestReportsPDFs/"), item.ReportName);
                        FileInfo fi = new FileInfo(path);
                        fi.Delete();
                    }

                    db.PatientReports.RemoveRange(patientReports);
                    db.LabTests.Remove(db.LabTests.Where(x => x.ID == Id).FirstOrDefault());
                    db.SaveChanges();
                    scope.Complete();
                }
                catch
                {
                    scope.Dispose();
                }
            }
            return RedirectToAction("Index");
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private IEnumerable<SelectListItem> GetDepartmentList()
        {
            var Departments = (from s in db.Departments
                               where s.DepartmentType.Type.ToLower() == "lab"
                               select new
                               {
                                   Text = s.Name,
                                   Value = s.ID
                               }).ToList();
            List<SelectListItem> DepartmentsList = new List<SelectListItem>();
            foreach (var item in Departments)
            {
                DepartmentsList.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            DepartmentsList.Insert(0, new SelectListItem() { Text = "-Select Department-", Value = "" });
            return DepartmentsList;
        }

        private IEnumerable<SelectListItem> GetStatusList()
        {

            List<SelectListItem> StatusList = new List<SelectListItem>();
            StatusList.Add(new SelectListItem { Text = "Partial", Value = "Partial" });
            StatusList.Add(new SelectListItem { Text = "Complete", Value = "Complete" });


            StatusList.Insert(0, new SelectListItem() { Text = "-Select Status-", Value = "" });
            return StatusList;
        }

    }
}