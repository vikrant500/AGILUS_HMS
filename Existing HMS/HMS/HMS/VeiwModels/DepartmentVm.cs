using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.VeiwModels
{
    public class DepartmentVm
    {
        public string Name { get; set; }
        public int DepartmentType { get; set; }
        public IEnumerable<SelectListItem> DepartmentTypes { get; set; }
    }
}