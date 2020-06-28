using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validations
{
    public class CreateTaskValidation : AbstractValidator<TaskDTO>
    {
        private readonly TeamworkContext _context;

        public CreateTaskValidation(TeamworkContext context)
        {
            _context = context;

            RuleFor(t => t.Name)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(80);

            RuleFor(t => t.Description)
                .MaximumLength(200);

            RuleFor(t => t.StoryPoints)
                .NotEmpty()
                .LessThan(20);

            RuleFor(t => t.Status)
                .IsInEnum();

            RuleFor(t => t.Priority)
                .IsInEnum();

            RuleFor(t => t.User)
                .NotEmpty()
                .WithMessage("Task has to be assigned on user.")
                .DependentRules(() =>
                {
                    RuleFor(t => t.User)
                    .Must(CheckUserExistence)
                    .WithMessage(dto => $"User with id:{dto.User.Id} doesn't exist. Please, try another user id.");
                });

            RuleFor(t => t.Project)
                .NotEmpty()
                .WithMessage("Task has to be assigned on project.")
                .DependentRules(() =>
                {
                    RuleFor(t => t.Project)
                    .Must(CheckProjectExistence)
                    .WithMessage(dto => $"Project with id:{dto.Project.Id} doesn't exist. Please, try another user id.");
                });

        }

        /**
         * Check if assigned User exists in database
         */
        private bool CheckUserExistence(TaskDTO dto, UserDTO user)
        {
            return _context.Users.Any(u => u.Id == dto.User.Id);
        }
        /**
         * Check if assigned Project exists in database
         */
        private bool CheckProjectExistence(TaskDTO dto, ProjectDTO user)
        {
            return _context.Projects.Any(u => u.Id == dto.Project.Id);
        }
    }
}
