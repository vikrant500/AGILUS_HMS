using HMS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using System.Data.Entity;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace HMS.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class InvoicesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        private readonly UserManager<AppUser> _userManager;
        private SendMessage sendMessage = new SendMessage();
        // GET: Invoice
        public InvoicesController()
        {

        }
        public InvoicesController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public ActionResult Index()
        {
            return View(db.Invoices.ToList());
        }

        public ActionResult Create()
        {
            Session["procedures"] = null;
            InvoiceVm obj = new InvoiceVm();
            List<SelectListItem> referalsList = (from c in db.Referals.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = $"{c.Name}",
                                                     Value = c.Name.ToString()
                                                 }).ToList();
            referalsList.Insert(0, new SelectListItem() { Text = "--Select Referal--", Value = "" });
            obj.ReferalsList = referalsList;
            return View(obj);
        }
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceBill bill = new InvoiceBill();
            Taxes Tax = new Taxes();
            Tax.Name = "Tax";
            Tax.Value = 0;
            var invoice = db.Invoices.Find(id);
            bill.CreatedDate = invoice.DateCreated;
            bill.ID = invoice.ID;
            bill.InvoiceItems = invoice.InvoiceItems;
            bill.Patient = invoice.Patient;
            
            bill.Total = invoice.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units));
            bill.Tax = (db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault());
            bill.NetTotal = bill.Total * (1 + (((decimal)bill.Tax / (decimal)100)));
            bill.Doctor = invoice.PresDoctor;

            return View(bill);
        }

        // POST: DoctorSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            Invoice a = db.Invoices.Find(id);
            //List<InvoiceItem> invoiceItem = new List<InvoiceItem>();
            //invoiceItem = db.InvoiceItems.Where(x => x.Invoice.ID == id).ToList();
            //foreach(var i in invoiceItem)
            //{
            //    db.InvoiceItems.Remove(i);
            //    db.SaveChanges();
            //}
            //db.Invoices.Remove(a);
            a.Status = "Cancelled";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int ID)
        {
            InvoiceBill bill = new InvoiceBill();
            Taxes Tax = new Taxes();
            Tax.Name = "Tax";
            Tax.Value = 0;
            var invoice = db.Invoices.Find(ID);
            bill.CreatedDate = invoice.DateCreated;
            bill.ID = invoice.ID;
            bill.InvoiceItems = invoice.InvoiceItems;
            bill.Patient = invoice.Patient;
            bill.Total = invoice.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units));
            bill.Tax = (db.Taxes.Where(x => x.Name == "GST").Select(x=>x.Value).DefaultIfEmpty(0).FirstOrDefault());
            bill.DiscountedTotal = bill.Total - invoice.DiscountAmount;
            bill.NetTotal = ((bill.Total - invoice.DiscountAmount) * (1 + (((decimal)bill.Tax / (decimal)100))));
            bill.Doctor = invoice.PresDoctor;
            bill.Center = db.Centers.Find(invoice.Patient.CenterID);
            bill.InvoiceTransactions = invoice.InvoiceTransactions;
            bill.AmountPaid = invoice.AmountPaid;
            bill.Status = invoice.Status;
            bill.InvoiceDisplayID = invoice.InvoiceDisplayID;
            bill.DiscountPercentage = invoice.DiscountPercentage;
            bill.DiscountAmount = invoice.DiscountAmount;
            
            return View(bill);
        }
        public ActionResult Edit(long id)
        {
            Session["procedures"] = null;
            InvoiceVm obj = new InvoiceVm();
            List<SelectListItem> referalsList = (from c in db.Referals.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = $"{c.Name}",
                                                     Value = c.Name.ToString()
                                                 }).ToList();
            referalsList.Insert(0, new SelectListItem() { Text = "--Select Referal--", Value = "" });
            obj.ReferalsList = referalsList;
            obj.Id = id;
            Invoice invoice = db.Invoices.Find(id);
            obj.PatientID =(int) invoice.Patient.ID;
            obj.LabNo = invoice.LabNo;
            obj.Description = invoice.Description;
            obj.PaymentMode = invoice.PaymentMode;
            obj.PresDoctor = invoice.PresDoctor;
            obj.RefBy = invoice.RefBy;
            obj.Sponsor = invoice.Sponsor;
            var a = invoice.InvoiceItems;
            
            
            return View(obj);
        }
        public ActionResult Pay(long ID)
        {
            InvoiceBill bill = new InvoiceBill();
            var invoice = db.Invoices.Find(ID);
            bill.CreatedDate = invoice.DateCreated;
            bill.ID = invoice.ID;
            bill.InvoiceItems = invoice.InvoiceItems;
            bill.Patient = invoice.Patient;
            bill.Total = invoice.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units));
            bill.Tax = (db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault());
            bill.DiscountedTotal = bill.Total - invoice.DiscountAmount;
            bill.NetTotal = ((bill.Total-invoice.DiscountAmount) * (1 + (((decimal)bill.Tax / (decimal)100))));
            bill.Doctor = invoice.PresDoctor;
            bill.AmountPaid = invoice.AmountPaid;
            bill.InvoiceTransactions = invoice.InvoiceTransactions;
            bill.DiscountAmount = invoice.DiscountAmount;
            bill.DiscountPercentage = invoice.DiscountPercentage;
            return View(bill);
        }
        public ActionResult UpdatePay(string id,string pay,string mode)
        {
            InvoiceBill bill = new InvoiceBill();
            var invoice = db.Invoices.Find(Convert.ToInt64(id));
            decimal percentage=0;
            if (pay =="" || pay == null)
            {
                pay = "0";
                //return RedirectToAction("Index");
            }
            invoice.AmountPaid = invoice.AmountPaid + Convert.ToDecimal(pay);
            if (invoice.InvoiceItems.Count() > 0)
            {
                percentage = (invoice.AmountPaid / (((invoice.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units)))-invoice.DiscountAmount) * (1 + (((decimal)(db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault()) / (decimal)100)))) * 100);
            }
            if (percentage != (decimal)100)
            {
                invoice.Status = Math.Round(percentage,2).ToString() + "% Paid";

            }
            else {
                invoice.Status = "Paid";
            }
            InvoiceTransactions tran = new InvoiceTransactions();
            tran.Amount =Convert.ToDecimal( pay);
            tran.DateCreated = DateTime.Now;
            tran.Invoice = invoice;
            tran.ModeOfPayment = mode;
            var smsmsg = "";
            if (db.Messages.Where(x => x.Name == "PaymentMessage").FirstOrDefault() != null)
            { smsmsg = db.Messages.Where(x => x.Name == "PaymentMessage").FirstOrDefault().MessageText.Replace("[patientname]", tran.Invoice.Patient.Salutation +" "+ tran.Invoice.Patient.FirstName).Replace("[amount]", tran.Amount.ToString()); }
            else { smsmsg = "Dear Patient your payment at Sharanya Health Care is received Sucessfully"; }
            sendMessage.SMS(tran.Invoice.Patient.CallingNumber, smsmsg);

            invoice.InvoiceTransactions.Add(tran);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public string Edit(InvoiceVm model)
        {

            if (Session["procedures"] != null)
            {
                List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

                List<InvoiceProcedures> invoiceProcedures = new List<InvoiceProcedures>();
                invoiceProcedures = (List<InvoiceProcedures>)Session["procedures"];

                foreach (InvoiceProcedures item in invoiceProcedures)
                {
                    invoiceItems.Add(new InvoiceItem()
                    {
                        Discount = item.Discount,
                        Procedure = db.Procedures.Where(x => x.ID == item.ID).FirstOrDefault(),
                        Units = item.Units
                    });
                }

                Invoice invoice = db.Invoices.Find(model.Id);
                invoice.Patient = db.Patients.Where(x => x.ID == model.PatientID).FirstOrDefault();
                invoice.LabNo = model.LabNo;
                invoice.PaymentMode = model.PaymentMode;
                invoice.PresDoctor = model.PresDoctor;
                invoice.RefBy = model.RefBy;
                invoice.Sponsor = model.Sponsor;
                invoice.Description = model.Description;
                invoice.InvoiceItems = invoiceItems;
                db.InvoiceItems.RemoveRange(db.InvoiceItems.Where(x => x.Invoice.ID == model.Id).ToList());
                //db.InvoiceItems.AddRange(invoiceItems);
                invoice.LastModifiedDate = DateTime.Now;
                invoice.Status = "Unpaid";
                DateTime dateTime = invoice.DateCreated;
                string financialYear = (dateTime.Month >= 4 ? dateTime.ToString("yy") + '-' + dateTime.AddYears(1).ToString("yy") : dateTime.AddYears(-1).ToString("yy") + '-' + dateTime.ToString("yy"));
                invoice.InvoiceDisplayID = financialYear + '/' + invoice.ID.ToString();
                decimal percentage = 0;
                if (invoice.InvoiceItems.Count() > 0)
                {
                    try
                    {
                        percentage = (invoice.AmountPaid / (((invoice.InvoiceItems.Sum(x => ((x.Procedure.Price * (1 - (x.Discount / 100))) * x.Units))) - invoice.DiscountAmount) * (1 + (((decimal)(db.Taxes.Where(x => x.Name == "GST").Select(x => x.Value).DefaultIfEmpty(0).FirstOrDefault()) / (decimal)100)))) * 100);

                    }
                    catch (System.DivideByZeroException)
                    {
                        percentage = 0;
                    }
                }
                if (percentage != (decimal)100)
                {
                    invoice.Status = Math.Round(percentage, 2).ToString() + "% Paid";

                }
                else
                {
                    invoice.Status = "Paid";
                }
                if (ModelState.IsValid)
                {
                    
                    db.SaveChanges();
                    return "1";
                }
            };
            return "0";
        }
        [HttpPost]
        public string Create(InvoiceVm model)
        {

            if (Session["procedures"] != null)
            {
                List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

                List<InvoiceProcedures> invoiceProcedures = new List<InvoiceProcedures>();
                invoiceProcedures = (List<InvoiceProcedures>)Session["procedures"];
                decimal total = 0;
                foreach (InvoiceProcedures item in invoiceProcedures)
                {
                    invoiceItems.Add(new InvoiceItem()
                    {
                        Discount = item.Discount,
                        Procedure = db.Procedures.Where(x => x.ID == item.ID).FirstOrDefault(),
                        Units = item.Units
                    });
                    total = total + (db.Procedures.Where(x => x.ID == item.ID).FirstOrDefault().Price * item.Units);
                }

                Invoice invoice = new Invoice();
                invoice.Doctor = db.Doctors.Where(x => x.ID == model.DoctorID).FirstOrDefault();
                invoice.Patient = db.Patients.Where(x => x.ID == model.PatientID).FirstOrDefault();
                invoice.LabNo = model.LabNo;
                invoice.PaymentMode = model.PaymentMode;
                invoice.PresDoctor = model.PresDoctor;
                invoice.RefBy = model.RefBy;
                invoice.Sponsor = model.Sponsor;
                invoice.Department = db.Departments.Where(x=> x.ID==model.DepartmentID).FirstOrDefault();
                invoice.Description = model.Description;
                invoice.InvoiceItems = invoiceItems;
                invoice.DateCreated = DateTime.Now;
                invoice.Status = "Unpaid";
                invoice.DiscountAmount = model.DiscountAmount;

                invoice.CreatedBy = User.Identity.Name;
                if (total > 0)
                {
                    invoice.DiscountPercentage = Math.Round((model.DiscountAmount / total) * 100, 2);
                }
                if (ModelState.IsValid)
                {
                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                    DateTime dateTime = invoice.DateCreated;

                    string financialYear = (dateTime.Month >= 4 ? dateTime.ToString("yy") + '-' + dateTime.AddYears(1).ToString("yy") : dateTime.AddYears(-1).ToString("yy") + '-' + dateTime.ToString("yy"));
                    invoice.InvoiceDisplayID = financialYear + '/' + invoice.ID.ToString();
                    db.SaveChanges();
                    return "1";
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                    return errors.FirstOrDefault().Select(z=>z.ErrorMessage).FirstOrDefault().ToString();
                }
            }
            else
            {
                return "Invoice Has No Procedures";
                
            }
            
        }
        public JsonResult GetProceduresData(string procedureName)
        {
            IEnumerable<long> ids = new List<long>();
            if (Session["procedures"] != null)
            {
                ids = ((List<InvoiceProcedures>)Session["procedures"]).Select(x => x.ID);
            }

            List<ProcedureResult> procedureResults = (from proc in db.Procedures
                                                      where proc.Name.Contains(procedureName)
                                                      select
                                                      new ProcedureResult
                                                      {
                                                          ID = proc.ID,
                                                          Name = proc.Name,
                                                          Price = proc.Price
                                                      }).ToList().Where(x => !ids.Contains(x.ID)).ToList();

            ProcedureResult[] procedures = new ProcedureResult[procedureResults.Count];
            procedures = procedureResults.ToArray();
            return Json(procedures, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPatientACData(string patientName)
        {
            List<PatientACResult> patientACResult = (from pat in db.Patients
                                                     where pat.FirstName.Contains(patientName) || pat.LastName.Contains(patientName) || pat.ID.ToString().Contains(patientName)
                                                     select
                                                     new PatientACResult
                                                     {
                                                         ID = pat.ID,
                                                         Name = pat.FirstName
                                                     }).ToList();

            PatientACResult[] patients = new PatientACResult[patientACResult.Count];
            patients = patientACResult.ToArray();
            return Json(patients, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPatientACCData(string patientName)
        {
            List<PatientACResult> patientACResult = (from pat in db.Patients
                                                     where pat.FirstName.Contains(patientName) || pat.LastName.Contains(patientName) || pat.ID.ToString().Contains(patientName)
                                                     select
                                                     new PatientACResult
                                                     {
                                                         ID = pat.ID,
                                                         Name =pat.Salutation+ pat.FirstName+" "+pat.MiddleName+" " +pat.LastName
                                                     }).ToList();

            PatientACResult[] patients = new PatientACResult[patientACResult.Count];
            patients = patientACResult.ToArray();
            return Json(patients, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProcedureACData(int ID)
        {
            Procedure procedureDetails = new Procedure();
            var search = db.Procedures.Where(x => x.ID == ID).FirstOrDefault();
            procedureDetails = new Procedure
            {
                Name = search.Name,
                Price=search.Price
            };
            return Json(procedureDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPatientData(int ID)
        {
            PatientDetailsVm patientDetails = new PatientDetailsVm();
            var search = db.Patients.Where(x => x.ID == ID).FirstOrDefault();
            patientDetails = new PatientDetailsVm
            {
                ID = search.ID,
                FirstName = search.FirstName,
                MiddleName = search.MiddleName,
                LastName = search.LastName,
                Address = search.Address,
                City = search.City,
                Pincode = search.PinCode,
                ContactNumber = search.CallingNumber,
                Gender = search.Gender,
                Email=search.EmailID
            };
            if (patientDetails.LastName == null)
            {
                patientDetails.LastName = " ";
            }
            return Json(patientDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDoctorIdData(int ID)
        {
            PatientDetailsVm patientDetails = new PatientDetailsVm();
            var search = db.Doctors.Where(x => x.ID == ID).FirstOrDefault();
            patientDetails = new PatientDetailsVm
            {
                ID = search.ID,
                FirstName = search.Name,
                
                Address = search.Address,
                
            };
            if (patientDetails.LastName == null)
            {
                patientDetails.LastName = " ";
            }
            return Json(patientDetails, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddProcedure(long _ProcID, int _Units, decimal _Discount)
        {
            var procedure = db.Procedures.Where(x => x.ID == _ProcID).FirstOrDefault();
            _Discount = (_Discount / procedure.Price) * 100;
            List<InvoiceProcedures> invoiceProcedures = new List<InvoiceProcedures>();
            if (procedure != null)
            {
                invoiceProcedures.Add(new InvoiceProcedures { ID = _ProcID, ProcedureName = procedure.Name, Price = procedure.Price, Units = _Units, Total = procedure.Price * _Units, Discount = _Discount, NetAmount = (procedure.Price * (1 - (_Discount / 100))) * _Units });
            }
            if (Session["procedures"] == null)
            {
                Session["procedures"] = invoiceProcedures;
            }
            else
            {
                invoiceProcedures.AddRange((List<InvoiceProcedures>)Session["procedures"]);
                Session["procedures"] = invoiceProcedures;
            }
            return PartialView("_InvoiceProcedures", invoiceProcedures);
        }
        public ActionResult DelProcedure(long _ProcID)
        {
            List<InvoiceProcedures> invoiceProcedures = new List<InvoiceProcedures>();
            invoiceProcedures.AddRange((List<InvoiceProcedures>)Session["procedures"]);
            InvoiceProcedures item = invoiceProcedures.Where(x => x.ID == _ProcID).FirstOrDefault();
            invoiceProcedures.Remove(item);
            Session["procedures"] = invoiceProcedures;
            return PartialView("_InvoiceProcedures", invoiceProcedures);
        }
        public ActionResult RenderProcedures(long id)
        {
            List<InvoiceProcedures> invoiceProcedures = new List<InvoiceProcedures>();

            var a =(db.InvoiceItems.Where(x => x.Invoice.ID == id).ToList());
            foreach(var procedure in a)
            {
                invoiceProcedures.Add(new InvoiceProcedures { ID = procedure.Procedure.ID, ProcedureName = procedure.Procedure.Name, Price = procedure.Procedure.Price, Units = procedure.Units, Total = procedure.Procedure.Price * procedure.Units, Discount = procedure.Discount, NetAmount = (procedure.Procedure.Price * (1 - (procedure.Discount / 100))) * procedure.Units });
            }
            Session["procedures"] = invoiceProcedures;
            return PartialView("_InvoiceProcedures", invoiceProcedures);
        }
    }
}
public class InvoiceBill
{
    public long ID { get; set; }
    public string InvoiceDisplayID { get; set; }
    public Patient Patient { get; set; }
    public string Doctor { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
    public ICollection<InvoiceTransactions> InvoiceTransactions { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Tax { get; set; }
    public decimal Total { get; set; }
    public decimal NetTotal { get; set; }
    public  Center Center{get; set;}
    public decimal AmountPaid { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountedTotal { get; set; }
    public string Status { get; set; }
    
}
public class InvoiceProcedures
{
    public long ID { get; set; }
    public string ProcedureName { get; set; }
    public decimal Price { get; set; }
    public int Units { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public decimal NetAmount { get; set; }

}

public class ProcedureResult
{
    public long ID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}


public class PatientACResult
{
    public long ID { get; set; }
    public string Name { get; set; }

}

public class PatientDetailsVm
{
    public long ID { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string ContactNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string Pincode { get; set; }
}