using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHLeMudra.API
{
    public class ApiMethod
    {
        public static string ValidateLogin { get; set; } = "ValidateLogin";
        public static string InitiateAndSign { get; set; } = "InitiateAndSign";
        public static string InitiateAndSignFlexiForm { get; set; } = "InitiateAndSignFlexiForm";
        public static string DownloadWorkflowDocuments { get; set; } = "DownloadWorkflowDocuments";
        public static string GetWorkflowInfo { get; set; } = "GetWorkflowInfo";

    }
}
