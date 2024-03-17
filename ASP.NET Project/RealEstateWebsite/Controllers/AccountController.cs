using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using RealEstateWebsite.Identity;
using RealEstateWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealEstateWebsite.Controllers
{
    public class AccountController : Controller
    {
        DataContext db = new DataContext();
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;
        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);

        }
        public async Task<ActionResult> GetUsers()
        {
            var allUsers = await UserManager.Users.ToListAsync();

            List<ProfilGuncelleme> myList = new List<ProfilGuncelleme>();
            foreach (var item in allUsers)
            {
                ProfilGuncelleme prf = new ProfilGuncelleme();
                prf.id = item.Id;
                prf.Username = item.UserName;
                prf.Name = item.Name;
                prf.Surname = item.Surname;
                prf.Email = item.Email;
                myList.Add(prf);
            }
            return View(myList);
        }

        public ActionResult UserCreate()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.UserName = model.Username;
                user.Surname = model.Surname;
                user.Email = model.Email;
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("GetUsers");
                }
                else
                {
                    ModelState.AddModelError("fisterUserError", "kullanıcı oluşturma hatası..");
                }
            }
            return View(model);
        }

        public ActionResult UserEdit(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            ProfilGuncelleme profil = new ProfilGuncelleme
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.UserName
            };

            return View(profil);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(ProfilGuncelleme model)
        {
            var user = UserManager.FindById(model.id);
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.UserName = model.Username;
            user.Email = model.Email;
            UserManager.Update(user);
            return RedirectToAction("GetUsers");
        }
        public ActionResult UserDelete(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            ProfilGuncelleme profil = new ProfilGuncelleme
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.UserName
            };

            return View(profil);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public  ActionResult UserDelete(ProfilGuncelleme model)
        {
            var user = UserManager.FindById(model.id);

            if (user != null)
            {
                List<Ilan> findAds = db.Ilans.Where(a => a.UserName.Equals(user.UserName)).ToList();
                if(findAds != null)
                {
                    foreach (var item in findAds)
                    {
                        db.Ilans.Remove(item);
                        db.SaveChanges();
                    }
                    
                }
                
                UserManager.Delete(user);
                
                return RedirectToAction("GetUsers");
            }

            // Kullanıcı bulunamazsa, hata işleme yapılabilir
            return HttpNotFound();
        }




        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.UserName = model.Username;
                user.Surname = model.Surname;
                user.Email = model.Email;
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "kullanıcı oluşturma hatası..");
                }
            }
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(model.Username, model.Password);
                if (user != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties, identityclaims);
                    if (!String.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "There is no such user!");
                }
            }
            return View(model);
        }
        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Profil()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = UserManager.FindById(id);
            var data = new ProfilGuncelleme()
            {
                id = user.Id,
                Name = user.Name,
                Username = user.UserName,
                Surname = user.Surname,
                Email = user.Email
            };
            return View(data);
        }

        [HttpPost]
        public ActionResult Profil(ProfilGuncelleme model)
        {
            var user = UserManager.FindById(model.id);
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.UserName = model.Username;
            user.Email = model.Email;
            UserManager.Update(user);
            return View("Update");
        }

        public ActionResult SifreDegistir()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult SifreDegistir(SifreDegistirme model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                return View("Update");
            }
            return View(model);
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}