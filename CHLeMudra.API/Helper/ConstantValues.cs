using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHLeMudra.API.Helper
{
    public class ConstantValues
    {
        public static IList<APIUser> GetUserList()
        {
            IList<APIUser> users = new List<APIUser>();
            users.Add(new APIUser() {Id=101,UserName="WebUsr",Password="Web@34$#" });
            users.Add(new APIUser() { Id = 102,UserName = "AndroidUsr", Password = "An@39$$" });
            users.Add(new APIUser() { Id = 103, UserName = "IOSUsr", Password = "Os@32$$" });
            return users;
        }


    }

    public class APIUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}



