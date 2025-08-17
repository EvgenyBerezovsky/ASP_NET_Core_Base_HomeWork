using Application.Abstractions.Repositories;
using Application.DTOs;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InfrastructureTests.Services
{
    public class ProfileServiceTests
    {
        private readonly ProfileService _profileService;
        private readonly Mock<IDataRepository> _dataRepositoryMock;
        public ProfileServiceTests()
        {
            _dataRepositoryMock = new Mock<IDataRepository>();
            _profileService = new ProfileService(_dataRepositoryMock.Object);
        }
        [Fact]
        public async Task AddUserAsync_WhenUserAdded_ShouldReturnCorrectDtoAsync()
        {
            // Arrange
            var newUser = new User
            {
                Name = "Test User",
                Login = "testuser",
                Password = "password123"
            };
            var newAddUserDTO = new AddUserDTO
            {
                Name = newUser.Name,
                Login = newUser.Login,
                Password = newUser.Password
            };
            var expectedGetUserDTO = new GetUserDTO
            {
                Name = newUser.Name,
                Login = newUser.Login,
                Password = newUser.Password
            };

            _dataRepositoryMock.Setup(r => r.AddUserAsync(It.Is<User>(u =>
                u.Name == newUser.Name &&
                u.Login == newUser.Login &&
                u.Password == newUser.Password)))
                .ReturnsAsync(new User
                {
                    Id = 1,
                    Name = "Test User",
                    Login = "testuser",
                    Password = "password123"
                });

            // Act
            var realGetUserDTO = await _profileService.AddUserAsync(newAddUserDTO);

            // Assert
            realGetUserDTO.Should().BeEquivalentTo(expectedGetUserDTO);
        }
        [Fact]
        public async Task AddUserAsync_WhenFailedToAddNewUser_ShouldThrowExceptionAsync()
        {
            // Arrange
            var newUser = new User
            {
                Id = 1,
                Name = "Test User",
                Login = "testuser",
                Password = "password123"
            };
            var newAddUserDTO = new AddUserDTO
            {
                Name = newUser.Name,
                Login = newUser.Login,
                Password = newUser.Password
            };
            string expectedExceptionMessage = "Failed to add new user!";

            _dataRepositoryMock.Setup(r => r.AddUserAsync(newUser)).ReturnsAsync(default(User));

            // Act & Assert
            try
            {
                await _profileService.AddUserAsync(newAddUserDTO);
            }
            catch (Exception ex)
            {

                ex.Should().BeOfType<InvalidOperationException>();
                ex.Message.Should().Be(expectedExceptionMessage);
            }
        }
        [Fact]
        public async Task DeleteUserByLoginAsync_WhenManagedToDelete_ShouldReturnCorrectDtoAsync()
        {
            //Arrange
            var login = "testuser";
            var expectedGetUserDto = new GetUserDTO
            {
                Name = "Test User",
                Login = login,
                Password = "password123"
            };
            _dataRepositoryMock.Setup(r => r.DeleteUserByLoginAsync(login))
                .ReturnsAsync(new User
                {
                    Id = 1,
                    Name = "Test User",
                    Login = login,
                    Password = "password123"
                });

            //Act
            var realGetUserDto = await _profileService.DeleteUserByLoginAsync(login);

            //Assert
            realGetUserDto.Should().BeEquivalentTo(expectedGetUserDto);
        }
        [Fact]
        public async Task DeleteUserByLoginAsync_WhenFailedToDelete_ShouldThrowExeptionAsync()
        {
            //Arrange
            var login = "testuser";
            var expectedErrorMessage = $"Failed to delete user with login {login}!";
            _dataRepositoryMock.Setup(r => r.DeleteUserByLoginAsync(login)).ReturnsAsync(default(User));

            //Act, Assert
            try
            {
                await _profileService.DeleteUserByLoginAsync(login);
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<InvalidOperationException>();
                ex.Message.Should().Be($"Failed to delete user with login {login}!");
            }
        }
        [Fact]
        public async Task GetUserByLoginAsync_WhenUserFound_ShouldReturnCorrectDtoAsync()
        {
            // Arrange
            var login = "testuser";
            var expectedGetUserDto = new GetUserDTO
            {
                Name = "Test User",
                Login = login,
                Password = "password123"
            };
            _dataRepositoryMock.Setup(r => r.GetUserByLoginAsync(login))
                .ReturnsAsync(new User
                {
                    Id = 1,
                    Name = "Test User",
                    Login = login,
                    Password = "password123"
                });
            // Act
            var realGetUserDto = await _profileService.GetUserByLoginAsync(login);
            // Assert
            realGetUserDto.Should().BeEquivalentTo(expectedGetUserDto);
        }
        [Fact]
        public async Task GetUserByLoginAsync_WhenUserNotFound_ShouldThrowExceptionAsync()
        {
            // Arrange
            var login = "testuser";
            var expectedErrorMessage = $"Failed to find user with login {login}!";
            _dataRepositoryMock.Setup(r => r.GetUserByLoginAsync(login)).ReturnsAsync(default(User));
            // Act, Assert
            try
            {
                await _profileService.GetUserByLoginAsync(login);
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<InvalidOperationException>();
                ex.Message.Should().Be(expectedErrorMessage);
            }
        }
        [Fact]
        public async Task UpdateUserAsync_WhenUserUpdated_ShouldReturnCorrectDtoAsync()
        {
            // Arrange
            var login = "testuser";
            var existedUser = new User
            {
                Id = 1,
                Name = "Existed User",
                Login = login,
                Password = "password123"
            };
            var userToUpdate = new User
            {
                Id = 1,
                Name = "Updated User",
                Login = "updateduser",
                Password = "newpassword123"
            };
            var userToUpdateDto = new UpdateUserDTO
            {
                NewName = "Updated User",
                NewLogin = "updateduser",
                NewPassword = "newpassword123"
            };
            var expectedGetUserDto = new GetUserDTO
            {
                Name = userToUpdate.Name,
                Login = userToUpdate.Login,
                Password = userToUpdate.Password
            };

            _dataRepositoryMock.Setup(r => r.GetUserByLoginAsync(login))
                .ReturnsAsync(existedUser);

            _dataRepositoryMock.Setup(r => r.UpdateUserAsync(It.Is<User>(u =>
                u.Id == userToUpdate.Id &&
                u.Name == userToUpdate.Name &&
                u.Login == userToUpdate.Login &&
                u.Password == userToUpdate.Password)))
                .ReturnsAsync(new User
                {
                    Id = 1,
                    Name = "Updated User",
                    Login = "updateduser",
                    Password = "newpassword123"
                });
            // Act
            var realGetUserDto = await _profileService.UpdateUserAsync(userToUpdateDto, login);
;
            // Assert
            realGetUserDto.Should().BeEquivalentTo(expectedGetUserDto);

        }
        [Fact]
        public async Task UpdateUserAsync_WhenUserNotFound_ShouldThrowExceptionAsync()
        {
            // Arrange
            var login = "testuser";
            var userToUpdateDto = new UpdateUserDTO
            {
                NewName = "Updated User",
                NewLogin = "updateduser",
                NewPassword = "newpassword123"
            };
            string expectedErrorMessage = $"User with login {login} not found!";
            _dataRepositoryMock.Setup(r => r.GetUserByLoginAsync(login)).ReturnsAsync(default(User));
            // Act, Assert
            try
            {
                await _profileService.UpdateUserAsync(userToUpdateDto, login);
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<InvalidOperationException>();
                ex.Message.Should().Be(expectedErrorMessage);
            }
        }
        [Fact]
        public async Task UpdateUserAsync_WhenFailedToUpdate_ShouldThrowExceptionAsync()
        {
            // Arrange
            var login = "testuser";
            var existedUser = new User
            {
                Id = 1,
                Name = "Existed User",
                Login = login,
                Password = "password123"
            };
            var userToUpdate = new User
            {
                Id = 1,
                Name = "Updated User",
                Login = "updateduser",
                Password = "newpassword123"
            };
            var userToUpdateDto = new UpdateUserDTO
            {
                NewName = "Updated User",
                NewLogin = "updateduser",
                NewPassword = "newpassword123"
            };
            string expectedErrorMessage = $"Failed to update user with login {login}!";
            _dataRepositoryMock.Setup(r => r.GetUserByLoginAsync(login)).ReturnsAsync(existedUser);
            _dataRepositoryMock.Setup(r => r.UpdateUserAsync(userToUpdate)).ReturnsAsync(default(User));
            // Act, Assert
            try
            {
                await _profileService.UpdateUserAsync(userToUpdateDto, login);
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<InvalidOperationException>();
                ex.Message.Should().Be(expectedErrorMessage);
            }
        }
    }
}
