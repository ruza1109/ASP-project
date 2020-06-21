using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamwork.DTO
{
    public class UserDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public RoleDTO Role { get; set; }

        public string UserInfo 
        {
            get => $"Full name: {FullName}, Username: {Username}"; 
        }

    }
}
