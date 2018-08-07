using Component.Models;
using Component.Models.Driver;
using Component.Utility.Enums;
using DAL;
using System.Collections.Generic;

namespace BLL.Interface
{
    public interface IBODriver
    {
        Driver InsertDriver(RegisterDriverBindingModel driver,CultureType culture);
        bool Exists(string email);
        Driver AuthenticateCredentials(string username, string password);
        List<RequestItem> GetAllRequests(int Items,int Page);
        List<RequestItem> GetRiderRequests(int Driver_Id, int Items, int Page);
        bool CancelBooking(int Driver_Id, int Request_Id);
        RequestItem AcceptRequest(int Driver_Id, int Request_Id);
        Driver UpdateAvailabilityStatus(int Driver_Id, bool IsAvailable);
        bool UpdateNotificationStatus(int Driver_Id,bool IsNotificationOn);
        bool ChangeDriverPassword(int Driver_Id,string Password,string NewPassword);
        Driver GetDriverProfile(int Driver_Id);
        //bool RateDriver(RateDriverBindingModel model,CultureType culture);
    }
}
