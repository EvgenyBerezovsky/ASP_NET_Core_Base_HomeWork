using Lesson008.Models;

namespace Lesson008.Services
{

    public class UserService : IUserService
    {
        private static readonly List<User> _users = new List<User>
        {
            new User { Name = "Millie Smith",   Login = "MillieSmith",   Password = "MillieSmith123",   Department = Department.HumanResources,  Position = Position.Manager },
            new User { Name = "Freya Morrison", Login = "FreyaMorrison", Password = "FreyaMorrison123", Department = Department.Sales,           Position = Position.Receptionist },
            new User { Name = "Poppy Gray",     Login = "PoppyGray",     Password = "PoppyGray123",     Department = Department.IT,              Position = Position.Admin },
            new User { Name = "Ella Reid",      Login = "EllaReid",      Password = "EllaReid123",      Department = Department.Marketing,       Position = Position.Manager },
            new User { Name = "Sophie Murray",  Login = "SophieMurray",  Password = "SophieMurray123",  Department = Department.HumanResources,  Position = Position.HR },
            new User { Name = "Isabella Gray",  Login = "IsabellaGray",  Password = "IsabellaGray123",  Department = Department.Administration,  Position = Position.Manager },
            new User { Name = "Millie Foot",    Login = "MillieFoot",    Password = "MillieFoot123",    Department = Department.Finance,         Position = Position.Receptionist },
            new User { Name = "Aria Simpson",   Login = "AriaSimpson",   Password = "AriaSimpson123",   Department = Department.IT,              Position = Position.Manager }
        };
        public bool AddNewUser(User user)
        {
            if (!_users.Exists(u => u.Login == user.Login))
            {
                _users.Add(user);
                return true;
            }
            return false;
        }

        public bool DeleteUser(User user)
        {
            return _users.Remove(user);
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User? GetUserByLogin(string login)
        {
            return _users.FirstOrDefault(u => u.Login == login); 
            
        }
    }
}
