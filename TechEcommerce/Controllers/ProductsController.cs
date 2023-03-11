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
    public class ProductsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        List<Products> p = new List<Products>();
        

       public ActionResult AccessoriG() 
        {
            try
            {
                p.Clear();
                if(p.Count == 0)
                {
                    Products pro = new Products();
                    return View(pro.GetIdG());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AccessoriS()
        {
            try {
                p.Clear();
                if (p.Count == 0)
                {
                    Products pro = new Products();
                    return View(pro.GetIdS());
                } else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Componenti()
        {
            try
            {
                p.Clear();
                if (p.Count == 0)
                {
                    Products pro = new Products();
                    List<Products> prod = pro.GetIdC();
                    return View(prod);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SpecifyId(int? id)
        {
            var product = db.Products.Where(a => a.IdTypeProducts == id).ToList();
            return View(product);
        }

        [Authorize(Roles = "1")]
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.TypeProducts);
            return View(products.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        [Authorize(Roles = "1")]
        public ActionResult Add()
        {
            ViewBag.IdTypeProducts = new SelectList(db.TypeProducts, "IDTypeProducts", "NameType");
            return View();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "NameProducts,CodeProducts,ImgProducts,Cost,ExitDate,ProductDescription,IdTypeProducts")] Products products, HttpPostedFileBase uploadedImg)
        {
            if (!ModelState.IsValid)
            {
                if (uploadedImg == null)
                {
                    ViewBag.UploadErr = "Carica un'immagine";  
                    return View(products);
                }
                else
                {
                    products.ImgProducts = uploadedImg.FileName;
                    string path = Server.MapPath("~/Content/Img-Card/" + uploadedImg.FileName);
                    uploadedImg.SaveAs(path);
                }
            
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTypeProducts = new SelectList(db.TypeProducts, "IDTypeProducts", "NameType", products.IdTypeProducts);
            return View(products);
        }

        [Authorize(Roles = "1")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTypeProducts = new SelectList(db.TypeProducts, "IDTypeProducts", "NameType", products.IdTypeProducts);
            return View(products);
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDProduct,NameProducts,CodeProducts,ImgProducts,Cost,ExitDate,ProductDescription,IdTypeProducts")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTypeProducts = new SelectList(db.TypeProducts, "IDTypeProducts", "NameType", products.IdTypeProducts);
            return View(products);
        }

        [Authorize(Roles = "1")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        [Authorize(Roles = "1")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
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
