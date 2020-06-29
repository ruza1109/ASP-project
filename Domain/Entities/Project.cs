using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }

        //  Project has many Users
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; } = new HashSet<ProjectUser>(); 
        
        //  Project has many Tasks
        public virtual ICollection<Task> Tasks { get; set; }

    }
}
