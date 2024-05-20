using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Infrastructure;
using HMS.VeiwModels;

namespace HMS.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class InvoiceReportsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: InvoiceReports
        public ActionResult Index(string doctor="", string from="", string to="", string procedure="", string department="")

        {
            DateTime? fromDate = null, toDate = null;

            if (!String.IsNullOrWhiteSpace(from))
            {
                fromDate = Convert.ToDateTime(from);
            }
            else
            {
                fromDate = DateTime.Today.AddYears(-100);
            }
            if (!String.IsNullOrWhiteSpace(to))
            {
                toDate = Convert.ToDateTime(to).AddDays(1);
            }
            else
            {
                toDate = DateTime.Today.AddDays(1);
            }
            IEnumerable<HMS.Models.Invoice> Invoices = new List<HMS.Models.Invoice>();
            List<InvoiceReport> retobj = new List<InvoiceReport>();
            Invoices = (from i in db.Invoices
                        select i
                      ).ToList();
            
            foreach (var item in Invoices)
            {
                
                if (item.Department == null)
                {
                    item.Department = item.Doctor.Department;
                }
                
                var obj = new InvoiceReport
                {
                    ID=item.ID,
                    InvoiceDisplayId = item.InvoiceDisplayID,
                    DateCreated=item.DateCreated,
                    PatientId=item.Patient.ID,
                    PaidAmount=item.AmountPaid ,
                    PendingAmount = item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) - item.DiscountAmount - item.AmountPaid+ item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) * (db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault()) / 100,
                    CreatedBy=item.CreatedBy,
                    PatientName = item.Patient.FirstName + ' ' +item.Patient.LastName,
                    Doctor = item.PresDoctor,
                    DepartmentName=item.Department.Name,
                    Date = item.DateCreated,
                    TotalCost = item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units))-item.DiscountAmount+ item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) * (db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault()) / 100,
                    InvoiceItems = item.InvoiceItems,
                    InvoiceTransactions=item.InvoiceTransactions
                };
                
                
                retobj.Add(obj);
            }
            if (procedure != "" && department!="")
            {
                var temp = (from i in db.Invoices
                            join items in db.InvoiceItems on i.ID equals items.Invoice.ID
                            where items.Procedure.Name == procedure 
                            select i).ToList();
                retobj = new List<InvoiceReport>();
                foreach (var item in temp)
                {
                    if (item.Department == null)
                    {
                        item.Department = item.Doctor.Department;
                    }
                    var obj = new InvoiceReport
                    {
                        ID = item.ID,
                        InvoiceDisplayId = item.InvoiceDisplayID,
                        DateCreated = item.DateCreated,
                        PatientId = item.Patient.ID,
                        PaidAmount = item.AmountPaid,
                        PendingAmount = item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) - item.DiscountAmount - item.AmountPaid + item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) * (db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault()) / 100,
                        CreatedBy = item.CreatedBy,
                        PatientName = item.Patient.FirstName + ' ' + item.Patient.LastName,
                        DepartmentName = item.Doctor.Department.Name,
                        Doctor = item.PresDoctor,
                        Date = item.DateCreated,
                        TotalCost = item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) - item.DiscountAmount + item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) * (db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault()) / 100,
                        InvoiceItems = item.InvoiceItems,
                        InvoiceTransactions = item.InvoiceTransactions
                    };
                    retobj.Add(obj);
                }
            }
            else if (procedure != "" && department == "")
            {
                var temp = (from i in db.Invoices
                            join items in db.InvoiceItems on i.ID equals items.Invoice.ID
                            where items.Procedure.Name == procedure
                            select i).ToList();
                retobj = new List<InvoiceReport>();
                foreach (var item in temp)
                {
                    if (item.Department == null)
                    {
                        item.Department = item.Doctor.Department;
                    }
                    var obj = new InvoiceReport
                    {
                        ID = item.ID,
                        InvoiceDisplayId = item.InvoiceDisplayID,
                        DateCreated = item.DateCreated,
                        PatientId = item.Patient.ID,
                        PaidAmount = item.AmountPaid,
                        PendingAmount = item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) - item.DiscountAmount - item.AmountPaid + item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) * (db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault()) / 100,
                        CreatedBy = item.CreatedBy,
                        PatientName = item.Patient.FirstName + ' ' + item.Patient.LastName,
                        DepartmentName = item.Doctor.Department.Name,
                        Doctor = item.PresDoctor,
                        Date = item.DateCreated,
                        TotalCost = item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) - item.DiscountAmount + item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) * (db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault()) / 100,
                        InvoiceItems = item.InvoiceItems,
                        InvoiceTransactions = item.InvoiceTransactions

                    };
                    retobj.Add(obj);
                }
            }
            else if (department != "" && procedure=="")
            {
                var tempa = (from i in db.Invoices
                             join items in db.InvoiceItems on i.ID equals items.Invoice.ID
                             
                             select i).ToList();
                retobj = new List<InvoiceReport>();
                foreach (var item in tempa)
                {
                    if (item.Department == null)
                    {
                        item.Department = item.Doctor.Department;
                    }
                    var obj = new InvoiceReport
                    {
                        ID = item.ID,
                        InvoiceDisplayId = item.InvoiceDisplayID,
                        DateCreated = item.DateCreated,
                        PatientId = item.Patient.ID,
                        PaidAmount = item.AmountPaid,
                        PendingAmount = item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) - item.DiscountAmount - item.AmountPaid + item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) * (db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault()) / 100,
                        CreatedBy = item.CreatedBy,
                        PatientName = item.Patient.FirstName + ' ' + item.Patient.LastName,
                        DepartmentName = item.Department.Name,
                        Doctor = item.PresDoctor,
                        Date = item.DateCreated,
                        TotalCost = item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) - item.DiscountAmount + item.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)) * (db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault()) / 100,
                        InvoiceItems = item.InvoiceItems,
                        InvoiceTransactions = item.InvoiceTransactions
                    };
                    retobj.Add(obj);
                }
            }
            if (department != "")
            {
                retobj.RemoveAll(x => x.DepartmentName != department);
            }
            if (doctor != "")
            {
                retobj.RemoveAll(x => x.Doctor != doctor);
            }
            if (from != "")
            {
                retobj.RemoveAll(x => x.Date < Convert.ToDateTime(fromDate));
            }
            if (to != "")
            {
                retobj.RemoveAll(x => x.Date > Convert.ToDateTime(toDate));
            }
            foreach (var item in retobj)
            {
                string a = "";
                string b = "";
                if (item.InvoiceItems.Count > 0)
                {
                    foreach (var invoiceItem in item.InvoiceItems)
                    {
                        if (!a.Contains(invoiceItem.Procedure.Name))
                        {
                            a = a + invoiceItem.Procedure.Name + ", ";
                        }
                    }
                }
                else
                {
                    a = "No Procedures Added  ";
                }
                if (item.InvoiceTransactions.Count > 0)
                {
                    foreach (var invoicetransactions in item.InvoiceTransactions)
                    {
                        if (!b.Contains(invoicetransactions.ModeOfPayment))
                        {
                            b = b + invoicetransactions.ModeOfPayment + ", ";
                        }
                    }
                }
                else
                {
                    b = "No Payment Done  ";
                }
                item.ProcedureString = a.Substring(0,a.Length-2);
                item.PaymentString = b.Substring(0,b.Length-2);
            }
            return View(retobj);
            //return View(db.InvoiceReports.ToList());
        }

        public JsonResult GetProceduresData(string procedureName)
        {
            IEnumerable<long> ids = new List<long>();
            if (Session["procedures"] != null)
            {
                ids = ((List<InvoiceReportProcedures>)Session["procedures"]).Select(x => x.ID);
            }

            List<InvoiceReportProcedureResult> procedureResults = (from proc in db.Procedures
                                                      where proc.Name.Contains(procedureName)
                                                      select
                                                      new InvoiceReportProcedureResult
                                                      {
                                                          ID = proc.ID,
                                                          Name = proc.Name,
                                                          Price = proc.Price
                                                      }).ToList().Where(x => !ids.Contains(x.ID)).ToList();

            InvoiceReportProcedureResult[] procedures = new InvoiceReportProcedureResult[procedureResults.Count];
            procedures = procedureResults.ToArray();
            return Json(procedures, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDepartmentsData(string departmentName)
        {
            IEnumerable<long> ids = new List<long>();
            if (Session["departments"] != null)
            {
                ids = ((List<InvoiceReportDepartmentResult>)Session["departments"]).Select(x => x.ID);
            }

            List<InvoiceReportDepartmentResult> departmentResults = (from proc in db.Departments
                                                      where proc.Name.Contains(departmentName)
                                                      select
                                                      new InvoiceReportDepartmentResult
                                                      {
                                                          ID = proc.ID,
                                                          Name = proc.Name,
                                                          
                                                      }).ToList().Where(x => !ids.Contains(x.ID)).ToList();

            InvoiceReportDepartmentResult[] departments = new InvoiceReportDepartmentResult[departmentResults.Count];
            departments = departmentResults.ToArray();
            return Json(departments, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCategoryData(string categoryName)
        {
            IEnumerable<long> ids = new List<long>();
            

            List<InvoiceReportCategoryResult> categoryResults = (from proc in db.Categories
                                                                     where proc.CategoryName.Contains(categoryName)
                                                                     select
                                                                     new InvoiceReportCategoryResult
                                                                     {
                                                                         ID = proc.CategoryID,
                                                                         Name = proc.CategoryName,

                                                                     }).ToList().Where(x => !ids.Contains(x.ID)).ToList();

            InvoiceReportCategoryResult[] categorys = new InvoiceReportCategoryResult[categoryResults.Count];
            categorys = categoryResults.ToArray();
            return Json(categorys, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDoctorsData(string doctorName)
        {
            IEnumerable<long> ids = new List<long>();
            if (Session["doctors"] != null)
            {
                ids = ((List<InvoiceReportDoctorResult>)Session["doctors"]).Select(x => x.ID);
            }

            List<InvoiceReportDoctorResult> doctorResults = (from proc in db.Doctors
                                                                     where proc.Name.Contains(doctorName)
                                                                     select
                                                                     new InvoiceReportDoctorResult
                                                                     {
                                                                         ID = proc.ID,
                                                                         Name = proc.Name

                                                                     }).ToList().Where(x => !ids.Contains(x.ID)).ToList();

            InvoiceReportDoctorResult[] doctors = new InvoiceReportDoctorResult[doctorResults.Count];
            doctors = doctorResults.ToArray();
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }
        // GET: InvoiceReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceReport invoiceReport = db.InvoiceReports.Find(id);
            if (invoiceReport == null)
            {
                return HttpNotFound();
            }
            return View(invoiceReport);
        }

        // GET: InvoiceReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvoiceReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "ID,PatientName,Doctor")] InvoiceReport invoiceReport)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceReports.Add(invoiceReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoiceReport);
        }

        // GET: InvoiceReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceReport invoiceReport = db.InvoiceReports.Find(id);
            if (invoiceReport == null)
            {
                return HttpNotFound();
            }
            return View(invoiceReport);
        }

        // POST: InvoiceReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,PatientName,Doctor")] InvoiceReport invoiceReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoiceReport);
        }

        // GET: InvoiceReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceReport invoiceReport = db.InvoiceReports.Find(id);
            if (invoiceReport == null)
            {
                return HttpNotFound();
            }
            return View(invoiceReport);
        }

        // POST: InvoiceReports/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceReport invoiceReport = db.InvoiceReports.Find(id);
            db.InvoiceReports.Remove(invoiceReport);
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
public class InvoiceReportProcedures
{
    public long ID { get; set; }
    public string ProcedureName { get; set; }
    public decimal Price { get; set; }
    public int Units { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public decimal NetAmount { get; set; }

}
public class InvoiceReportProcedureResult
{
    public long ID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
public class InvoiceReportDepartmentResult
{
    public long ID { get; set; }
    public string Name { get; set; }
    
}
public class InvoiceReportCategoryResult
{
    public long ID { get; set; }
    public string Name { get; set; }

}
public class InvoiceReportDoctorResult
{
    public long ID { get; set; }
    public string Name { get; set; }
    
}
