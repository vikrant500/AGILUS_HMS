using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class PatientReport
    {
        public long ID { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual LabTest LabTest { get; set; }
        public string ReportName { get; set; }
        public string Status { get; set; }
        public DateTime? DateUploaded { get; set; }
        public bool Downloaded { get; set; }
        public DateTime? DateDownload { get; set; }
    }
}