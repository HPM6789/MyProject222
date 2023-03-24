using BusinessObjects.Models;
using DataAccess;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public User checkLogin(string email, string password) => UserDao.checkLogin(email, password);

        public string GetRoleByEmail(string email) => UserDao.GetRoleByEmail(email);
    }
}
