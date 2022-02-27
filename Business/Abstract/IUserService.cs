using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        User Login(string username, string password);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        List<User> GetAll();
        User Get(int id);
        

    }
}
