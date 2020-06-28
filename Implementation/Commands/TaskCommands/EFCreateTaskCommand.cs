using Application.Commands.Task;
using Application.DTO;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.TaskCommands
{
    public class EFCreateTaskCommand : BaseCommand, ICreateTaskCommand
    {
        private readonly CreateTaskValidation _validation;

        public EFCreateTaskCommand(TeamworkContext context, IMapper mapper, CreateTaskValidation validation) : base(context, mapper)
        {
            _validation = validation;
        }

        public int Id => 18;

        public string Name => "Create Task";

        public void Execute(TaskDTO dto)
        {
            _validation.ValidateAndThrow(dto);

            var task = Mapper.Map<Task>(dto);

            //  Set Principal Entity to null in order to prevent creating it
            task.User = null;
            task.UserId = dto.User.Id;

            task.Project = null;
            task.ProjectId = dto.Project.Id;

            Context.Tasks.Add(task);
            Context.SaveChanges();
        }
    }
}
