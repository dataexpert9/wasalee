using BLL.Interface;
using System;
using DAL;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Component.Utility.Enums;
using Component.Utility;
using Component.Models;
using Component.Models.Driver;

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

        public Driver InsertDriver(RegisterDriverBindingModel model, CultureType culture)
        {
            try
            {
                var driver = new Driver
                {
                    Email = model.Email,
                    LicenseNo = model.LicenseNo,
                    PhoneNo = model.PhoneNo,
                    DateOfBirth = model.DateOfBirth,
                    Password = CryptoHelper.Hash(model.Password),
                    IsNotificationsOn = true,
                    SignInType = (int)UserTypes.Driver,
                    CreatedDate = DateTime.UtcNow,
                    IsAvailable = true,
                    BriefInfo = model.BriefIntro,
                    FullName = model.FullName,
                    HomeAddress = model.HomeAddress,
                    WorkHistory = model.WorkHistory
                };

                _dbContext.Drivers.Add(driver);
                _dbContext.SaveChanges();

                return driver;
            }
            catch (Exception ex)
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

        public List<RequestItem> GetAllRequests(int Items, int Page)
        {
            return _dbContext.RequestItem.Include(x => x.RequestItemML).Include(x => x.RequestItemImages).Include(x => x.Driver).Where(x => x.Status == (int)RequestItemStatus.Requested && x.IsDeleted == false).Skip(Items * Page).Take(Items).OrderBy(x => x.Id).ToList();
        }

        public List<RequestItem> GetRiderRequests(int Driver_Id, int Items, int Page)
        {
            return _dbContext.RequestItem.Include(x => x.RequestItemML).Include(x => x.RequestItemImages).Include(x => x.Driver).Where(x => (x.Status == (int)RequestItemStatus.Completed || x.Status == (int)RequestItemStatus.Delivered) && x.IsDeleted == false && x.Driver_Id == Driver_Id).Skip(Items * Page).Take(Items).OrderBy(x => x.Id).ToList();
        }

        public bool CancelBooking(int Driver_Id, int Request_Id)
        {
            //  pending request is when rider will accept request but didnt delivered
            // requested is when user submit his/her request but not yet accepted by rider
            var item = _dbContext.RequestItem.FirstOrDefault(x => x.Id == Request_Id && x.Status == (int)RequestItemStatus.Pending && x.IsDeleted == false);
            if (item != null)
            {
                item.Status = (int)RequestItemStatus.Cancelled;
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public RequestItem AcceptRequest(int Driver_Id, int Request_Id)
        {
            var Request = _dbContext.RequestItem.Include(x => x.RequestItemML).Include(x => x.Driver).Include(x => x.RequestItemImages).FirstOrDefault(x => x.Id == Request_Id && x.Status == (int)RequestItemStatus.Pending);
            if (Request.Driver_Id.HasValue && Request.Driver_Id != null)
            {
                return null;
            }
            if (Request != null)
            {
                Request.Driver_Id = Driver_Id;
                Request.Status = (int)RequestItemStatus.Pending;
                _dbContext.SaveChanges();
                return Request;
            }
            else
                return null;
        }

        public Driver UpdateAvailabilityStatus(int Driver_Id, bool IsAvailable)
        {
            var Driver = _dbContext.Drivers.FirstOrDefault(x => x.Id == Driver_Id);

            if (Driver != null)
            {
                Driver.IsAvailable = IsAvailable;
                _dbContext.SaveChanges();
                return Driver;
            }
            else
                return null;

        }

        public bool UpdateNotificationStatus(int Driver_Id, bool IsNotificationOn)
        {
            var driver = _dbContext.Drivers.FirstOrDefault(x => x.Id == Driver_Id);
            if (driver != null)
            {
                driver.IsNotificationsOn = IsNotificationOn;
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool ChangeDriverPassword(int Driver_Id, string Password, string NewPassword)
        {

            var Driver = _dbContext.Drivers.FirstOrDefault(x => x.Id == Driver_Id);

            var hashPass = CryptoHelper.Hash(Password);
            var NewhashPass = CryptoHelper.Hash(NewPassword);

            if (Driver.Password == hashPass)
            {
                Driver.Password = NewhashPass;
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Driver GetDriverProfile(int Driver_Id)
        {
            var Driver = _dbContext.Drivers.FirstOrDefault(x => x.Id == Driver_Id);
            if (Driver != null)
                return Driver;
            else
                return null;
        }

        public DriverRating RateDriver(RateDriverBindingModel model, CultureType culture)
        {

            try
            {
                var rating = _dbContext.DriverRating.Add(new DriverRating
                {
                    Driver_Id = model.Driver_Id,
                    Rating = model.Rating,
                    Reason = model.Reason,
                    RatedAt = DateTime.UtcNow,
                    User_Id = model.User_Id,
                    Type = (int)RatingTypes.RateDriver
                });

                var Request = _dbContext.RequestItem.FirstOrDefault(x => x.Id == model.Request_Id);
                if (Request != null)
                    Request.IsUserRated = true;

                _dbContext.SaveChanges();
                return _dbContext.DriverRating.FirstOrDefault(x => x.Id == rating.Entity.Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Driver GetDriverDetailsById(int Driver_Id, CultureType culture)
        {
            return _dbContext.Drivers.Include(a => a.DriverRating).Include("DriverRating.User").FirstOrDefault(x => x.Id == Driver_Id);
        }

        public bool ReportProblem(ReportProblemBindingModel model)
        {
            var rating = _dbContext.DriverRating.Add(new DriverRating
            {
                Driver_Id = model.Driver_Id,
                RatedAt = DateTime.UtcNow,
                User_Id = model.User_Id,
                Type = (int)RatingTypes.ReportProblem,
                ReportProblemMessage_Id = model.ReportProblemMessage_Id
            });
            _dbContext.SaveChanges();
            return true;
        }

        public List<DriverRating> GetDriverRatings(int Driver_Id, int? Items = 3, int? Page = 0)
        {
            return _dbContext.DriverRating.Include(x => x.User).Where(x => x.Driver_Id == Driver_Id && x.Type == (int)RatingTypes.RateDriver).Skip(Items.Value * Page.Value).Take(Items.Value).OrderBy(x => x.Id).ToList();
        }
        public int GetTotalRatingsOfDriver(int Driver_Id)
        {
            return _dbContext.DriverRating.Count(x => x.Driver_Id == Driver_Id && x.Type == (int)RatingTypes.RateDriver);
        }



    }
}
