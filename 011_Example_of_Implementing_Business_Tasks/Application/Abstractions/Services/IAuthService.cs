using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IAuthService
    {
        public Task<string?> LoginUserAsync(LoginUserDTO loginUserDTO);
    }
}
