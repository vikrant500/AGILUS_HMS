using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class StaffSalary
    {
        public long ID { get; set; }
        public virtual Staff Staff { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDate { get; set; }
    }
}