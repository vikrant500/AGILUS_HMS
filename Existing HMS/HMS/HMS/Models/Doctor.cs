using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Models
{
    public class Doctor
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public virtual Department Department { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PaymentType { get; set; }
        public long Charges { get; set; }
        public string Speciality { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public DateTime DateOfJoining { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }

    public class DoctorViewModel
    {
        public long ID { get; set; }
        public string DoctorName { get; set; }
        [Required]
        public int DepartmentID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PaymentType { get; set; }
        public long Charges { get; set; }
        public string Speciality { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public DateTime DateOfJoining { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
    }
}