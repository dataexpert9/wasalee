using BLL.Interface;
using System;
using DAL;
using System.Linq;

namespace BLL.Implementation
{
    public class BODriver : IBODriver
    {
        public DataContext _dbContext { get; set; }

        public BODriver(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(string email)
        {
            try
            {
                return _dbContext.Drivers.Any(x => x.Email == email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Driver InsertDriver(Driver driver)
        {
            try
            {
                _dbContext.Drivers.Add(driver);
                _dbContext.SaveChanges();
                return driver;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Driver AuthenticateCredentials(string username, string password)
        {
            try
            {
                return _dbContext.Drivers.FirstOrDefault(x => x.Email == username && x.Password == password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
