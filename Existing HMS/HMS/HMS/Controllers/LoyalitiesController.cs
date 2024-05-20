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
    [Authorize(Roles = "Administrators")]
    public class LoyalitiesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Loyalities
        public ActionResult Index()
        {
            return View(db.Loyalities.ToList());
        }

        // GET: Loyalities/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loyality loyality = db.Loyalities.Find(id);
            if (loyality == null)
            {
                return HttpNotFound();
            }
            return View(loyality);
        }
        public JsonResult GetProceduresData(string procedureName)
        {
            IEnumerable<long> ids = new List<long>();
            if (Session["loyalityProcedures"] != null)
            {
                ids = ((List<LoyalityVMProcedure>)Session["loyalityProcedures"]).Select(x => x.ID);
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
        // GET: Loyalities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Loyalities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "ID")] Loyality loyality)
        {
            if (ModelState.IsValid)
            {
                db.Loyalities.Add(loyality);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loyality);
        }

        // GET: Loyalities/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loyality loyality = db.Loyalities.Find(id);
            
            
            if (loyality == null)
            {
                return HttpNotFound();
            }
            return View(loyality);
        }
        public ActionResult EditAdd(long _ProcID,long loyalityID)
        {
            var loyality = db.Loyalities.Find(loyalityID);
            var procedure = db.LoyalityProcedures.Where(x => x.Loyality.ID == loyalityID).ToList();
            if (procedure.Count < 5)
            {
                var item = db.Procedures.Find(_ProcID);
                if (!procedure.Any(x=>x.Procedure.ID==_ProcID))
                {
                    procedure.Add(new LoyalityProcedure()
                    {
                        Procedure = item
                    }) ;
                    loyality.Procedures = procedure;
                    db.SaveChanges();
                }
            }
            return View(loyality);
        }
        public ActionResult EditDel(long _ProcID, long loyalityID)
        {
            var loyality = db.Loyalities.Find(loyalityID);
            var procedure = db.LoyalityProcedures.Where(x => x.Loyality.ID == loyalityID).ToList();
            procedure.RemoveAll(x => x.Procedure.ID == _ProcID);
            loyality.Procedures = procedure;
            db.SaveChanges();
            return View(loyality);
        }
        // POST: Loyalities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID")] Loyality loyality)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loyality).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loyality);
        }

        // GET: Loyalities/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loyality loyality = db.Loyalities.Find(id);
            if (loyality == null)
            {
                return HttpNotFound();
            }
            return View(loyality);
        }

        // POST: Loyalities/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            Loyality loyality = db.Loyalities.Find(id);
            var a = db.LoyalityProcedures.Where(x => x.Loyality.ID == id).ToList();
            db.LoyalityProcedures.RemoveRange(a);
            var delloyalitydependents= db.LoyalityDependents.Where(x=>x.Loyality.ID==id).ToList();
            db.LoyalityDependents.RemoveRange(delloyalitydependents);
            db.Loyalities.Remove(loyality);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddProcedure(long _ProcID)
        {
            var procedure = db.Procedures.Where(x => x.ID == _ProcID).FirstOrDefault();
            List<LoyalityVMProcedure>loyalityProcedures = new List<LoyalityVMProcedure>();
            loyalityProcedures.Add(new LoyalityVMProcedure { ID = _ProcID, Name=procedure.Name,Price=procedure.Price});
            if (Session["loyalityProcedures"] == null)
            {
                Session["loyalityProcedures"] = loyalityProcedures;
            }
            else
            {
                var  a=(List<LoyalityVMProcedure>)Session["loyalityProcedures"];
                if (a.Count == 5)
                {
                    return PartialView("_LoyalityProcedures", a);
                }
                loyalityProcedures.AddRange(a);
                Session["loyalityProcedures"] = loyalityProcedures;
            }
            return PartialView("_LoyalityProcedures", loyalityProcedures);
        }
        public ActionResult AddMember(int ID)
        {
            List<LoyalityProcedure> procedurelist = new List<LoyalityProcedure>();
            List<LoyalityDependents> dependentslist = new List<LoyalityDependents>();
            if (Session["loyalityProcedures"] != null)
            {
                foreach (var item in (List<LoyalityVMProcedure>)Session["loyalityProcedures"])
                {
                    procedurelist.Add(new LoyalityProcedure()
                    {
                        Procedure = db.Procedures.Find(item.ID)

                    });

                }
            }
            if (Session["loyalityDependents"] != null)
            {
                foreach (var item in (List<LoyalityVMDependent>)Session["loyalityDependents"])
                {
                    dependentslist.Add(new LoyalityDependents()
                    {
                        Name = item.Name,
                        Relation = item.Relation,
                        Gender = item.Gender,
                        Age = item.Age,
                        DOB = Convert.ToDateTime(item.DOB)

                    });

                }
            }
            
            
                Loyality loyality = new Loyality();
            loyality.Patient = db.Patients.Where(x => x.ID == ID).FirstOrDefault();
            loyality.Procedures = procedurelist;
            loyality.LoyalityDependents = dependentslist;
            
            
                db.Loyalities.Add(loyality);
                db.SaveChanges();
                return View();
            
            

           
        }
        public ActionResult AddDependent(string name,string relation,string gender, string dob,int age)
        {
            
            List<LoyalityVMDependent> loyalityVMDependents = new List<LoyalityVMDependent>();
            loyalityVMDependents.Add(new LoyalityVMDependent { Name = name, Relation = relation, Gender = gender, DOB =dob,Age=age});
            if (Session["loyalityDependents"] == null)
            {
                Session["loyalityDependents"] = loyalityVMDependents;
            }
            else
            {
                loyalityVMDependents.AddRange((List<LoyalityVMDependent>)Session["loyalityDependents"]);
                Session["loyalityDependents"] = loyalityVMDependents;
            }
            return PartialView("_LoyalityDependents", loyalityVMDependents);
        }
        public ActionResult DelProcedure(long _ProcID)
        {
            List<LoyalityVMProcedure> loyalityVMProcedures = new List<LoyalityVMProcedure>();
            loyalityVMProcedures.AddRange((List<LoyalityVMProcedure>)Session["loyalityProcedures"]);
            LoyalityVMProcedure item = loyalityVMProcedures.Where(x => x.ID == _ProcID).FirstOrDefault();
            loyalityVMProcedures.Remove(item);
            Session["loyalityProcedures"] = loyalityVMProcedures;
            return PartialView("_LoyalityProcedures", loyalityVMProcedures);
        }
        public ActionResult DelDependent(int _ProcID)
        {
            List<LoyalityVMDependent> loyalityVMDependents = new List<LoyalityVMDependent>();
            loyalityVMDependents.AddRange((List<LoyalityVMDependent>)Session["loyalityDependents"]);
            
            loyalityVMDependents.RemoveAt(_ProcID);
            Session["loyalityDependents"] = loyalityVMDependents;
            return PartialView("_LoyalityDependents", loyalityVMDependents);
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
public class LoyalityVMProcedure
{ 
public long ID { get; set; }
public string Name { get; set; }

public decimal Price { get; set; }
}

public class LoyalityVMDependent
{
    public string Name { get; set; }
    public string Relation { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }

    public string DOB { get; set; }


}
