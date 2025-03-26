using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHLeMudra.Proxy
{
    public class ValidateLoginProxy
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class ValidateLoginTokenApiModel
    {
        public string AuthToken { get; set; }
        public bool IsPasswordUpdated { get; set; }
    }

    public class ValidateLoginApiModel
    {
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; }
        public int ErrorCode { get; set; }
        public ValidateLoginTokenApiModel Response { get; set; }
    }
}