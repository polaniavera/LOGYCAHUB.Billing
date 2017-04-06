namespace LOGYCAHUB.Billing.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using DAL.UnitOfWork;
    using Microsoft.Owin.Security.OAuth;

    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            
            var user = _unitOfWork.UserApiRepository.Get(u => u.STR_USER == context.UserName && u.PASSWORD_USER == context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "El nombre de usuario o password es incorrecto");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }
    }
}