using EMS.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EMS.Web.Services
{
    public class EmsUserClaimsPrincipalFactory<TUser, TRole> : UserClaimsPrincipalFactory<TUser, TRole>
        where TUser : class
        where TRole : class
    {
        public EmsUserClaimsPrincipalFactory(UserManager<TUser> userManager, RoleManager<TRole> roleManager, IOptions<IdentityOptions> optionsAccessor) :
            base(userManager, roleManager, optionsAccessor)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var principal = await base.CreateAsync(user);

            var userId = await UserManager.GetUserIdAsync(user);

            var emsUser = await UserManager.FindByIdAsync(userId) as User;

            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Surname, emsUser.LastName));
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.GivenName, emsUser.FirstName));

            return principal;
        }
    }
}
