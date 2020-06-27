using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }

        public IEnumerable<UserDTO> Users { get; set; } = new List<UserDTO>();

    }
}
