using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class LoyalityProcedure
    {
        public long ID { get; set; }
        
        public virtual Procedure Procedure { get; set; }
        public virtual Loyality Loyality { get; set; }
    }
}