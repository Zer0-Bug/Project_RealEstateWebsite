using RealEstateWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Emlak.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IlanListesi()
        {
            var ilan = db.Ilans.Include(i => i.Mahalle).Include(i => i.Tip).ToList();
            return View(ilan);
        }
    }
}