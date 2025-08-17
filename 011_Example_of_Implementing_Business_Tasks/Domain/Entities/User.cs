using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }   
        public string Password { get; set; }
        public required string Name { get; set; }
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public List<Category> Categories { get; set; } = new List<Category>();

    }
}
