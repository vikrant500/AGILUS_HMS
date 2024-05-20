using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class Department
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Procedure> Procedures { get; set; }
        public virtual ICollection<LabTest> LabTests { get; set; }
        public virtual DepartmentType DepartmentType { get; set; }
    }
}