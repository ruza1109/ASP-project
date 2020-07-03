using Application.Commands.TaskLog;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.TaskLogCommands
{
    public class EFDeleteTaskLogCommand : BaseCommand, IDeleteTaskLogCommand
    {
        public EFDeleteTaskLogCommand(TeamworkContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 25;

        public string Name => "Delete Task Log";

        public void Execute(int id)
        {
            var taskLog = Context.TaskLogs.Find(id);

            if (taskLog == null)
            {
                throw new EntityNotFoundException(id);
            }

            taskLog.DeletedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
