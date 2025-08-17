using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IDataRepository _dataRepository;
        public ProfileService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }
        public async Task<GetUserDTO> AddUserAsync(AddUserDTO addUserDTO)
        {
            var user = new User
            {
                Name = addUserDTO.Name,
                Login = addUserDTO.Login,
                Password = addUserDTO.Password
            };

            var res = await _dataRepository.AddUserAsync(user);

            if (res == null)
            {
                throw new InvalidOperationException("Failed to add new user!");
            }
            return new GetUserDTO
            {
                Name = user.Name,
                Login = user.Login,
                Password = user.Password 
            };
        }
        public async Task<GetUserDTO> DeleteUserByLoginAsync(string login)
        {
            var user = await _dataRepository.DeleteUserByLoginAsync(login);
            if (user == null)
            {
                throw new InvalidOperationException($"Failed to delete user with login {login}!");
            }
            return new GetUserDTO
            {
                Login = user.Login,
                Name = user.Name,
                Password = user.Password
            };
        }
        public async Task<GetUserDTO> GetUserByLoginAsync(string login)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);
            if (user == null)
            {
                throw new InvalidOperationException($"Failed to find user with login {login}!");
            }
            return new GetUserDTO
            {
                Login = user.Login,
                Name = user.Name,
                Password = user.Password 
            };
        }
        public async Task<GetUserDTO> UpdateUserAsync(UpdateUserDTO updateUserDTO, string login)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);
            if (user == null)
            {
                throw new InvalidOperationException($"User with login {login} not found!");
            }
            user.Name = updateUserDTO.NewName;
            user.Login = updateUserDTO.NewLogin;
            user.Password = updateUserDTO.NewPassword;

            if (await _dataRepository.UpdateUserAsync(user) == null)
            {
                throw new InvalidOperationException($"Failed to update user with login {login}!");
            }
            
            return new GetUserDTO
                  {
                      Login = user.Login,
                      Name = user.Name,
                      Password = user.Password
                  };
        }
    }
}
