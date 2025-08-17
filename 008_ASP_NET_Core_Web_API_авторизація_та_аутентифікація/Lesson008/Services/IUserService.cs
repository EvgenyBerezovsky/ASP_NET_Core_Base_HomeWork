using Lesson008.Models;

namespace Lesson008.Services
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public bool AddNewUser(User user);
        public bool DeleteUser(User user);
        public User? GetUserByLogin(string login);
    }
}
