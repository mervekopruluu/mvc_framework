using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class HeaderController : Controller
    {
        private IUserService _userService;
        public HeaderController( IUserService userService)
        {
            _userService = userService;
        }
        [ChildActionOnly]
        public PartialViewResult _Menu()
        {
            int userId = int.Parse(User.Identity.Name);
            var user = _userService.Get(userId);
            return PartialView(user);
        }
        [ChildActionOnly]
        public PartialViewResult _Notification()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult _Userbox()
        {
            try
            {
                int userId = int.Parse(User.Identity.Name);
                User user = _userService.Get(userId);
                return PartialView(user);
            }
            catch
            {
                return PartialView(new User() { Name = "Incorrect", Surname = "User" });
            }
        }
    }
}