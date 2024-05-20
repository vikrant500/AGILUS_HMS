using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class JustDial
    {
        public long ID { get; set; }
        public string PatientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Procedure { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

    }
}