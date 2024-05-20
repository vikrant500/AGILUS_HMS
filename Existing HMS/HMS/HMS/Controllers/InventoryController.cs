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
    public class InventoryController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Procedures
        public ActionResult Index(string staff,string from, string to, string category)
        {
            List<InventoryListVm> itemsList = new List<InventoryListVm>();
            itemsList = (from items in db.Inventories
                          join cat in db.Categories on items.Category.CategoryID equals cat.CategoryID

                          select
                          new InventoryListVm
                          {
                              ID = items.ID,
                              Name = items.Name,
                              CategoryName = cat.CategoryName,
                              Price = items.Price,
                              Quantity=items.Quantity,
                              Description = items.Descrption,
                              StaffName=items.Staff.Name,
                              ModeOfPayment=items.ModeOfPayment,
                              CreatedDate=items.DateCreated,
                              Type=items.Option

                          }).ToList();
            //return View(db.Procedures.ToList());
            if (staff != null && staff != "")
            {
                itemsList.RemoveAll(x => x.StaffName != staff);
            }
            if (category != null && category != "")
            {
                itemsList.RemoveAll(x => x.CategoryName != category);
            }
            if (from != null && from != "")
            {
                itemsList.RemoveAll(x => x.CreatedDate < Convert.ToDateTime(from));
            }
            if (to != null && to != "")
            {
                itemsList.RemoveAll(x => x.CreatedDate > Convert.ToDateTime(to).AddDays(1));
            }
            return View(itemsList);
        }

        // GET: Procedures/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory item = db.Inventories.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Procedures/Create
        public ActionResult Create()
        {
            InventoryVm itemVm = new InventoryVm();
            FillCategoryDropDown(itemVm);
            return View(itemVm);
        }

        // POST: Procedures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Create(InventoryVm itemvm)
        {
            if (String.IsNullOrEmpty(itemvm.Description) || String.IsNullOrEmpty(itemvm.StaffName) || String.IsNullOrEmpty(itemvm.Option))
                { return "0"; }
            {
                Inventory item = new Inventory();
                item.Category = db.Categories.Where(x => x.CategoryID == itemvm.CategoryID).FirstOrDefault();
                item.Name = itemvm.Name;
                item.Price = itemvm.Price;
                item.Quantity = itemvm.Quantity;
                item.Descrption = itemvm.Description;
                item.DateCreated = DateTime.Now;
                item.Option = itemvm.Option;
                item.ModeOfPayment = itemvm.ModeOfPayment;
                item.Staff = db.Staffs.Find(itemvm.StaffID);
                if (ModelState.IsValid)
                {
                    db.Inventories.Add(item);
                    db.SaveChanges();
                    return "1";
                }
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
            Inventory item = db.Inventories.Find(id);
            InventoryVm itemvm = new InventoryVm();
            itemvm.ID = item.ID;
            itemvm.CategoryID = item.Category.CategoryID;
            itemvm.Name = item.Name;
            itemvm.Price = item.Price;
            itemvm.Description = item.Descrption;
            itemvm.Quantity = item.Quantity;
            itemvm.Option = item.Option;
            itemvm.StaffID =(int) item.Staff.ID;
            itemvm.StaffName = item.Staff.Name;
            if (item == null)
            {
                return HttpNotFound();
            }
            FillCategoryDropDown(itemvm);
            return View(itemvm);
        }

        // POST: Procedures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit( InventoryVm itemvm)
        {
            if (ModelState.IsValid)
            {
                Inventory item = db.Inventories.Find(itemvm.ID);
                item.Category = db.Categories.Where(x => x.CategoryID == itemvm.CategoryID).FirstOrDefault();
                item.Name = itemvm.Name;
                item.CategoryID = item.Category.CategoryID;
                item.Price = itemvm.Price;
                item.Quantity = itemvm.Quantity;
                item.Descrption = itemvm.Description;
                item.DateCreated = DateTime.Now;
                item.Option = itemvm.Option;
                item.Staff = db.Staffs.Find(itemvm.StaffID);
                if (ModelState.IsValid)
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(itemvm);
        }

        // GET: Procedures/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory item = db.Inventories.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Procedures/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(long id)
        {
            Inventory item = db.Inventories.Find(id);
            db.Inventories.Remove(item);
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

        private void FillCategoryDropDown(InventoryVm itemVm)
        {
            List<SelectListItem> stateList = (from s in db.Categories.AsEnumerable()
                                              select new SelectListItem
                                              {
                                                  Text = $"{s.CategoryName}",
                                                  Value = s.CategoryID.ToString()
                                              }).ToList();
            stateList.Insert(0, new SelectListItem() { Text = "--Select Category--", Value = "" });
            itemVm.CategoryList = stateList;
        }
    }
}
