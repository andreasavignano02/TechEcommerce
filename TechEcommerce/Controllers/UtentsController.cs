using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TechEcommerce;

namespace TechEcommerce.Controllers
{
    public class UtentsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var utents = db.Utents.Include(u => u.Rules);
            return View(utents.ToList());
        }

        public ActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Utents user)
        {
            var count = db.Utents.Count(u => u.Username == user.Username && u.Password == user.Password);


            if (count == 1)
            {
                Utents current = db.Utents.Where(u => u.Username == user.Username && u.Password == user.Password).First();
                FormsAuthentication.SetAuthCookie(current.Username, true);
                if (current.IdRules.ToString() == "1")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    
                    return Redirect(FormsAuthentication.DefaultUrl);
                }
            }
            else
            {
                ViewBag.ErrorAuthentication = "Non esistono queste credenziali prova a registrarti";
                return View();
            }
        }
            public ActionResult SignIn()
            {
                return View();
            }

        [HttpPost]
        public ActionResult SignIn(Utents utents)
        {
            var count = db.Utents.Count(u => u.Username == utents.Username);
            if (count == 0)
            {
                utents.IdRules.ToString("2");
                db.Utents.Add(utents);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ExistUser = "Le credenziali messe sono già in uso";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }




        [Authorize(Roles = "Admin")]
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
    }
}
