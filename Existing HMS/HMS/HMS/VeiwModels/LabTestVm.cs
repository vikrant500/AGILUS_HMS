using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.VeiwModels
{
    public class LabTestVm
    {
        public long PatientID { get; set; }
        public long DepartmentID { get; set; }
        public DateTime TestDate { get; set; }
        public string TestName { get; set; }
        public string Comment { get; set; }
        public IEnumerable<SelectListItem> LabsList { get; set; }
    }
}