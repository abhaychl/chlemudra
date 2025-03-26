using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHLeMudra.API.Proxy
{
    public class ValidateLoginProxy
    {
        public bool IsSuccess { get; set; }
        public IList<string> Messages { get; set; }
        public int ErrorCode { get; set; }
        public LoginAuthTokenProxy Response { get; set; }
    }
    public class LoginAuthTokenProxy
    {
        public string AuthToken { get; set; }
        public bool IsPasswordUpdated { get; set; }
        public bool IsPasswordCompliant { get; set; }
        public int ErrorCode { get; set; }

    }
}
