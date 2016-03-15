using A11_RBS.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace A11_RBS.Infrastructure.Services
{
    public class UserExtensions
    {
        public static string GetUserId()
        {
            var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    return userIdClaim.Value; 
                }
            }
            return null;


        }

        public static ApplicationUser GetAppUser()
        {

            var us = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser user = us.FindById(GetUserId());
            return user;
        }
    }
}
