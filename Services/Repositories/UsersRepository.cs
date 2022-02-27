using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Models;
using Services.Helpers;


namespace Services.Repositories
{

    public interface IUsersRepository
    {
        public Task CreateUser(User user);
        public Task EditUser(User user);
    }
    public class UsersRepository: IUsersRepository
    {
        moptaskDBContext _context;
        public UsersRepository(moptaskDBContext context)
        {
            this._context = context;
        }

        public async Task CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task EditUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }


    }
}
