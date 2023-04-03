using Microsoft.AspNetCore.Authentication;

namespace Blunderr.Web.Authentication
{
    public class TokenAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public const string Name = "TokenAuthentication";

        public const string CookieName = "BLUNDERRAUTH";
    }
}