using CHLeMudra.API.Helper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;


namespace CHLeMudra.API.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        //  private readonly IUserService _userService;
        private readonly IOptions<ServiceSettings> _serviceSettings;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,IOptions<ServiceSettings> serviceSettings)
            // IUserService userService)
            : base(options, logger, encoder, clock)
        {
            _serviceSettings = serviceSettings;
            //_userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (_serviceSettings.Value.Environment.ToLower() == "dev")
            {
                var claims1 = new[] {
                    new Claim(ClaimTypes.NameIdentifier, "om"),
                    new Claim(ClaimTypes.Name, "123"),
                };
                var identity1 = new ClaimsIdentity(claims1, Scheme.Name);
                var principal1 = new ClaimsPrincipal(identity1);
                var ticket1 = new AuthenticationTicket(principal1, Scheme.Name);

                return AuthenticateResult.Success(ticket1);
            }

            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            APIUser user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                user = ConstantValues.GetUserList().Where(c => c.UserName == username && c.Password == password).FirstOrDefault();
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (user == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };
            //var claims = new[] {
            //    new Claim(ClaimTypes.NameIdentifier, "om"),
            //    new Claim(ClaimTypes.Name, "123"),
            //};
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }



    }



}