using CHLeMudra.Data;
using CHLeMudra.Models;
using CHLeMudra.Entity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CHLeMudra.Web.Filters
{
    public class CustomAuthorize : AuthorizeAttribute
    {
#pragma warning disable CS0108 // 'CustomAuthorize.Roles' hides inherited member 'AuthorizeAttribute.Roles'. Use the new keyword if hiding was intended.
        public string Roles { get; set; }
#pragma warning restore CS0108 // 'CustomAuthorize.Roles' hides inherited member 'AuthorizeAttribute.Roles'. Use the new keyword if hiding was intended.
        IUserRepository _user;
        public CustomAuthorize()
        {
            _user = new UserRepository();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (UserAuthenticate.IsAuthenticated)
            {
                var rols = Roles.Split(',');
                string s = Enum.GetName(typeof(Roles), UserAuthenticate.Role);
                if (rols.Contains(s))
                    return;
            }
            base.OnAuthorization(filterContext);
        }
    }
}