using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IProfileService
    {
        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="user">The user object to be added.</param>
        /// <returns>The login of the added user, or null if the user already exists.</returns>
        public Task<GetUserDTO> AddUserAsync(AddUserDTO user);

        /// <summary>
        /// Updates an existing user's information.
        /// </summary>
        /// <param name="user">The user object with updated information.</param>
        /// <returns>The updated user object, or null if the user was not found.</returns>
        public Task<GetUserDTO> UpdateUserAsync(UpdateUserDTO user, string login);

        /// <summary>
        /// Retrieves a user by their login.
        /// </summary>
        /// <param name="login">The login of the user to retrieve.</param>
        /// <returns>The user object if found, otherwise null.</returns>
        public Task<GetUserDTO> GetUserByLoginAsync(string login);

        /// <summary>
        /// Deletes a user by their login.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>The user object to delete if found, otherwise null</returns>
        public Task<GetUserDTO> DeleteUserByLoginAsync(string login);
    }
}
