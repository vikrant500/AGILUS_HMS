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
    public class ReferalsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Referals
        public ActionResult Index()
        {
            return View(db.Referals.ToList());
        }

        // GET: Referals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referal Referal = db.Referals.Find(id);
            if (Referal == null)
            {
                return HttpNotFound();
            }
            return View(Referal);
        }

        // GET: Referals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Referals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "ID,Name")] Referal Referal)
        {
            if (ModelState.IsValid)
            {
                db.Referals.Add(Referal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Referal);
        }

        // GET: Referals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referal Referal = db.Referals.Find(id);
            if (Referal == null)
            {
                return HttpNotFound();
            }
            return View(Referal);
        }

        // POST: Referals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,Name")] Referal Referal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Referal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Referal);
        }

        // GET: Referals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referal Referal = db.Referals.Find(id);
            if (Referal == null)
            {
                return HttpNotFound();
            }
            return View(Referal);
        }

        // POST: Referals/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Referal Referal = db.Referals.Find(id);
            db.Referals.Remove(Referal);
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
