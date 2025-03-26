using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CHLeMudra.Helper
{
    public class ConfigSettings
    {
        public static string UserName { get; set; } = ConfigurationManager.AppSettings["UserName"];
        public static string Password { get; set; } = ConfigurationManager.AppSettings["Password"];
        public static string  SecretKey { get; set; } = ConfigurationManager.AppSettings["SecretKey"];       
        public static string eMudraSignApiUrl { get; set; } = ConfigurationManager.AppSettings["eMudraSignApiUrl"];
        public static string Access_Token { get; set; } = ConfigurationManager.AppSettings["Access_Token"];
        public static string TemplateId { get; set; } = ConfigurationManager.AppSettings["TemplateId"];

    }
    

    public class APiHelper
    {
        public static string ValidateLogin { get; set; } = "ValidateLogin";
        public static string InitiateAndSign { get; set; } = "InitiateAndSign";
        public static string InitiateAndSignFlexiForm { get; set; } = "InitiateAndSignFlexiForm";
        public static string DownloadWorkflowDocuments { get; set; } = "DownloadWorkflowDocuments";

    }


}