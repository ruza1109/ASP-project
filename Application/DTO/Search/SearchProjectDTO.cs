using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Search
{
    public class SearchProjectDTO : PagedSearch
    {
        public string Name { get; set; }
        public string Deadline { get; set; }
        public string User { get; set; }

    }
}
