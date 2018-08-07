using BLL.Interface;
using Component.Utility;
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
        public IBOSettings _bLLSettings { get; set; }

        public BOUser(DataContext dbContext, IBOSettings bLLSettings)
        {
            _dbContext = dbContext;
            _bLLSettings = bLLSettings;
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
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == username && x.Password == password);
                //BLLSettings sett = new BLLSettings();
                if (user != null)
                {
                    user.Settings = _bLLSettings.LoadSettings();
                    return user;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ChangeUserPassword(int User_Id, string OldPassword, string NewPassword)
        {
            try
            {
                var OldhashPass = CryptoHelper.Hash(OldPassword);
                var NewhashPass = CryptoHelper.Hash(NewPassword);

                var user = _dbContext.Users.FirstOrDefault(x => x.Id == User_Id && x.Password == OldhashPass);
                if (user == null)
                    return false;
                else
                { user.Password = NewhashPass; _dbContext.SaveChanges(); return true; }

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public User VerifyUser(int User_Id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == User_Id);
            if (user != null)
            {
                user.Status = Convert.ToInt16(Component.Utility.UserStatus.Active);
                _dbContext.SaveChanges();
                return user;
            }
            else
                return null;

        }
        public bool UpdateNotificationStatus(int User_Id, bool Status)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == User_Id);
            if (user != null)
            {
                user.IsNotificationsOn = Status;
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;

        }


        //public User SocialLogin(string accessToken, int? socialLoginType)
        //{
        //    if (socialLoginType.HasValue && !string.IsNullOrEmpty(accessToken))
        //    {
        //        SocialLogins socialLogin = new SocialLogins();
        //        // send access token and social login type to GetSocialUserData in return it will give you full name email and profile picture of user 
        //        var socialUser = socialLogin.GetSocialUserData(accessToken, (SocialLogins.SocialLoginType)socialLoginType.Value);

        //        if (socialUser != null)
        //        {
        //            // if user have privacy on his / her email then we will create email address from his user Id which will be send by mobile developer 
        //            if (string.IsNullOrEmpty(socialUser.Result.email))
        //            {
        //                //socialUser.email = socialUser.id + "@gmail.com";
        //            }
        //            //var existingUser = _dbContext.Users.FirstOrDefault(x=>x.Email==socialUser.Result.) //ctx.Users.Include(x => x.UserSubscriptions).Include(x => x.UserAddresses).Include(x => x.PaymentCards).FirstOrDefault(x => x.Email == socialUser.email);

        //            //if (existingUser != null)
        //            //{
        //            //    // if user already have registered through social login them wee will always check his picture and name just to get updated values of that user from facebook 
        //            //    existingUser.ProfilePictureUrl = socialUser.picture;
        //            //    existingUser.FullName = socialUser.name;
        //            //    ctx.SaveChanges();
        //            //    await existingUser.GenerateToken(Request);
        //            //    AppSettings.LoadSettings();
        //            //    existingUser.BasketSettings = AppSettings.Settings;
        //            //    CustomResponse<User> response = new CustomResponse<User> { Message = Global.ResponseMessages.Success, StatusCode = (int)HttpStatusCode.OK, Result = existingUser };
        //            //    return Ok(response);
        //            //}
        //            //else
        //            //{
        //            //    int SignInType = 0;
        //            //    if (socialLoginType.Value == (int)BasketApi.SocialLogins.SocialLoginType.Google)
        //            //    {
        //            //        SignInType = (int)Utility.SocialLoginType.Google;
        //            //    }
        //            //    else if (socialLoginType.Value == (int)BasketApi.SocialLogins.SocialLoginType.Facebook)
        //            //        SignInType = (int)Utility.SocialLoginType.Facebook;


        //            //    var newUser = new User { FullName = socialUser.name, Email = socialUser.email, ProfilePictureUrl = socialUser.picture, SignInType = SignInType, Status = 1, IsNotificationsOn = true };
        //            //    ctx.Users.Add(newUser);
        //            //    ctx.SaveChanges();
        //            //    await newUser.GenerateToken(Request);
        //            //    AppSettings.LoadSettings();
        //            //    newUser.BasketSettings = AppSettings.Settings;

        //            //    HostingEnvironment.QueueBackgroundWorkItem(cancellationToken =>
        //            //    {
        //            //        sendJoiningEmail(socialUser.email);
        //            //    });
        //            //    return Ok(new CustomResponse<User> { Message = Global.ResponseMessages.Success, StatusCode = (int)HttpStatusCode.OK, Result = newUser });
        //            //        }

        //            //}
        //            //else
        //            //return BadRequest("Unable to get user info");
        //        }
        //        //else
        //        //return BadRequest("Please provide access token along with social login type");
        //    }
        //    return null;
        //}
    }
}
