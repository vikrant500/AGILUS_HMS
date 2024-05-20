using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.VeiwModels
{
    public class CustomerReportsListVm
    {
        public long ReportID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TestName { get; set; }
        public DateTime TestDate { get; set; }
        public string FileName { get; set; }
        public bool Downloaded { get; set; }
        public string Status { get; set; }
    }
}