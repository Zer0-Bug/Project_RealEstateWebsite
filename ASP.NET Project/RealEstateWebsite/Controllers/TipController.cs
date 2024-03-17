using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;

namespace Emlak.Controllers
{
    public class TipController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Tip
        public ActionResult Index()
        {
            var tips = db.Tips.Include(t => t.Durum);
            return View(tips.ToList());
        }
        public PartialViewResult DurumAd1()
        {
            var durumad1 = db.Tips.Where(i => i.DurumId == 1).FirstOrDefault();
            return PartialView(durumad1);
        }
        public PartialViewResult DurumAd2()
        {
            var durumad2 = db.Tips.Where(i => i.DurumId == 2).FirstOrDefault();
            return PartialView(durumad2);
        }
        public PartialViewResult DurumTip1()
        {
            var durumtip1 = db.Tips.Where(i => i.DurumId == 1).ToList();
            return PartialView(durumtip1);
        }
        public PartialViewResult DurumTip2()
        {
            var durumtip2 = db.Tips.Where(i => i.DurumId == 2).ToList();
            return PartialView(durumtip2);
        }

        // GET: Tip/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = db.Tips.Find(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            return View(tip);
        }

        // GET: Tip/Create
        public ActionResult Create()
        {
            ViewBag.DurumId = new SelectList(db.Durums, "DurumId", "DurumAd");
            return View();
        }

        // POST: Tip/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipId,TipAd,DurumId")] Tip tip)
        {
            if (ModelState.IsValid)
            {
                db.Tips.Add(tip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DurumId = new SelectList(db.Durums, "DurumId", "DurumAd", tip.DurumId);
            return View(tip);
        }

        // GET: Tip/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = db.Tips.Find(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            ViewBag.DurumId = new SelectList(db.Durums, "DurumId", "DurumAd", tip.DurumId);
            return View(tip);
        }

        // POST: Tip/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipId,TipAd,DurumId")] Tip tip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DurumId = new SelectList(db.Durums, "DurumId", "DurumAd", tip.DurumId);
            return View(tip);
        }

        // GET: Tip/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = db.Tips.Find(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            return View(tip);
        }

        // POST: Tip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tip tip = db.Tips.Find(id);
            db.Tips.Remove(tip);
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
