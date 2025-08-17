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
    public class UserService : IUserService
    {
        private readonly IDataRepository _dataRepository;
        public UserService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }
        public List<GetUserDTO> GetAll()
        {
            var users = _dataRepository.GetAllUsers();
            if (users == null || !users.Any())
            {
                return new List<GetUserDTO>();
            }

            return users.Select(user => MapUserToGetUserDTO(user)).ToList();
        }

        private GetUserDTO MapUserToGetUserDTO(User user)
        {
            return new GetUserDTO
            {
                Name = user.Name,
                Scores = user.Scores?.Select(score => new GetScoreDTO
                {
                    Time = score.Time,
                    Topic = score.Topic,
                    Result = score.Result
                }).ToList()
            };
        }
    }
}
