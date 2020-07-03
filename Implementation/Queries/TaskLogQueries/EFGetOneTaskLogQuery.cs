using Application.Commands.TaskLog;
using Application.DTO;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Implementation.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.TaskLogQueries
{
    public class EFGetOneTaskLogQuery : BaseCommand, IGetOneTaskLogQuery
    {
        public EFGetOneTaskLogQuery(TeamworkContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 22;

        public string Name => "Get One Task Log";

        public TaskLogDTO Execute(int id)
        {
            var taskLog = Context.TaskLogs
                .Where(t => t.Id == id)
                .Include(t => t.Task)
                .FirstOrDefault();

            if(taskLog == null)
            {
                throw new EntityNotFoundException(id);
            }

            return Mapper.Map<TaskLogDTO>(taskLog);
        }
    }
}
