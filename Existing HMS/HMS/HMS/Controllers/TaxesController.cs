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
    public class TaxesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Taxes
        public ActionResult Index()
        {
            return View(db.Taxes.ToList());
        }

        // GET: Taxes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxes taxes = db.Taxes.Find(id);
            if (taxes == null)
            {
                return HttpNotFound();
            }
            return View(taxes);
        }

        // GET: Taxes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Taxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "ID,Name,Value")] Taxes taxes)
        {
            if (ModelState.IsValid)
            {
                db.Taxes.Add(taxes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taxes);
        }

        // GET: Taxes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxes taxes = db.Taxes.Find(id);
            if (taxes == null)
            {
                return HttpNotFound();
            }
            return View(taxes);
        }

        // POST: Taxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,Name,Value")] Taxes taxes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taxes);
        }

        // GET: Taxes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxes taxes = db.Taxes.Find(id);
            if (taxes == null)
            {
                return HttpNotFound();
            }
            return View(taxes);
        }

        // POST: Taxes/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            Taxes taxes = db.Taxes.Find(id);
            db.Taxes.Remove(taxes);
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
