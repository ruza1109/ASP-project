using Application.DTO.Pagination;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Search
{
    public class SearchTaskDTO : PagedSearch
    {
        public string Name { get; set; }
        public int StoryPoints { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string User { get; set; }
        public string Project { get; set; }

    }
}
