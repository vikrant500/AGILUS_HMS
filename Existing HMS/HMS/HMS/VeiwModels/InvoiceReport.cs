using HMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HMS.VeiwModels
{
    public class InvoiceReport
    {
        public long ID { get; set; }
        public string InvoiceDisplayId { get; set; }
        public DateTime DateCreated { get; set; }
        [DisplayName("UHID")]
        public long PatientId { get; set; }
        [DisplayName("Department")]
        public string DepartmentName { get; set; }
        [DisplayName("Name")]
        public string PatientName { get; set; }
        public string Doctor { get; set; }
        [DisplayName("Treatment")]
        public string ProcedureString { get; set; }
        [DisplayName("Payment")]
        public string PaymentString { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Paid")]
        public decimal PaidAmount { get; set; }
        [DisplayName("Pending")]
        public decimal PendingAmount { get; set; }
        [DisplayName("Cost")]
        public decimal TotalCost { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        public virtual ICollection<InvoiceTransactions> InvoiceTransactions { get; set; }
    }
}