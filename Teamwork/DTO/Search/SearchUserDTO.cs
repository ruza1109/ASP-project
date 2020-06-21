using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamwork.DTO.Search
{
    public class SearchUserDTO
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public RoleDTO Role { get; set; }

    }
}
