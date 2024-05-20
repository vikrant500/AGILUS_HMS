using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class Biometric
    {
        public long ID { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeIn  { get; set; }
        public DateTime TimeOut { get; set; }
        public string Attendance { get; set; }
        public int Overtime { get; set; }
        public string Center { get; set; }
        public int WorkingHour { get; set; }
    }
}