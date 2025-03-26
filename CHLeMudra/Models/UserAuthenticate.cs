using CHLeMudra.Data;
using CHLeMudra.Entity;

using System;
using System.Web;

namespace CHLeMudra.Models
{
    public class UserAuthenticate
    {
        public static int UserId
        {
            get
            {
                return Convert.ToInt32(GetUserDetailsFromCookie(1));
            }
        }

        public static string UserName
        {
            get
            {
                return GetUserDetailsFromCookie(4);
            }
        }
        public static string Name
        {
            get
            {
                return GetUserDetailsFromCookie(3);

            }
        }
        public static bool IsAuthenticated
        {
            get
            {
                var User = GetUserDetailsFromCookie();
                return User == null ? false : true;
            }
        }
        public static int Role
        {
            get
            {
                return Convert.ToInt32(GetUserDetailsFromCookie(2));
            }
        }
        public static Usermaster GetUser(int UserId)
        {
            IUserRepository _account = new UserRepository(); ;
            return _account.GetUser(UserId);
        }
        public static string GetUserDetailsFromCookie(int index)
        {
            if (HttpContext.Current.Request.Cookies["CHLeMudra_UserSession"] != null)
            {
                string CookieValue = CommonHelper.Decrypt(HttpContext.Current.Request.Cookies["CHLeMudra_UserSession"].Value);

                if (!string.IsNullOrEmpty(CookieValue))
                {
                    string[] Values = CookieValue.Split('!');
                    if (Values.Length > 2)
                    {
                        if (Values[0] == HttpContext.Current.Session.SessionID)
                        {
                            return Values[index];
                        }
                    }
                }
            }
            return null;
        }
        public static Usermaster GetUserDetailsFromCookie()
        {
            if (HttpContext.Current.Request.Cookies["CHLeMudra_UserSession"] != null)
            {
                string CookieValue = CommonHelper.Decrypt(HttpContext.Current.Request.Cookies["CHLeMudra_UserSession"].Value);
                //   string CookieValue = HttpContext.Current.Request.Cookies["CHLeMudra_UserSession"].Value;
                if (!string.IsNullOrEmpty(CookieValue))
                {
                    string[] Values = CookieValue.Split('!');
                    if (Values.Length > 2)
                    {
                        if (Values[0] == HttpContext.Current.Session.SessionID)
                        {
                            IUserRepository _account = new UserRepository();
                            int UserId = Convert.ToInt32(Values[1]);
                            return _account.GetUser(UserId);
                        }
                    }
                }
            }
            return null;
        }


        public static void Logout(HttpContext context)
        {
            if (context.Request.Cookies["CHLeMudra_UserSession"] != null)
            {
                context.Response.Cookies["CHLeMudra_UserSession"].Expires = DateTime.Now.AddDays(-1);

            }

        }
    }
}