using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum Status
    {
        ToDo,
        InProgress,
        Done
    }
    public class Task : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StoryPoints { get; set; }
        public Status Status { get; set; }
        public Priority Priority{ get; set; }

        //  Task has one User
        public int UserId { get; set; }
        public virtual User User { get; set; }

        //  Task has one Project
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        //  Task has many TaskLog
        public virtual TaskLog Log { get; set; }
    }
}
