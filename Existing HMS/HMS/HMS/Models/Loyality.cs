using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class Loyality
    {
        public long ID { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<LoyalityDependents>LoyalityDependents { get; set; }
        public virtual ICollection<LoyalityProcedure> Procedures { get; set; }
    }
}