using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IBOUser
    {
        //User GetUser(int id);
        User InsertUser(User user);
        bool Exists(string email);
        User AuthenticateCredentials(string username, string password);
        //bool UpdateUser(User user);
    }
}
