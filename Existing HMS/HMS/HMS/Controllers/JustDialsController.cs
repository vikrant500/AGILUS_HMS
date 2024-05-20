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
using PagedList;

namespace HMS.Controllers
{
    public class JustDialsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: JustDials
        public ActionResult Index()
        {

            //int pageSize = 10;
            //int pageNumber = (page ?? 1);
            //return View(db.JustDials.ToList().ToPagedList(pageNumber, pageSize));
            return View(db.JustDials.ToList());
        }

        // GET: JustDials/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JustDial justDial = db.JustDials.Find(id);
            if (justDial == null)
            {
                return HttpNotFound();
            }
            return View(justDial);
        }

        // GET: JustDials/Create
        public ActionResult Create()
        {
            return View(new JustDial());
        }

        // POST: JustDials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create( JustDial justDial)
        {

            if (ModelState.IsValid)
            {
              db.JustDials.Add(justDial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(justDial);
        }

        // GET: JustDials/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JustDial justDial = db.JustDials.Find(id);
            if (justDial == null)
            {
                return HttpNotFound();
            }
            return View(justDial);
        }

        // POST: JustDials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,PhoneNumber,Email,Message")] JustDial justDial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(justDial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(justDial);
        }

        // GET: JustDials/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JustDial justDial = db.JustDials.Find(id);
            if (justDial == null)
            {
                return HttpNotFound();
            }
            return View(justDial);
        }

        // POST: JustDials/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            JustDial justDial = db.JustDials.Find(id);
            db.JustDials.Remove(justDial);
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
