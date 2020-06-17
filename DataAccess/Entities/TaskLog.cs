using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class TaskLog : BaseEntity
    {
        public int Time { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        //  TaskLog has one Task
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
