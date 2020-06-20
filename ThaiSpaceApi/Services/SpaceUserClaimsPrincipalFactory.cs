using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SpaceServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ThaiSpaceApi.Services
{
    public class SpaceUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<SpaceUser, IdentityRole>
    {
        public SpaceUserClaimsPrincipalFactory(UserManager<SpaceUser> userManager,
                                            RoleManager<IdentityRole> roleManager,
                                            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(SpaceUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            var claims = new List<Claim>();
            if (user.Gender == "Male")
                claims.Add(new Claim(JwtClaimTypes.NickName, "Strong"));
            else
                claims.Add(new Claim(JwtClaimTypes.NickName, "BS"));

            identity.AddClaims(claims);
            return principal;
        }
    }
}
