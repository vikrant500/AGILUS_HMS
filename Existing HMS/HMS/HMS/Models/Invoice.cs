using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Models
{

    public class Invoice
    {
        public long ID { get; set; }
        public string InvoiceDisplayID { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Department Department { get; set; }
        public string PaymentMode { get; set; }
        public string Sponsor { get; set; }
        public string PresDoctor { get; set; }
        public string RefBy { get; set; }
        public string LabNo { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        public virtual ICollection<InvoiceTransactions> InvoiceTransactions { get; set; }
    }

    public class InvoiceVm
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="Patient Details Are Required")]
        [RegularExpression(@"^([1-9].*)$",
         ErrorMessage = "Patient Details are Required")]
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int DepartmentID { get; set; }
        public string PaymentMode { get; set; }
        public string Sponsor { get; set; }
        [Required(ErrorMessage ="Prescribed Doctor Is Required")]
        public string PresDoctor { get; set; }
        [Required(ErrorMessage = "Department Feild Is Required")]
        public string Department { get; set; }
        [Required(ErrorMessage ="Patient Reference Feild Is Required")]
        public string RefBy { get; set; }
        public string LabNo { get; set; }
        public string Description { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public IEnumerable<SelectListItem> ReferalsList { get; set; }
    }
}