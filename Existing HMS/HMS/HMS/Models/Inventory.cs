using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Models
{
    public class Inventory
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Descrption { get; set; }
        public string ModeOfPayment { get; set; }
        public int  Quantity { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Category Category{ get; set; }
        public virtual Staff Staff { get; set; }
        public string Option { get; set; }



    }
    public class InventoryVm
    {
        public long ID { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int StaffID { get; set; }
        public string ModeOfPayment { get; set; }
        public string Option { get; set; }
        public decimal Price { get; set; }
        public string StaffName { get; set; }
    }

    public class InventoryListVm
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string StaffName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public string ModeOfPayment { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
