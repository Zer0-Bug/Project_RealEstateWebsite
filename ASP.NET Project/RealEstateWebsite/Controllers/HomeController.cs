using RealEstateWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using RealEstateWebsite.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Emlak.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;
        public HomeController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);

        }
        // GET: Home
        public ActionResult Index()
        {
            var imgs = db.Resims.ToList();
            ViewBag.imgs = imgs;

            var ilan = db.Ilans.Include(m => m.Mahalle).Include(e => e.Tip).Where(i => i.Confirmation == 1);


            return View(ilan.ToList());
        }
        public ActionResult DurumList(int id)
        {
            var imgs = db.Resims.ToList();
            ViewBag.imgs = imgs;
            var ilan = db.Ilans.Where(i => i.DurumId == id).Include(m => m.Mahalle).Include(e => e.Tip);
            return View(ilan.ToList());
        }
        public ActionResult MenuFiltre(int id)
        {
            var imgs = db.Resims.ToList();
            ViewBag.imgs = imgs;
            var filtre = db.Ilans.Where(i => (i.TipId == id)&& i.Confirmation==1).Include(m => m.Mahalle).Include(e => e.Tip).ToList();
            return View(filtre);
        }

        public PartialViewResult PartialFiltre()
        {
            ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirId", "SehirAd");
            ViewBag.durumlist = new SelectList(DurumGetir(), "DurumId", "DurumAd");
            return PartialView();
        }
        public ActionResult Filtre(int? min, int? max, int? sehirid, int? mahalleid, int? semtid, int? durumid, int? tipid)
        {
            var imgs = db.Resims.ToList();
            ViewBag.imgs = imgs;

            var filtre = db.Ilans.Where(i =>
                (!min.HasValue || i.Fiyat >= min) &&
                (!max.HasValue || i.Fiyat <= max) &&
                (!durumid.HasValue || i.DurumId == durumid) &&
                (!sehirid.HasValue || i.SehirId == sehirid) &&
                (!semtid.HasValue || i.SemtId == semtid) &&
                (!mahalleid.HasValue || i.MahalleId == mahalleid) &&
                (!tipid.HasValue || i.TipId == tipid) && (i.Confirmation ==1)
            ).Include(m => m.Mahalle).Include(e => e.Tip).ToList();

            return View(filtre);
        }
        public List<Sehir> SehirGetir()
        {
            List<Sehir> sehirler = db.Sehirs.ToList();
            return sehirler;
        }
        public ActionResult SemtGetir(int SehirId)
        {
            List<Semt> semtlist = db.Semts.Where(x => x.SehirId == SehirId).ToList();
            ViewBag.semtlistesi = new SelectList(semtlist, "SemtId", "SemtAd");
            return PartialView("SemtPartial");
        }
        public ActionResult MahalleGetir(int SemtId)
        {
            List<Mahalle> mahallelist = db.Mahalles.Where(x => x.SemtId == SemtId).ToList();
            ViewBag.mahallelistesi = new SelectList(mahallelist, "MahalleId", "MahalleAd");
            return PartialView("MahallePartial");
        }
        public List<Durum> DurumGetir()
        {
            List<Durum> durumlar = db.Durums.ToList();
            return durumlar;
        }
        public ActionResult TipGetir(int DurumId)
        {
            List<Tip> tiplist = db.Tips.Where(x => x.DurumId == DurumId).ToList();
            ViewBag.tiplistesi = new SelectList(tiplist, "TipId", "TipAd");
            return PartialView("TipPartial");
        }
        public ActionResult Search(string q)
        {
            var imgs = db.Resims.ToList();
            ViewBag.imgs = imgs;
            var ara = db.Ilans.Include(m => m.Mahalle).Include(e => e.Tip).Where(c => c.Confirmation==1);
            if (!string.IsNullOrEmpty(q))
            {
                ara = ara.Where(i => i.Acıklama.Contains(q) || i.Mahalle.MahalleAd.Contains(q) || i.Tip.TipAd.Contains(q) || i.Adres.Contains(q) || i.Kat.Contains(q) || i.Mahalle.Semt.SemtAd.Contains(q) || i.Mahalle.Semt.Sehir.SehirAd.Contains(q) || i.Tip.Durum.DurumAd.Contains(q));
            }
            return View(ara.ToList());
        }
        public ActionResult Details(int id)
        {
            var ilan = db.Ilans.Where(i => i.IlanId == id).Include(m => m.Mahalle).Include(e => e.Tip).FirstOrDefault();
            var user = UserManager.Users.Where(a => a.UserName.Equals(ilan.UserName)).FirstOrDefault();
            ilan.Email = user.Email;
            var imgs = db.Resims.Where(i => i.IlanId == id).ToList();
            ViewBag.imgs = imgs;
            return View(ilan);
        }
        public PartialViewResult Slider()
        {
            var ilan = db.Ilans.Where(i => i.Confirmation == 1).ToList().Take(5);
            var imgs = db.Resims.ToList();

            ViewBag.imgs = imgs;
            return PartialView(ilan);
        }
    }
}