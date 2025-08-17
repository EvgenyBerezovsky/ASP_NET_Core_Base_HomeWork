﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AddUserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public required string Name { get; set; }
    }
}
