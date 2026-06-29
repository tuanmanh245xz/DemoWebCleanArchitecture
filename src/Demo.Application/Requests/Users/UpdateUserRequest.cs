using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Requests.Users
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public int Phone { get; set; }
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string Region { get; set; } = "";
        public string PostalCode { get; set; } = "";
    }
}
