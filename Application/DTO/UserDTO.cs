using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }

        public RoleDTO Role { get; set; }

        public string UserInfo => $"Full name: {FullName}, Username: {Username}"; 

    }
}
