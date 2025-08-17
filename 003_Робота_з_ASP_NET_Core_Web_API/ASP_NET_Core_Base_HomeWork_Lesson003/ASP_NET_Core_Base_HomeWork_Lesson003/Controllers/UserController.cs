using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_Base_HomeWork_Lesson003.Controllers
{
    [Route("[Controller]")]
    public class UserController : Controller
    {
        private static List<User> users = new List<User>()
        {
            new User()
            {
                Id = 1,
                Name = "John Smith",
                Description = "JohnDescription...",
                BirthdayDate = new DateTime(2001, 12, 01)
            },

            new User()
            {
                Id = 2,
                Name = "Dan Mitchell",
                Description = "DanDescription...",
                BirthdayDate = new DateTime(2002, 10, 02)
            },

            new User()
            {
                Id = 3,
                Name = "Lora Sky",
                Description = "LoraDescription...",
                BirthdayDate = new DateTime(2003, 11, 11)
            },

            new User()
            {
                Id = 4,
                Name = "Mat Nikuya",
                Description = "DanDescription...",
                BirthdayDate = new DateTime(2004, 05, 04)
            },

            new User()
            {
                Id = 5,
                Name = "Sam Yopt",
                Description = "SamDescription...",
                BirthdayDate = new DateTime(2005, 06, 11)
            }
        };

        [HttpGet("GetAllUsers")]
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(users);
        }

        [HttpGet("GetAllUsers/{filter}")]
        public ActionResult<List<User>> GetFilteredUsers([FromQuery] UserFilter filter)
        {
            var filteredUsers = users.Where(u =>
                u.Name.Contains(filter.Name) && filter.Date.HasValue && u.BirthdayDate.Date >= filter.Date.Value).ToList();
            
            return Ok(filteredUsers);
        }
        public ActionResult<User> AddNewUser([FromBody] User user)
        {
            users.Add(user);
            return Ok(user);
        }

        [HttpPost("EditUser")]
        public ActionResult<User> EditUser([FromBody] User user)
        {
            var editedUser = users.FirstOrDefault(u => u.Id == user.Id);
            if (editedUser != null)
            {
                editedUser.Name = user.Name;
                editedUser.Description = user.Description;
                editedUser.BirthdayDate = user.BirthdayDate;
                return Ok(editedUser);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("DeleteUserById/{id}")]
        public ActionResult<User> DeleteUserById([FromRoute] int id)
        {
            var removedUsed = users.Where(u => u.Id == id).FirstOrDefault();
            if (removedUsed != null)
            {
                users.Remove(removedUsed);
                return Ok(removedUsed);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
