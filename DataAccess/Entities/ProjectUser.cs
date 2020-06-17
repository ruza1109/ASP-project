using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class ProjectUser
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }

        public virtual Project Project { get; set; }
        public virtual User User { get; set; }

    }
}
