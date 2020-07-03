using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validations
{
    public class UpdateTaskLogValidation : AbstractValidator<TaskLogDTO>
    {
        private readonly TeamworkContext _context;

        public UpdateTaskLogValidation(TeamworkContext context)
        {
            _context = context;

            RuleFor(t => t.Time)
               .NotEmpty()
               .LessThanOrEqualTo(8);

            RuleFor(t => t.Description)
                .MaximumLength(200);

            RuleFor(t => t.Date)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Today);

            RuleFor(t => t.Task)
                .NotEmpty()
                .WithMessage("Log has to be assigned on task.")
                .DependentRules(() =>
                {
                    RuleFor(t => t.Task)
                    .Must((dto, task) => _context.Tasks.Any(u => u.Id == dto.Task.Id))
                    .WithMessage(dto => $"Task with id:{dto.Task.Id} doesn't exist. Please, try another task id.")
                    .Must((dto, task) => !_context.TaskLogs.Any(u => u.TaskId != dto.Task.Id))
                    .WithMessage(dto => $"Task with an id:{dto.Task.Id} is already logged.");
                });

        }
    }
}
