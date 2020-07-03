using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Search
{
    public class SearchTaskLogDTO : PagedSearch
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
