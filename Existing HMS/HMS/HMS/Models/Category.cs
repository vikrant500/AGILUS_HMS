using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class Category
    {
        //public Category()
        //{
        //    Inventories = new HashSet<Inventory>();
        //}
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}