using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateUserDTO
    {
        public string NewLogin { get; set; }
        public string NewPassword { get; set; }
        public string NewName { get; set; }
    }
}
