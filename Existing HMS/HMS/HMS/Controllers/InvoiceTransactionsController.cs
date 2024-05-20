using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Infrastructure;
using HMS.Models;

namespace HMS.Controllers
{
    public class InvoiceTransactionsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        private SendMessage sendMessage = new SendMessage();
        // GET: InvoiceTransactions
        public ActionResult Index()
        {
            
            
            return View(db.InvoiceTransactions.ToList());
        }

        // GET: InvoiceTransactions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceTransactions invoiceTransactions = db.InvoiceTransactions.Find(id);
            if (invoiceTransactions == null)
            {
                return HttpNotFound();
            }
            return View(invoiceTransactions);
        }

        // GET: InvoiceTransactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvoiceTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "ID,Amount,ModeOfPayment,DateCreated,LastModified")] InvoiceTransactions invoiceTransactions)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceTransactions.Add(invoiceTransactions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoiceTransactions);
        }

        // GET: InvoiceTransactions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceTransactions invoiceTransactions = db.InvoiceTransactions.Find(id);
            if (invoiceTransactions == null)
            {
                return HttpNotFound();
            }
            return View(invoiceTransactions);
        }

        // POST: InvoiceTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,Amount,ModeOfPayment,DateCreated,LastModified")] InvoiceTransactions invoiceTransactions)
        {
            if (ModelState.IsValid)
            {
                var editInvoiceTransaction = db.InvoiceTransactions.Find(invoiceTransactions.ID);
                editInvoiceTransaction.LastModified = DateTime.Now;
                editInvoiceTransaction.ModeOfPayment = invoiceTransactions.ModeOfPayment;
                db.Entry(editInvoiceTransaction).State = EntityState.Modified;
                
                db.SaveChanges();
                var invoiceid = editInvoiceTransaction.Invoice.ID;
                return RedirectToAction("Pay", "Invoices", new { id = invoiceid });
            }
            return View(invoiceTransactions);
        }

        // GET: InvoiceTransactions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceTransactions invoiceTransactions = db.InvoiceTransactions.Find(id);
            if (invoiceTransactions == null)
            {
                return HttpNotFound();
            }
            return View(invoiceTransactions);
        }

        // POST: InvoiceTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            InvoiceTransactions invoiceTransactions = db.InvoiceTransactions.Find(id);
            var invoice = db.Invoices.Find(invoiceTransactions.Invoice.ID);
            var invoiceId = invoice.ID;
            invoice.AmountPaid = invoice.AmountPaid - invoiceTransactions.Amount;
            decimal percentage=0;
            if (invoice.InvoiceItems.Count() > 0)
            {
                percentage = (invoice.AmountPaid / (((invoice.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units))) - invoice.DiscountAmount) * (1 + (((decimal)(db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault()) / (decimal)100)))) * 100);
            }
            if (percentage != (decimal)100)
            {
                invoice.Status = Math.Round(percentage, 2).ToString() + "% Paid";

            }
            else
            {
                invoice.Status = "Paid";
            }
            db.InvoiceTransactions.Remove(invoiceTransactions);
            db.SaveChanges();
            return RedirectToAction("Pay", "Invoices", new { id = invoiceId });
            //return RedirectToAction("Index");
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
