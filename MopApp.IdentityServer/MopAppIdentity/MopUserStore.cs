using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Services.Models;

namespace MopApp.IdentityServer.MopAppIdentity
{
    public class MopUserStore : IUserStore<MopUser>, IUserPasswordStore<MopUser>
    {
        private readonly moptaskDBContext context;
        private IHttpContextAccessor httpContextAccessor;
        private IConfiguration configuration;
        private MopUser mopUser = new MopUser();

        public MopUserStore(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, moptaskDBContext context)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.context = context;
        }

        public Task<string> GetUserIdAsync(MopUser mopUser, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(mopUser.Id);
        }

        public Task<string> GetUserNameAsync(MopUser mopUser, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(mopUser.UserName);
        }

        public Task SetUserNameAsync(MopUser mopUser, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(MopUser mopUser, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(MopUser mopUser, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(MopUser mopUser, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(MopUser mopUser, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(MopUser mopUser, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<MopUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            User user = context.Users.Where(u => u.Id.ToString().ToLower() == userId.ToLower()).FirstOrDefaultAsync().Result;
            mopUser.Id = user.Id.ToString();
            mopUser.Email = user.Email;
            mopUser.UserName = user.FirstName;
            mopUser.PasswordHash = new PasswordHasher<MopUser>().HashPassword(mopUser, user.Password);

            return Task.FromResult(mopUser);
        }

        public Task<MopUser> FindByNameAsync(string email, CancellationToken cancellationToken)
        {
            User user = context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync().Result;
            mopUser.Id = user.Id.ToString();
            mopUser.Email = user.Email;
            mopUser.PasswordHash = new PasswordHasher<MopUser>().HashPassword(mopUser, user.Password);

            return Task.FromResult(mopUser);
        }

        public void Dispose()
        {

        }

        public Task SetPasswordHashAsync(MopUser user, string passwordHash, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(user.PasswordHash = passwordHash);
        }

        public Task<string> GetPasswordHashAsync(MopUser user, CancellationToken cancellationToken)
        {
            return user.PasswordHash != null ? Task.FromResult<string>(user.PasswordHash.ToString()) : null;
        }

        public Task<bool> HasPasswordAsync(MopUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
