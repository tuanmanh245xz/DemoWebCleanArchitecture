using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class UserLogin
    {
        public int Id { get; set; }
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public string Roler { get; set; } = "";
    }
}
