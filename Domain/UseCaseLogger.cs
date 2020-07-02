using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UseCaseLogger
    {
        public int Id { get; set; }
        public string Actor { get; set; }
        public string UseCase { get; set; }
        public string Data { get; set; }
        public DateTime DateTime { get; set; }

    }
}
