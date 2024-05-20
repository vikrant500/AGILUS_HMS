using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.VeiwModels
{
    public class ProfitLoss
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Expence { get; set; }
        public decimal Revenue { get; set; }
        public decimal DoctorSalary { get; set; }

        public decimal StaffSalary { get; set; }
        public decimal ProfitOrLoss { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

    }
    public class RevenueDetails
    { 
        public long InvoiceId { get; set; }
        public string PatientName { get; set; }
        public int NumOfProcedures { get; set; }
        public decimal TotalCost { get; set; }
    
    
    }
    public class ExpenceDetails
    { 
        public long ID { get; set; }
        public string ItemName { get; set; }
        public string ModeOfPayment { get; set; }
        public decimal CostPerItem { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
    
    }
}