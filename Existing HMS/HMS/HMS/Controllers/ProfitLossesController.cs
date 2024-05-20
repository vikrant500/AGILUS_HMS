using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Infrastructure;
using HMS.VeiwModels;

namespace HMS.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class ProfitLossesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: ProfitLosses
        public ActionResult Index(string from, string to)
        {
            DateTime? fromDate=null,toDate = null;

            if (!String.IsNullOrWhiteSpace(from))
            {
                fromDate = Convert.ToDateTime(from);
            }
            
            if (!String.IsNullOrWhiteSpace(to))
            {
                toDate = Convert.ToDateTime(to).AddDays(1);
            }
            if (fromDate != null && toDate == null)
            {
                toDate = DateTime.Today.AddDays(1);
            }
            List<ProfitLoss> listobj = new List<ProfitLoss>();
            ProfitLoss obj = new ProfitLoss();
            obj.Name = "Total Revenue - Expenses";
            if (fromDate != null && toDate!=null)
            {
                obj.Expence = (from i in db.Inventories
                               where i.DateCreated > fromDate && i.DateCreated < toDate
                               select i).AsNoTracking().ToList().Sum(x => x.Price * x.Quantity);

                obj.Revenue = (from invoiceitem in db.InvoiceItems
                               join invoice in db.Invoices on invoiceitem.Invoice.ID equals invoice.ID
                               join proc in db.Procedures on invoiceitem.Procedure.ID equals proc.ID
                               where invoice.Status.ToLower()=="paid" && invoice.DateCreated > fromDate && invoice.DateCreated < toDate
                               select ((proc.Price * (1 - (invoiceitem.Discount / 100))) * invoiceitem.Units)).ToList().Sum();
                obj.DoctorSalary = (from i in db.DoctorSalaries
                                    where i.PaidDate > fromDate && i.PaidDate < toDate
                                    select i).AsNoTracking().ToList().Sum(x => x.Amount);
                obj.StaffSalary = (from i in db.StaffSalaries
                                   where i.PaidDate > fromDate && i.PaidDate < toDate
                                   select i).AsNoTracking().ToList().Sum(x => x.Amount);
                obj.ProfitOrLoss = obj.Revenue - obj.Expence - obj.DoctorSalary - obj.StaffSalary;
                obj.From = (DateTime)fromDate;
                obj.To = (DateTime)toDate;
            }
            else {
                obj.Expence = (from i in db.Inventories
                               
                               select i).AsNoTracking().ToList().Sum(x => x.Price * x.Quantity);

                obj.Revenue = (from invoiceitem in db.InvoiceItems
                               join invoice in db.Invoices on invoiceitem.Invoice.ID equals invoice.ID
                               join proc in db.Procedures on invoiceitem.Procedure.ID equals proc.ID
                               where invoice.Status.ToLower()=="paid"
                               select ((proc.Price * (1 - (invoiceitem.Discount / 100))) * invoiceitem.Units)).ToList().Sum();
                obj.DoctorSalary = (from i in db.DoctorSalaries
                                    select i).AsNoTracking().ToList().Sum(x => x.Amount);
                obj.StaffSalary = (from i in db.StaffSalaries
                                   select i).AsNoTracking().ToList().Sum(x => x.Amount);
                obj.ProfitOrLoss = obj.Revenue - obj.Expence - obj.DoctorSalary - obj.StaffSalary;
            }
            listobj.Add(obj);
            return View(listobj);
        }

        // GET: ProfitLosses/Details/5
        public ActionResult ExpenceDetails(string from, string to)
        {
            IEnumerable<ExpenceDetails> retobj = new List<ExpenceDetails>();
            if (String.IsNullOrWhiteSpace(from) && String.IsNullOrWhiteSpace(from))
            {


                retobj = (from i in db.Inventories
                          
                          select
                          new ExpenceDetails
                          {
                              ID = i.ID,
                              ItemName = i.Name,
                              Quantity = i.Quantity,
                              CostPerItem = i.Price,
                              TotalCost = i.Price * i.Quantity,


                          }).ToList();
            }
            else if (!String.IsNullOrWhiteSpace(from) && String.IsNullOrWhiteSpace(from))
            {
                DateTime fromDate = (DateTime.ParseExact(from.Substring(0, 10), "MM/dd/yyyy", CultureInfo.InvariantCulture));
                DateTime toDate = DateTime.Today;


                retobj = (from i in db.Inventories
                          where i.DateCreated >= fromDate && i.DateCreated <= toDate
                          select
                          new ExpenceDetails
                          {
                              ID = i.ID,
                              ItemName = i.Name,
                              Quantity = i.Quantity,
                              CostPerItem = i.Price,
                              TotalCost = i.Price * i.Quantity,


                          }).ToList();
            }
            else
            {


                DateTime fromDate = (DateTime.ParseExact(from.Substring(0, 10), "MM/dd/yyyy", CultureInfo.InvariantCulture));
                DateTime toDate = Convert.ToDateTime(DateTime.ParseExact(to.Substring(0, 10), "MM/dd/yyyy", CultureInfo.InvariantCulture));

                
                retobj = (from i in db.Inventories
                          where i.DateCreated >= fromDate && i.DateCreated <= toDate
                          select
                          new ExpenceDetails
                          {
                              ID = i.ID,
                              ItemName = i.Name,
                              Quantity = i.Quantity,
                              CostPerItem = i.Price,
                              TotalCost = i.Price * i.Quantity,


                          }).ToList();
            }
            return View(retobj);
        }
        public ActionResult DayExpenceDetails(string date)
        {
            List<ExpenceDetails> retobj = new List<ExpenceDetails>();
            if (String.IsNullOrWhiteSpace(date))
            {


                retobj = (from i in db.Inventories
                          where i.DateCreated.Year==DateTime.Today.Year && i.DateCreated.Month == DateTime.Today.Month && i.DateCreated.Day == DateTime.Today.Day
                          select
                          new ExpenceDetails
                          {
                              ID = i.ID,
                              ItemName = i.Name,
                              Quantity = i.Quantity,
                              CostPerItem = i.Price,
                              TotalCost = i.Price * i.Quantity,
                              ModeOfPayment=i.ModeOfPayment

                          }).ToList();
            }
            
            else
            {

                DateTime queryDate = Convert.ToDateTime(date);
                //DateTime fromDate = (DateTime.ParseExact(date.Substring(0, 10), "MM/dd/yyyy", CultureInfo.InvariantCulture));
                //DateTime toDate = Convert.ToDateTime(DateTime.ParseExact(to.Substring(0, 10), "MM/dd/yyyy", CultureInfo.InvariantCulture));


                var list = (db.Inventories.Where(x => x.DateCreated.Year == queryDate.Year && x.DateCreated.Month == queryDate.Month&& x.DateCreated.Day == queryDate.Day).ToList());
                foreach (var item in list)
                {
                    retobj.Add(  new  ExpenceDetails
                    {
                        ID = item.ID,
                        ItemName = item.Name,
                        Quantity = item.Quantity,
                        CostPerItem = item.Price,
                        TotalCost = item.Price * item.Quantity,
                        ModeOfPayment = item.ModeOfPayment


                    });
                }

                          
            }
            return View(retobj);
        }

        // GET: ProfitLosses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfitLosses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "ID,Name,Expence,Revenue,ProfitOrLoss")] ProfitLoss profitLoss)
        {
            if (ModelState.IsValid)
            {
                db.ProfitLosses.Add(profitLoss);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profitLoss);
        }

        // GET: ProfitLosses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfitLoss profitLoss = db.ProfitLosses.Find(id);
            if (profitLoss == null)
            {
                return HttpNotFound();
            }
            return View(profitLoss);
        }

        // POST: ProfitLosses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,Name,Expence,Revenue,ProfitOrLoss")] ProfitLoss profitLoss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profitLoss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profitLoss);
        }

        // GET: ProfitLosses/Delete/5
        public ActionResult RevenueDetails(string from,string to)
        {
            IEnumerable<RevenueDetails> retobj = new List<RevenueDetails>();
            if (String.IsNullOrWhiteSpace(from) && String.IsNullOrWhiteSpace(from))
            {
                retobj = (from invoiceitem in db.InvoiceItems
                          join invoice in db.Invoices on invoiceitem.Invoice.ID equals invoice.ID
                          join proc in db.Procedures on invoiceitem.Procedure.ID equals proc.ID
                          where invoice.Status.ToLower() == ("paid")
                          select
                          new RevenueDetails
                          {
                              InvoiceId = invoice.ID,
                              PatientName = invoice.Patient.FirstName + " " + invoice.Patient.LastName,
                              NumOfProcedures = invoice.InvoiceItems.Count(),
                              TotalCost = ((proc.Price * (1 - (invoiceitem.Discount / 100))) * invoiceitem.Units)


                          }).ToList();
            }
            else if (!String.IsNullOrWhiteSpace(from) && String.IsNullOrWhiteSpace(from))
            {
                DateTime fromDate = (DateTime.ParseExact(from.Substring(0, 10), "MM/dd/yyyy", CultureInfo.InvariantCulture));
                DateTime toDate = DateTime.Today;

                retobj = (from invoiceitem in db.InvoiceItems
                          join invoice in db.Invoices on invoiceitem.Invoice.ID equals invoice.ID
                          join proc in db.Procedures on invoiceitem.Procedure.ID equals proc.ID
                          where invoice.Status.ToLower() == ("paid") && invoice.DateCreated > fromDate && invoice.DateCreated <= toDate
                          select
                          new RevenueDetails
                          {
                              InvoiceId = invoice.ID,
                              PatientName = invoice.Patient.FirstName + " " + invoice.Patient.LastName,
                              NumOfProcedures = invoice.InvoiceItems.Count(),
                              TotalCost = ((proc.Price * (1 - (invoiceitem.Discount / 100))) * invoiceitem.Units)


                          }).ToList();
            }
            else
            {
                DateTime fromDate = (DateTime.ParseExact(from.Substring(0, 10), "MM/dd/yyyy", CultureInfo.InvariantCulture));
                DateTime toDate = Convert.ToDateTime(DateTime.ParseExact(to.Substring(0, 10), "MM/dd/yyyy", CultureInfo.InvariantCulture));
                
                retobj = (from invoiceitem in db.InvoiceItems
                          join invoice in db.Invoices on invoiceitem.Invoice.ID equals invoice.ID
                          join proc in db.Procedures on invoiceitem.Procedure.ID equals proc.ID
                          where invoice.Status.ToLower() == ("paid") && invoice.DateCreated > fromDate && invoice.DateCreated < toDate
                          select
                          new RevenueDetails
                          {
                              InvoiceId = invoice.ID,
                              PatientName = invoice.Patient.FirstName + " " + invoice.Patient.LastName,
                              NumOfProcedures = invoice.InvoiceItems.Count(),
                              TotalCost = ((proc.Price * (1 - (invoiceitem.Discount / 100))) * invoiceitem.Units)


                          }).ToList();
            }
                      
            return View(retobj);
        }
        public ActionResult DayRevenueDetails(string date)
        {
            List<RevenueDetails> retobj = new List<RevenueDetails>();
            if (String.IsNullOrWhiteSpace(date))
            {
                DateTime queryDate = DateTime.Today;
                retobj = (from invoicetran in db.InvoiceTransactions
                          join invoice in db.Invoices on invoicetran.Invoice.ID equals invoice.ID

                          //where invoice.Status.ToLower() == ("paid") && invoice.DateCreated.Year == queryDate.Year && invoice.DateCreated.Month == queryDate.Month && invoice.DateCreated.Day == queryDate.Day
                          where invoicetran.DateCreated.Year == queryDate.Year && invoicetran.DateCreated.Month == queryDate.Month && invoicetran.DateCreated.Day == queryDate.Day

                          select
                          new RevenueDetails
                          {
                              InvoiceId = invoice.ID,
                              PatientName = invoice.Patient.FirstName + " " + invoice.Patient.LastName,
                              NumOfProcedures = invoice.InvoiceItems.Count(),
                              TotalCost = invoicetran.Amount


                          }).ToList();
            }
            else
            {
                DateTime queryDate = Convert.ToDateTime(date);

                retobj = (from invoicetran in db.InvoiceTransactions
                          join invoice in db.Invoices on invoicetran.Invoice.ID equals invoice.ID

                          //where invoice.Status.ToLower() == ("paid") && invoice.DateCreated.Year == queryDate.Year && invoice.DateCreated.Month == queryDate.Month && invoice.DateCreated.Day == queryDate.Day
                          where invoicetran.DateCreated.Year == queryDate.Year && invoicetran.DateCreated.Month == queryDate.Month && invoicetran.DateCreated.Day == queryDate.Day

                          select
                          new RevenueDetails
                          {
                              InvoiceId = invoice.ID,
                              PatientName = invoice.Patient.FirstName + " " + invoice.Patient.LastName,
                              NumOfProcedures = invoice.InvoiceItems.Count(),
                              TotalCost = invoicetran.Amount


                          }).ToList();
            }

            return View(retobj);
        }
        // POST: ProfitLosses/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            ProfitLoss profitLoss = db.ProfitLosses.Find(id);
            db.ProfitLosses.Remove(profitLoss);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
