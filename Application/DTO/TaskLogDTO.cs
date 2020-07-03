using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class TaskLogDTO
    {
        public int Id { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Task Task { get; set; }
    }
}
