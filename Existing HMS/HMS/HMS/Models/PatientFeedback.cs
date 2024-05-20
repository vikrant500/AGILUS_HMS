using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class PatientFeedback
    {
        public long ID { get; set; }
        public string   PatientName { get; set; }
        public string PatientAddress { get; set; }
        public string ConsultedDoctor { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Receptionist_Behaviour { get; set; }
        public int WaitingTime { get; set; }
        public int Hygiene { get; set; }
        public int ExperienceWithDoc { get; set; }
        public int Billing { get; set; }
        public string SujjestionsToImprove { get; set; }
        public DateTime CreatedDateTime { get; set; }
        
    }
}