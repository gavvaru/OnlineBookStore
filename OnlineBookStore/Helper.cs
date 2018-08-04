using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookStore
{
    public class Helper
    {
        public static string UserData
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }
       
       
        public static int UserId
        {
            get
            {
                return Convert.ToInt32(UserData.Split('^')[0]);
            }

        }
        public static string UserName
        {
            get
            {
                return UserData.Split('^')[3];
            }
        }
        public static string Firstname
        {
            get
            {
                return UserData.Split('^')[4];
            }
        }
        public static string LastName
        {
            get
            {
                return UserData.Split('^')[5];
            }
        }
        public static string EmailId
        {
            get
            {
                return UserData.Split('^')[6];
            }
        }
        public static string Password
        {
            get
            {
                return UserData.Split('^')[7];
            }
        }
        
    }
}