using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;

namespace RealEstateWebsite.Controllers
{
    public class SehirController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Sehir
        public ActionResult Index()
        {
            return View(db.Sehirs.ToList());
        }

        // GET: Sehir/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sehir sehir = db.Sehirs.Find(id);
            if (sehir == null)
            {
                return HttpNotFound();
            }
            return View(sehir);
        }

        // GET: Sehir/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sehir/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SehirId,SehirAd")] Sehir sehir)
        {
            if (ModelState.IsValid)
            {
                db.Sehirs.Add(sehir);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sehir);
        }

        // GET: Sehir/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sehir sehir = db.Sehirs.Find(id);
            if (sehir == null)
            {
                return HttpNotFound();
            }
            return View(sehir);
        }

        // POST: Sehir/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SehirId,SehirAd")] Sehir sehir)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sehir).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sehir);
        }

        // GET: Sehir/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sehir sehir = db.Sehirs.Find(id);
            if (sehir == null)
            {
                return HttpNotFound();
            }
            return View(sehir);
        }

        // POST: Sehir/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sehir sehir = db.Sehirs.Find(id);
            db.Sehirs.Remove(sehir);
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
