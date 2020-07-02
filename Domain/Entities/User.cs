using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }


        //  User has one Role
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        //  User has many Projects
        public virtual ICollection<ProjectUser> UserProjects { get; set; } = new HashSet<ProjectUser>();

        //  User is leading many Projects 
        public virtual ICollection<Project> Projects { get; set; }

        //  User has many Tasks
        public virtual ICollection<Task> Tasks { get; set; }

        //  User has many UserUseCases
        public virtual ICollection<UserUseCase> UseCases { get; set; }

    }
}
