using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Models
{
    public class Appointment
    {
        public long ID { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Department Department { get; set; }
        public virtual Doctor Doctor { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointType { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDatetime { get; set; }
    }

    public class ApppointmentViewModel
    {
        public long ID { get; set; }
        public int PatientID { get; set; }
        public List<SelectListItem> Patients { get; set; }
        public int DepartmentID { get; set; }
        public List<SelectListItem> Departments { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentType { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }

    }

    public class AppointmentsDataVm
    {
        public string title { get; set; }
        public string start { get; set; }
        public string url { get; set; }
    }
}