using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class UserModel
    {
        public User User { get; set; }
        public List<User> Users { get; set; }

    }
}