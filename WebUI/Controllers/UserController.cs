using Business.Abstract;
using Entities.Concrete;
using WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("users")]
        public ActionResult Index()
        {
            UserModel model = new UserModel
            {
                User = new User(),
            };
            return View(model);
        }
        [Route("users")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(UserModel model)
        {
            try
            {
                Result result = new Result();

                if (ModelState.IsValid)
                {
                    if (model.User.Id == 0)
                    {
                        _userService.Add(model.User);
                    }
                    else
                    {
                        _userService.Update(model.User);
                    }

                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMsg = "Yanlış veri girişi!";
                    UserModel userModel = new UserModel
                    {
                        User = model.User,
                    };
                    return View(userModel);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = "Hata : " + ex.Message.ToString();
                UserModel userModel = new UserModel
                {
                    User = model.User,
                };
                return View(userModel);
            }

        }

        [Route("user/update/{id}")]
        public ActionResult Update(int id)
        {
            UserModel model = new UserModel
            {
                User = _userService.Get(id),
            };
            return View("Index", model);
        }

        [Route("user/delete/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                Result result = new Result();
                int userId = (int.Parse(User.Identity.Name));

                _userService.Delete(id);

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = "Hata : " + ex.Message.ToString();
                UserModel model = new UserModel
                {
                    User = new User(),
                };
                return View("Index", model);
            }
        }
        [ChildActionOnly]
        public PartialViewResult List()
        {
            List<User> users = _userService.GetAll();
            return PartialView(users);
        }

    }
}