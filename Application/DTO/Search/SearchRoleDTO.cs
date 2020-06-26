using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO.Search
{
    public class SearchRoleDTO : PagedSearch
    {
        public string Name { get; set; }
    }
}
