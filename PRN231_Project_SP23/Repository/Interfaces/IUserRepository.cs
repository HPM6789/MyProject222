using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int uId);
        void DeleteUser(User u);
        void InsertUser(User u);
        void UpdateProduct(User u);
        public User checkLogin(string email, string password);
        public string GetRoleByEmail(string email);
        public User GetUserByEmail(string email);
    }
}
