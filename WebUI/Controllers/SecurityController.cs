using Business.Abstract;
using Entities.Concrete;
using WebUI.Utilities;
using WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebUI.Controllers
{
    public class SecurityController : Controller
    {
        private IUserService _userService;
        private Crypto _crypto;
        public SecurityController(IUserService userService)
        {
            _userService = userService;
            _crypto = new Crypto();
        }
        // GET: Security
        [Route("login")]
        public ActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
            var cookie = Request.Cookies["mvc_project"];
            if (cookie != null)
            {
                var mycookie = _crypto.Decrypt(cookie["ticket"]);
                string[] _user = mycookie.Split('/');

                User user = _userService.Login(_user[0], _user[1]);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Id.ToString(), model.UserRememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Kullanıcı bulunamadı!";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Login(model.UserName, model.UserPassword);
                if (user != null)
                {
                    if (model.UserRememberMe == true)
                    {
                        HttpCookie cookie = new HttpCookie("mvc_project");
                        cookie["ticket"] = _crypto.Encrypt(user.Email + "/" + user.Password);
                        cookie.Expires = DateTime.Now.AddYears(1);

                        Response.Cookies.Add(cookie);
                    }
                    FormsAuthentication.SetAuthCookie(user.Id.ToString(), model.UserRememberMe);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Kullanıcı bulunamadı!";
                    return View(model);
                }
            }
            else
            {
                ViewBag.Message = "Yanlış veri girişi! ";
                return View(model);
            }

        }
        [Route("logoff")]
        public ActionResult Logoff()
        {
            if (GetCookie("mvc_project") != null)
            {
                Response.Cookies["mvc_project"].Expires = DateTime.Now.AddYears(-1);
            }
            FormsAuthentication.SignOut();
            return Redirect("login");
        }

        [Route("recover-password")]
        public ActionResult RecoverPassword()
        {
            return View();
        }
       
        private string GetCookie(string name)
        {
            //Böyle bir cookie mevcut mu kontrol ediyoruz
            if (Request.Cookies.AllKeys.Contains(name))
            {
                //böyle bir cookie varsa bize geri değeri döndürsün
                return Request.Cookies[name].Value;
            }
            return null;
        }

    }
}