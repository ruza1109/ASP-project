using Application.Commands.TaskLog;
using Application.DTO;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using FluentValidation;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.TaskLogCommands
{
    public class EFUpdateTaskLogCommand : BaseCommand, IUpdateTaskLogCommand
    {
        private readonly UpdateTaskLogValidation _validation;
        public EFUpdateTaskLogCommand(TeamworkContext context, IMapper mapper, UpdateTaskLogValidation validation) : base(context, mapper)
        {
            _validation = validation;
        }

        public int Id => 24;

        public string Name => "Update Task Log";

        public void Execute(TaskLogDTO dto)
        {
            _validation.ValidateAndThrow(dto);

            var taskLog = Context.TaskLogs.Find(dto.Id);

            if (taskLog == null)
            {
                throw new EntityNotFoundException(dto.Id);
            }

            Mapper.Map(dto, taskLog);

            taskLog.Task = null;

            Context.SaveChanges();
        }
    }
}
