using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TokenBased.Models
{
    public class MyAuthorizationServiceProvider:OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        {
            return base.GrantAuthorizationCode(context);
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if(context.UserName=="admin" && context.Password == "1234")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role,"admin"));
                identity.AddClaim(new Claim("username","admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name,"Tanvir"));
                context.Validated(identity);
            }
            else if(context.UserName=="user"&& context.Password=="1234")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("username", "user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Tanvir"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("Invalid Grant","Provided Username and Password is invalid");
                return;
            }
            //return base.GrantResourceOwnerCredentials(context);
        }
    }
}