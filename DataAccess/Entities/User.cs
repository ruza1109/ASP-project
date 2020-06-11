using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // User has one Role
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        // User has many Projects
        public virtual ICollection<ProjectUser> UserProjects { get; set; } = new HashSet<ProjectUser>();

        // TODO User has one created Project
    }
}
