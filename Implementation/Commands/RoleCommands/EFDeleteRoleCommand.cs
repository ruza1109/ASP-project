using Application.Commands.Role;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.RoleCommands
{
    public class EFDeleteRoleCommand : IDeleteRoleCommand
    {
        private readonly TeamworkContext _context;

        public EFDeleteRoleCommand(TeamworkContext context)
        {
            _context = context;
        }

        public int Id => 5;

        public string Name => "Delete Role";

        public void Execute(int id)
        {
            var role = _context.Roles.Find(id);

            if (role == null)
            {
                throw new EntityNotFoundException(id);
            }

            role.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
