using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using MopApp.IdentityServer.MopAppIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MopApp.IdentityServer.MopAppIdentity
{
    public class MopAppProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<MopUser> _claimsFactory;
        private readonly UserManager<MopUser> _userManager;

        public MopAppProfileService(UserManager<MopUser> userManager, IUserClaimsPrincipalFactory<MopUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var mopUser = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(mopUser);
            var claims = principal.Claims.ToList();

            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            claims.Add(new Claim("email", mopUser.Email));
            claims.Add(new Claim("userId", mopUser.Id));

            claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, mopUser.Email));
            claims.Add(new Claim(IdentityServerConstants.StandardScopes.OfflineAccess, "true"));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = true;
        }
    }
}
