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
    public class AppointmentsController : Controller
    {
        private readonly AppIdentityDbContext db = new AppIdentityDbContext();
        private SendMessage sendMessage = new SendMessage();    

        // GET: Appointments
        public ActionResult Index()
        {
            return View(db.Appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApppointmentViewModel model = new ApppointmentViewModel();
            Appointment appointment = db.Appointments.Find(id);
            model.AppointmentDate = appointment.AppointmentDate;
            model.AppointmentType = appointment.AppointType;
            model.ID = appointment.ID;
            model.DepartmentID = (int)appointment.Department.ID;
            model.Message = appointment.Message;
            model.PatientID = (int)appointment.Patient.ID;
            model.DoctorID = (int)appointment.Doctor.ID;
            model.Status = appointment.Status;
            if (appointment == null)
            {
                return HttpNotFound();
            }
            FillDropDowns(model);
            return View(model);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ApppointmentViewModel model = new ApppointmentViewModel();
            FillDropDowns(model);
            return View(model);
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create(ApppointmentViewModel appointmentvm)
        {
            Appointment appointment = new Appointment();
            appointment.AppointmentDate = appointmentvm.AppointmentDate;
            appointment.AppointType = appointmentvm.AppointmentType;
            appointment.CreatedDateTime = appointment.LastModifiedDatetime = DateTime.Now;
            appointment.Department = db.Departments.Where(x => x.ID == appointmentvm.DepartmentID).FirstOrDefault();
            appointment.Message = appointmentvm.Message;
            appointment.Patient = db.Patients.Where(x => x.ID == appointmentvm.PatientID).FirstOrDefault();
            appointment.Doctor = db.Doctors.Where(x => x.ID == appointmentvm.DoctorID).FirstOrDefault();
            appointment.Status = "Active";

            if (ModelState.IsValid)
            {
                var smsmsg = "";
                db.Appointments.Add(appointment);
                if (db.Messages.Where(x => x.Name == "AppointmentMessage").FirstOrDefault() != null)
                {
                    smsmsg = db.Messages.Where(x => x.Name == "AppointmentMessage").FirstOrDefault().MessageText.Replace("[patientname]", appointment.Patient.Salutation +" "+ appointment.Patient.FirstName).Replace("[doctorname]", appointment.Doctor.Name +" " +"of" ).Replace("[departmentname]", appointment.Department.Name+ " "+ "department").Replace("[date]", appointment.AppointmentDate.ToString("dd-MM-yyyy HH:mm"));
                }
                else
                {
                    smsmsg = "Dear Patient your appointment at Sharanya Health Care is placed Sucessfully";
                }
                sendMessage.SMS(appointment.Patient.CallingNumber, smsmsg);

                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            FillDropDowns(appointmentvm);
            return View(appointmentvm);
        }
        

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApppointmentViewModel model = new ApppointmentViewModel();
            Appointment appointment = db.Appointments.Find(id);
            model.AppointmentDate = appointment.AppointmentDate;
            model.AppointmentType = appointment.AppointType;
            model.ID = appointment.ID;
            model.DepartmentID = (int)appointment.Department.ID;
            model.Message = appointment.Message;
            model.PatientID = (int)appointment.Patient.ID;
            model.DoctorID = (int)appointment.Doctor.ID;
            model.Status = "Active";
            if (appointment == null)
            {
                return HttpNotFound();
            }
            FillDropDowns(model);
            return View(model);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit(ApppointmentViewModel appointmentvm)
        {
            Appointment appointment = db.Appointments.Find(appointmentvm.ID);
            appointment.AppointmentDate = appointmentvm.AppointmentDate;
            appointment.AppointType = appointmentvm.AppointmentType;
            appointment.LastModifiedDatetime = DateTime.Now;
            appointment.Department = db.Departments.Where(x => x.ID == appointmentvm.DepartmentID).FirstOrDefault();
            appointment.Message = appointmentvm.Message;
            appointment.Patient = db.Patients.Where(x => x.ID == appointmentvm.PatientID).FirstOrDefault();
            appointment.Doctor = db.Doctors.Where(x => x.ID == appointmentvm.DoctorID).FirstOrDefault();
            appointment.Status = "Active";
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointmentvm);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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


        private void FillDropDowns(ApppointmentViewModel ApppointmentViewModel)
        {
            List<SelectListItem> PatientList = (from s in db.Patients.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = $"{s.FirstName} {s.LastName}",
                                                    Value = s.ID.ToString()
                                                }).ToList();
            PatientList.Insert(0, new SelectListItem() { Text = "-Select Patient-", Value = "" });
            ApppointmentViewModel.Patients = PatientList;

            List<SelectListItem> DepartmentsList = (from s in db.Departments.AsEnumerable()
                                                    select new SelectListItem
                                                    {
                                                        Text = $"{s.Name}",
                                                        Value = s.ID.ToString()
                                                    }).ToList();
            DepartmentsList.Insert(0, new SelectListItem() { Text = "-Select Department-", Value = "" });
            ApppointmentViewModel.Departments = DepartmentsList;
        }


        public JsonResult GetPatientData(int ID)
        {
            PatientSearchVm patientSearchVm = new PatientSearchVm();
            var search = db.Patients.Where(x => x.ID == ID).FirstOrDefault();
            patientSearchVm = new PatientSearchVm
            {
                PatientPhone = search.CallingNumber,
                PatientEmail = search.EmailID
            };
            return Json(new { data = patientSearchVm }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppointmentData(string doctor, string department,string patient,string phone)
        {

            var search = db.Appointments.ToList();
            if (doctor != "" && doctor != null)
            {
                search.RemoveAll(x => x.Doctor.Name != doctor);
            }
            if (department != "" && department!= null)
            {
                search.RemoveAll(x => x.Department.Name != department);
            }
            if (patient != "" && patient != null)
            {
                search.RemoveAll(x => !x.Patient.FirstName.Contains(patient));
            }
            if (phone != "" && phone != null)
            {
                search.RemoveAll(x => !(x.Patient.WhatsappNumber.Contains(phone) || x.Patient.CallingNumber.Contains(phone)));
            }
            AppointmentsDataVm[] appointments = new AppointmentsDataVm[search.Count];
            int i = 0;
            foreach (var item in search)
            {
                int maxLength = 4;
                if (item.Department.Name.Length < 4)
                {
                    maxLength = item.Department.Name.Length;
                }
                appointments[i] = new AppointmentsDataVm()
                {
                    
                    title = $"{item.Patient.FirstName+'-'+item.Department.Name.Substring(0,maxLength)}",
                    start = item.AppointmentDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    url = "/Appointments/Details/" + item.ID
                };
                i++;
            }

            return Json(new { data = appointments }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDoctorsList(int DeptID)
        {
            List<SelectListItem> doctorsList = (from docs in db.Doctors.AsEnumerable()
                                                where docs.Department.ID == DeptID
                                                orderby docs.Name ascending
                                                select new SelectListItem
                                                {
                                                    Text = docs.Name,
                                                    Value = docs.ID.ToString()
                                                }).ToList();
            doctorsList.Insert(0, new SelectListItem() { Text = "--Select Doctor--", Value = "" });

            //TempData["Cities"] = citiesList;
            return PartialView("_DoctorOptions", doctorsList);
        }
    }
}
