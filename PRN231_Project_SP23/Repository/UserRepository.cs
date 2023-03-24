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

        public User GetUserByEmail(string email) => UserDao.GetUserByEmail(email);
        public void DeleteUser(User u) => UserDao.DeleteUser(u);
        public List<User> GetAllUsers() => UserDao.getAllUsers();

        public User GetUserById(int uId) => UserDao.FindUserById(uId);
        public void InsertUser(User u) => UserDao.InsertUser(u);
        public void UpdateProduct(User u) => UserDao.UpdateUser(u);

        public List<Role> GetAllRoles() => RoleDao.GetAllRoles();

        public Role GetRoleById(int roleId) => RoleDao.FindRoleById(roleId);
    }
}
