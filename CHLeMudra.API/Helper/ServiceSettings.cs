using System;
using System.Collections.Generic;
using System.Text;

namespace CHLeMudra.API
{
    public class ServiceSettings
    {
        public string EMudraSignApiUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SecretKey { get; set; }
        

        public string Environment { get; set; }
        public string EnquiryEmail { get; set; }      
        public string SMTP_Host { get; set; }
        public string SMTP_UserName { get; set; }
        public string SMTP_Password { get; set; }
        public int SMTP_Port { get; set; }
        public bool SMTP_IsEnableSSL { get; set; }      
    }
}
