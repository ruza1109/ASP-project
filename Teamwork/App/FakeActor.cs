using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamwork.App
{
    public class FakeActor : IApplicationActor
    {
        public int Id => 1;

        public string Identity => "Fake User";

        public IEnumerable<int> AllowedCommands => new List<int> { 1 };
    } 
    
    public class FakeAdminActor : IApplicationActor
    {
        public int Id => 2;

        public string Identity => "Fake Admin";

        public IEnumerable<int> AllowedCommands => Enumerable.Range(1,30);
    }
}
