using BLL.Interface;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Implementation
{
    public class BOUser : IBOUser
    {
        public DataContext _dbContext { get; set; }

        public BOUser(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(string email)
        {
            try
            {
                return _dbContext.Users.Any(x => x.Email == email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User InsertUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User AuthenticateCredentials(string username, string password)
        {
            try
            {
                return _dbContext.Users.FirstOrDefault(x => x.Email == username && x.Password == password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
