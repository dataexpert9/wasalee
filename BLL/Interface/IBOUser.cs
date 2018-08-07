using Component.Models;
using Component.Utility.Enums;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IBOUser
    {
        //User GetUser(int id);
        User InsertUser(RegisterBindingModel user, CultureType culture);
        bool Exists(string email);
        User AuthenticateCredentials(string username, string password,CultureType culture);
        //bool UpdateUser(User user);
        bool ChangeUserPassword(int User_Id,string Password,string NewPassword);

        bool UpdateNotificationStatus(int User_Id, bool Status);

        //User SocialLogin(string accessToken,int socialLoginType);

        User VerifyUser(int User_Id);
    }
}
