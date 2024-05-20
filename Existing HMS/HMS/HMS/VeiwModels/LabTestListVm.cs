using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.VeiwModels
{
    public class LabTestListVm
    {

        private string _firstName;

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public long TestID { get; set; }

        public string Testname { get; set; }
        public DateTime TestDate { get; set; }
        public string MobileNumber { get; set; }
        public bool? Downloaded { get; set; }
        public DateTime? ReportUploadDate { get; set; }
        public DateTime? ReportDownloadDate { get; set; }
    }
}