using Application.Commands.Task;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.TaskCommands
{
    public class EFDeleteTaskCommand : IDeleteTaskCommand
    {
        private readonly TeamworkContext _context;

        public EFDeleteTaskCommand(TeamworkContext context)
        {
            _context = context;
        }

        public int Id => 20;

        public string Name => "Delete Task";

        public void Execute(int id)
        {
            var task = _context.Tasks.Find(id);

            if(task == null)
            {
                throw new EntityNotFoundException(id);
            }

            task.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
