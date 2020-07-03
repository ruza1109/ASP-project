using Application.Commands.TaskLog;
using Application.DTO;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.TaskLogCommands
{
    public class EFCreateTaskLogCommand : BaseCommand, ICreateTaskLogCommand
    {
        private readonly CreateTaskLogValidation _validation;
        public EFCreateTaskLogCommand(TeamworkContext context, IMapper mapper, CreateTaskLogValidation validation) : base(context, mapper)
        {
            _validation = validation;
        }

        public int Id => 23;

        public string Name => "Create Task Log";

        public void Execute(TaskLogDTO dto)
        {
            _validation.ValidateAndThrow(dto);

            var taskLog = Mapper.Map<TaskLog>(dto);

            //  Set Principal Entity to null in order to prevent creating it
            taskLog.Task = null;
            taskLog.TaskId = dto.Task.Id;

            Context.TaskLogs.Add(taskLog);
            Context.SaveChanges();
        }
    }
}
