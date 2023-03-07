using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechEcommerce;

namespace TechEcommerce.Controllers
{
    public class UtentsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Utents
        public ActionResult Index()
        {
            var utents = db.Utents.Include(u => u.Rules);
            return View(utents.ToList());
        }

        // GET: Utents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utents utents = db.Utents.Find(id);
            if (utents == null)
            {
                return HttpNotFound();
            }
            return View(utents);
        }

        // GET: Utents/Create
        public ActionResult LogIn()
        {
            ViewBag.IdRules = new SelectList(db.Rules, "IDRules", "Rule");
            return View();
        }

        // POST: Utents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind(Include = "IdUtent,Username,Password,IdRules")] Utents utents)
        {
            if (ModelState.IsValid)
            {
                db.Utents.Add(utents);
                db.SaveChanges();
                return RedirectToAction("LogIn");
            }

            ViewBag.IdRules = new SelectList(db.Rules, "IDRules", "Rule", utents.IdRules);
            return View(utents);
        }

        // GET: Utents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utents utents = db.Utents.Find(id);
            if (utents == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRules = new SelectList(db.Rules, "IDRules", "Rule", utents.IdRules);
            return View(utents);
        }

        // POST: Utents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtent,Username,Password,IdRules")] Utents utents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRules = new SelectList(db.Rules, "IDRules", "Rule", utents.IdRules);
            return View(utents);
        }

        // GET: Utents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utents utents = db.Utents.Find(id);
            if (utents == null)
            {
                return HttpNotFound();
            }
            return View(utents);
        }

        // POST: Utents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utents utents = db.Utents.Find(id);
            db.Utents.Remove(utents);
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
