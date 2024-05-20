using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class LoyalityDependents
    {
        public long ID { get; set; }
        public virtual Loyality Loyality { get; set; }
        public string Name { get; set; }
        public string Relation { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
    }
}