using Application.Commands.Task;
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

namespace Implementation.Queries.TaskQuery
{
    public class EFGetOneTaskQuery : BaseCommand, IGetOneTaskQuery
    {
        public EFGetOneTaskQuery(TeamworkContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 17;

        public string Name => "Get One Task";

        public TaskDTO Execute(int id)
        {
            var task = Context.Tasks.Where(t => t.Id == id)
                .Include(u => u.User)
                .Include(p => p.Project)
                .FirstOrDefault();

            if(task == null)
            {
                throw new EntityNotFoundException(id);
            }

            return Mapper.Map<TaskDTO>(task);
        }
    }
}
