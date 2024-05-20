using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class DoctorSchedule
    {
        public long ID { get; set; }
        public virtual Doctor Doctor { get; set; }
        public List<string> Days { get; set; }
        public string DaysAvailable { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}