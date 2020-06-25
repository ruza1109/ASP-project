using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IApplicationActor
    {
        public int Id { get; }
        public string Identity { get; }
        public IEnumerable<int> AllowedCommands { get; }
    }
}
