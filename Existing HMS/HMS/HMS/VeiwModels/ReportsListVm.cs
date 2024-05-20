using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.VeiwModels
{
    public class ReportsListVm
    {
        public string PatientName { get; set; }
        public string MobileNumber { get; set; }
        public DateTime LastAppointmentDateTime { get; set; }
        public bool DoesUserExist { get; set; }
    }
}