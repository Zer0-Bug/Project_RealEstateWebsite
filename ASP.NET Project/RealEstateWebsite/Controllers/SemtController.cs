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
    public class SemtController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Semt
        public ActionResult Index()
        {
            var semts = db.Semts.Include(s => s.Sehir);
            return View(semts.ToList());
        }

        // GET: Semt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semt semt = db.Semts.Find(id);
            if (semt == null)
            {
                return HttpNotFound();
            }
            return View(semt);
        }

        // GET: Semt/Create
        public ActionResult Create()
        {
            ViewBag.SehirId = new SelectList(db.Sehirs, "SehirId", "SehirAd");
            return View();
        }

        // POST: Semt/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SemtId,SemtAd,SehirId")] Semt semt)
        {
            if (ModelState.IsValid)
            {
                db.Semts.Add(semt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SehirId = new SelectList(db.Sehirs, "SehirId", "SehirAd", semt.SehirId);
            return View(semt);
        }

        // GET: Semt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semt semt = db.Semts.Find(id);
            if (semt == null)
            {
                return HttpNotFound();
            }
            ViewBag.SehirId = new SelectList(db.Sehirs, "SehirId", "SehirAd", semt.SehirId);
            return View(semt);
        }

        // POST: Semt/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SemtId,SemtAd,SehirId")] Semt semt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SehirId = new SelectList(db.Sehirs, "SehirId", "SehirAd", semt.SehirId);
            return View(semt);
        }

        // GET: Semt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semt semt = db.Semts.Find(id);
            if (semt == null)
            {
                return HttpNotFound();
            }
            return View(semt);
        }

        // POST: Semt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Semt semt = db.Semts.Find(id);
            db.Semts.Remove(semt);
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
