using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Models;
using Services.Repositories;

namespace Services.Services
{
    public interface IUsersService
    {
        public Task CreateUser(User user);
        public Task EditUser(User userInDb, User user);
    }
    public class UsersService : IUsersService
    {

        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
        }
        public Task CreateUser(User user)
        {
            return _usersRepository.CreateUser(user);
        }

        public Task EditUser(User userInDb, User user)
        {
            userInDb.FirstName = user.FirstName;
            userInDb.LastName = user.LastName;
            userInDb.Email = user.Email;
            return _usersRepository.EditUser(user);
        }
    }
}
