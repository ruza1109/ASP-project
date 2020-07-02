using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Search
{
    public class SearchLogDTO : PagedSearch
    {
        public string Actor { get; set; }
        public string UseCase { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
