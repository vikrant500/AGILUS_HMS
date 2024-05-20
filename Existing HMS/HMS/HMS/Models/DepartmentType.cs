using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class DepartmentType
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}