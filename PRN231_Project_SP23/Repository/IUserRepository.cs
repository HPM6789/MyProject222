using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {
        public User checkLogin(string email, string password);
        public string GetRoleByEmail(string email);
    }
}
