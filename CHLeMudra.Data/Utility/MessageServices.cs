using CHLeMudra.Entity;
using System.IO;
using System.Web;


namespace CHLeMudra.Data
{
    public class MessageServices
    { 
        public static bool SendPassword(Usermaster user)
        {

            if (user != null)
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/ForgotPassword.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Name}", user.Name);
                body = body.Replace("{Password}", user.Password);

                //   string AdminEmail = System.Configuration.ConfigurationSettings.AppSettings["Email"].ToString();
                EmailSender email = new EmailSender();
                return email.SendEmail(user.Email, "", "Mechatech : Forgot Password", body);

            }
            return false;

        }
        public static bool SendCorporateRegistartionEmail(Usermaster user)
        {

            if (user != null)
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/CorporateRegistration.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Name}", user.Name);
                body = body.Replace("{Username}", user.UserName);
                body = body.Replace("{Password}", user.Password);

#pragma warning disable CS0618 // 'ConfigurationSettings.AppSettings' is obsolete: 'This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.AppSettings'
                string AdminEmail = System.Configuration.ConfigurationSettings.AppSettings["AdminEmail"].ToString();
#pragma warning restore CS0618 // 'ConfigurationSettings.AppSettings' is obsolete: 'This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.AppSettings'
                EmailSender email = new EmailSender();
                return email.SendEmail(user.Email, AdminEmail, "", "Mechatech : Registration", body);

            }
            return false;

        }
        public static bool SendDriverRegistartionEmail(Usermaster user)
        {

            if (user != null)
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/DriverRegistration.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Name}", user.Name);
                body = body.Replace("{Username}", user.UserName);
                body = body.Replace("{Password}", user.Password);
#pragma warning disable CS0618 // 'ConfigurationSettings.AppSettings' is obsolete: 'This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.AppSettings'
                string AdminEmail = System.Configuration.ConfigurationSettings.AppSettings["AdminEmail"].ToString();
#pragma warning restore CS0618 // 'ConfigurationSettings.AppSettings' is obsolete: 'This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.AppSettings'
                EmailSender email = new EmailSender();
                return email.SendEmail(user.Email, AdminEmail, "", "Mechatech : Corporate Registration", body);

            }
            return false;

        }

    }

}