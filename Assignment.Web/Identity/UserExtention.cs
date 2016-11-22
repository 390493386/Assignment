using Assignment.Core.Domain;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace Assignment.Web.Identity
{
    public static class UserExtention
    {
        public static ClaimsIdentity CreateIdentity(this User user)
        {
            ClaimsIdentity userIdentity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            userIdentity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            userIdentity.AddClaim(new Claim("Account", user.Account));
            return userIdentity;
        }
    }
}