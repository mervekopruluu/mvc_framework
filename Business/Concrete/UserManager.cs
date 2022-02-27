using Business.Abstract;
using Business.Utilities;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;

        }

        public void Add(User user)
        {
            ValidationTool.Validate(new UserValidator(), user);
            _userDal.Add(user);
        }

        public void Delete(int id)
        {
            User user = _userDal.Get(x => x.Id == id);
            user.Deleted = true;
            _userDal.Update(user);
        }

        public User Get(int id)
        {
            return _userDal.Get(x => x.Id == id);
        }

        public List<User> GetAll()
        {
            return _userDal.GetAll(x => x.Deleted == false);
        }

        public User Login(string username, string password)
        {
            return _userDal.Get(x => x.Email == username && x.Password == password && x.Deleted == false);
        }

       
        public void Update(User user)
        {
            ValidationTool.Validate(new UserValidator(), user);
            _userDal.Update(user);
        }
    }
}