using System.Security.Claims;
using Blunderr.Core.Data.Entities.Users;

namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    public static class ClaimsPrincipalExtensions
    {
        public static UserRole Role(this ClaimsPrincipal claimsPrincipal)
        {
            string roleString = claimsPrincipal.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            if(!Enum.TryParse<UserRole>(roleString, out UserRole role))
                throw new Exception("Role was not provided by authentication scheme");

            return role;
        }

        public static int Id(this ClaimsPrincipal claimsPrincipal)
        {
            string IdString = claimsPrincipal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if(!Int32.TryParse(IdString, out int Id))
                throw new Exception("Id was not provided by authentication scheme");

            return Id;
        }

        public static string Name(this ClaimsPrincipal claimsPrincipal)
        {
            string? name = claimsPrincipal.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            if(name is null) throw new Exception("Name was not provided by authentication scheme");
            return name;
        }
    }
}