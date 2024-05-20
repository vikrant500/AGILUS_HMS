using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Models;

namespace HMS.VeiwModels
{
    public class Dashboard
    {
        public long  ID { get; set; }
        public int NumOfDoctors { get; set; }
        public int NumOfPatients { get; set; }
        public int AppointmentsAttended { get; set; }
        public int AppointmentsPending { get; set; }
        public virtual ICollection<Appointment> UpcommingAppointments { get; set; }
        public virtual ICollection<Patient> NewPatients { get; set;  }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}