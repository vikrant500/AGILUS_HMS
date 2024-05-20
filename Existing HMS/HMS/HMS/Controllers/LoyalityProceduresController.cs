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
    public class LoyalityProceduresController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: LoyalityProcedures
        public ActionResult Index()
        {
            return View(db.LoyalityProcedures.ToList());
        }

        // GET: LoyalityProcedures/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoyalityProcedure loyalityProcedure = db.LoyalityProcedures.Find(id);
            if (loyalityProcedure == null)
            {
                return HttpNotFound();
            }
            return View(loyalityProcedure);
        }

        // GET: LoyalityProcedures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoyalityProcedures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "ID")] LoyalityProcedure loyalityProcedure)
        {
            if (ModelState.IsValid)
            {
                db.LoyalityProcedures.Add(loyalityProcedure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loyalityProcedure);
        }

        // GET: LoyalityProcedures/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoyalityProcedure loyalityProcedure = db.LoyalityProcedures.Find(id);
            if (loyalityProcedure == null)
            {
                return HttpNotFound();
            }
            return View(loyalityProcedure);
        }

        // POST: LoyalityProcedures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID")] LoyalityProcedure loyalityProcedure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loyalityProcedure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loyalityProcedure);
        }

        // GET: LoyalityProcedures/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoyalityProcedure loyalityProcedure = db.LoyalityProcedures.Find(id);
            if (loyalityProcedure == null)
            {
                return HttpNotFound();
            }
            return View(loyalityProcedure);
        }

        // POST: LoyalityProcedures/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            LoyalityProcedure loyalityProcedure = db.LoyalityProcedures.Find(id);
            db.LoyalityProcedures.Remove(loyalityProcedure);
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
