using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace HMS.Models
{
    public class Procedure
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }

    public class ProcedureVm
    {
        public List<SelectListItem> DepartmentList { get; set; }
        public long DepartmentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class ProceduresListVm
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}