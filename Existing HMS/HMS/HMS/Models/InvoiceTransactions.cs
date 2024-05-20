using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class InvoiceTransactions
    {
        public long ID { get; set; }
        public virtual Invoice Invoice { get; set; }
        public decimal Amount { get; set; }
        public string ModeOfPayment { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastModified { get; set; }
    }
}