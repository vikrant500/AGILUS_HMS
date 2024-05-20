using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class UserPatientMapping
    {
        public long ID { get; set; }
        public virtual Patient Patient { get; set; }
        public string  UserID { get; set; }
    }
}