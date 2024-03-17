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
    public class MahalleController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Mahalles
        public ActionResult Index()
        {
            var mahalles = db.Mahalles.Include(m => m.Semt);
            return View(mahalles.ToList());
        }

        // GET: Mahalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mahalle mahalle = db.Mahalles.Find(id);
            if (mahalle == null)
            {
                return HttpNotFound();
            }
            return View(mahalle);
        }

        // GET: Mahalles/Create
        public ActionResult Create()
        {
            ViewBag.SemtId = new SelectList(db.Semts, "SemtId", "SemtAd");
            return View();
        }

        // POST: Mahalles/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MahalleId,MahalleAd,SemtId")] Mahalle mahalle)
        {
            if (ModelState.IsValid)
            {
                db.Mahalles.Add(mahalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SemtId = new SelectList(db.Semts, "SemtId", "SemtAd", mahalle.SemtId);
            return View(mahalle);
        }

        // GET: Mahalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mahalle mahalle = db.Mahalles.Find(id);
            if (mahalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.SemtId = new SelectList(db.Semts, "SemtId", "SemtAd", mahalle.SemtId);
            return View(mahalle);
        }

        // POST: Mahalles/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MahalleId,MahalleAd,SemtId")] Mahalle mahalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mahalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SemtId = new SelectList(db.Semts, "SemtId", "SemtAd", mahalle.SemtId);
            return View(mahalle);
        }

        // GET: Mahalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mahalle mahalle = db.Mahalles.Find(id);
            if (mahalle == null)
            {
                return HttpNotFound();
            }
            return View(mahalle);
        }

        // POST: Mahalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mahalle mahalle = db.Mahalles.Find(id);
            db.Mahalles.Remove(mahalle);
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
