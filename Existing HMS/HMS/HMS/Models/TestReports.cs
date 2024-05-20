using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Models
{
    public class TestReports
    {
        public long TestID { get; set; }
        public string FirstName { get; set; }
        public string MobileNumber { get; set; }
        public string TestName { get; set; }
        public DateTime TestDate { get; set; }
        public HttpPostedFileBase File { get; set; }
        public Dictionary<long, string> Files { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
        public string Status { get; set; }
    }
}