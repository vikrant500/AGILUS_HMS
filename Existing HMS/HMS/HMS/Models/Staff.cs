using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Models
{
    public class Staff
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


    }

    public class StaffViewModel
    {
        public long ID { get; set; }
        public string StaffName { get; set; }
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