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
    public class OrdersController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        Order o = new Order();
        
        public ActionResult AddCart(int? id)
        {
            try
            {
                Utents utent = db.Utents.Where(u => u.Username == User.Identity.Name).First();
                o.IdUtent = utent.IdUtent;
                o.IdProducts = id.Value;
                if (o.Quantity == 0)
                {
                    o.Quantity = 1;
                }
                db.Order.Add(o);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }catch (Exception ex) 
            {
                return View(ex.Message);
            }
        }

        public ActionResult Index()
        {
            double total = 0;
            Utents utent = db.Utents.Where(u => u.Username == User.Identity.Name).First();
            var order = db.Order.Where(o => o.IdUtent == utent.IdUtent).ToList();
            foreach (Order ord in order)
            {
                Products pro = db.Products.Where(p => p.IDProduct == ord.IdProducts).First();
                double totale = Convert.ToDouble(pro.Cost * ord.Quantity);
                total += totale;
            }
            ViewBag.Total = total;
            return View(order);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducts = new SelectList(db.Products, "IDProduct", "NameProducts", order.IdProducts);
            ViewBag.IdUtent = new SelectList(db.Utents, "IdUtent", "Username", order.IdUtent);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDOrder,IdProducts,Quantity,IdUtent")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducts = new SelectList(db.Products, "IDProduct", "NameProducts", order.IdProducts);
            ViewBag.IdUtent = new SelectList(db.Utents, "IdUtent", "Username", order.IdUtent);
            return View(order);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCart()
        {
            
            Utents utent = db.Utents.Where(u => u.Username == User.Identity.Name).First();
            var o = db.Order.Where(ord => ord.IdUtent == utent.IdUtent).ToList();
            db.Order.RemoveRange(o);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Confirmed()
        {
            try
            {
                Utents utent = db.Utents.Where(u => u.Username == User.Identity.Name).First();
                var o = db.Order.Where(ord => ord.IdUtent == utent.IdUtent).ToList();
                if (o.Count() > 0)
                {
                    db.Order.RemoveRange(o);
                    db.SaveChanges();
                    ViewBag.Confirmed = "Complimenti hai conseguito l'acquisto con successo";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorCart = "Il carrello è vuoto";
                    return RedirectToAction("Index");
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }


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
