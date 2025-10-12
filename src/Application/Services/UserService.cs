using Application.Interfaces;
using Application.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var userList = await _userRepository.GetUsers();
            return UserDTO.CreateList(userList);
        }
    }
}
