using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDao
    {
        public static User checkLogin (string email, string password)
        {
            User user = new User();
            try
            {
                using(var context = new PRN231_ProjectContext())
                {
                    user = context.Users.Where(u => u.Password.Equals(password) && u.Email.Equals(email)).FirstOrDefault();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public static string GetRoleByEmail(string email)
        {
            string role = "";
            try
            {
                using (var context = new PRN231_ProjectContext())
                {
                    role = context.Users.Include(u => u.Role).Where(u => u.Email.Equals(email)).Select(u => u.Role.RoleName).FirstOrDefault();
                }
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return role;
        }

        public static User GetUserByEmail(string email)
        {
            User user = new User();
            try
            {
                using (var context = new PRN231_ProjectContext())
                {
                    user = context.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
    }
}
