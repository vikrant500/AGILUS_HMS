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
    public class ProceduresController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Procedures
        public ActionResult Index()
        {
            IEnumerable<ProceduresListVm> procedures = new List<ProceduresListVm>();
            procedures = (from procs in db.Procedures
                          join dept in db.Departments on procs.Department.ID equals dept.ID

                          select
                          new ProceduresListVm
                          {
                              ID = procs.ID,
                              Name = procs.Name,
                              DepartmentName = dept.Name,
                              Price = procs.Price,
                              Description=procs.Description

                          }).ToList();
            //return View(db.Procedures.ToList());
            return View(procedures);
        }

        // GET: Procedures/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedure procedure = db.Procedures.Find(id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            return View(procedure);
        }

        // GET: Procedures/Create
        public ActionResult Create()
        {
            ProcedureVm procedureVm = new ProcedureVm();
            FillDepartmentDropDown(procedureVm);
            return View(procedureVm);
        }

        // POST: Procedures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Create(ProcedureVm procedurevm)
        {
            Procedure procedure = new Procedure();
            procedure.Department = db.Departments.Where(x => x.ID == procedurevm.DepartmentID).FirstOrDefault();
            procedure.Name = procedurevm.Name;
            procedure.Price = procedurevm.Price;
            procedure.Description = procedurevm.Description;

            if (ModelState.IsValid)
            {
                db.Procedures.Add(procedure);
                db.SaveChanges();
                return "1";
            }

            return "0";
        }

        // GET: Procedures/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedure procedure = db.Procedures.Find(id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            return View(procedure);
        }

        // POST: Procedures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Price")] Procedure procedure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(procedure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procedure);
        }

        // GET: Procedures/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedure procedure = db.Procedures.Find(id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            return View(procedure);
        }

        // POST: Procedures/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            Procedure procedure = db.Procedures.Find(id);
            db.Procedures.Remove(procedure);
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

        private void FillDepartmentDropDown(ProcedureVm procedureVm)
        {
            List<SelectListItem> stateList = (from s in db.Departments.AsEnumerable()
                                              select new SelectListItem
                                              {
                                                  Text = $"{s.Name}",
                                                  Value = s.ID.ToString()
                                              }).ToList();
            stateList.Insert(0, new SelectListItem() { Text = "--Select Department--", Value = "" });
            procedureVm.DepartmentList = stateList;
        }
    }
}
