using Application.Commands.Task;
using Application.DTO;
using Application.Email;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands.TaskCommands
{
    public class EFUpdateTaskCommand : BaseCommand, IUpdateTaskCommand
    {
        private readonly UpdateTaskValidation _validation;
        private readonly IEmailSender _sender;
        public EFUpdateTaskCommand(TeamworkContext context, IMapper mapper, UpdateTaskValidation validation, IEmailSender sender) : base(context, mapper)
        {
            _validation = validation;
            _sender = sender;
        }

        public int Id => 19;

        public string Name => "Update Task";

        public void Execute(TaskDTO dto)
        {
            _validation.ValidateAndThrow(dto);

            var task = Context.Tasks.Find(dto.Id);

            if(task == null)
            {
                throw new EntityNotFoundException(dto.Id);
            }

            Mapper.Map(dto, task);

            //  Set Principal Entity to null in order to prevent creating it
            task.User = null;
            task.Project = null;

            Context.SaveChanges();

            var emailData = Context.Tasks
                .Include(u => u.User)
                .Include(p => p.Project)
                    .ThenInclude(u => u.Leader)
                .FirstOrDefault();

            if (dto.Status == Status.Done)
            {
                _sender.Send(new EmailDTO
                {
                    Content = $"Task with name: {emailData.Name}, on project:{emailData.Project.Name}, created by: {emailData.User.FullName} is completed.",
                    SendTo = emailData.Project.Leader.Email,
                    Subject = "Task Completed!"
                });
            }

        }
    }
}
