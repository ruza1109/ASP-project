using Application.Commands.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.RoleCommands
{
    public class EFDeleteRoleCommand : IDeleteRoleCommand
    {
        public int Id => 5;

        public string Name => "Delete Role";

        public void Execute(int request)
        {
            throw new NotImplementedException();
        }
    }
}
