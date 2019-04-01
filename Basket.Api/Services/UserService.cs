using System;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;

namespace Basket.Api.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userRepository.Get(email);
            if (user==null)
            {
                throw new Exception("User not found");
            }

            return user;
        }

        public async Task CreateUser(string email)
        {
            var isExist = await _userRepository.Get(email);
            if (isExist!=null)
            {
                throw new Exception("User already exist");
            }

            await _userRepository.Create(new User(email));
        }
    }
}