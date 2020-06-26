using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO.Search
{
    public class SearchUserDTO : PagedSearch
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }

    }
}
