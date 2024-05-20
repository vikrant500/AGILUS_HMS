using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class LabTest
    {
        public long ID { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Department Department { get; set; }
        public DateTime TestDate { get; set; }
        public string TestName { get; set; }
        public string Comment { get; set; }
        public virtual ICollection<PatientReport> PatientReports { get; set; }
    }
}