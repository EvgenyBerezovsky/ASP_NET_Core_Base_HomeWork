using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RegisterUserDTO
    {
        public required string Name { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
       
    }
}
