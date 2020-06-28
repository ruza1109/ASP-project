using Application.DTO;
using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validations
{
    public class UpdateProjectValidation : AbstractValidator<ProjectDTO>
    {
        private readonly TeamworkContext _context;
        public UpdateProjectValidation(TeamworkContext context)
        {
            _context = context;

            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(40)
                .Must(CheckProjectNameUniqueness)
                .WithMessage(dto => $"'{dto.Name}' project name already exists in database. Please, try another project name.");

            RuleFor(p => p.Description)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(p => p.Deadline)
                .NotEmpty()
                .DependentRules(() =>
                {
                    RuleFor(p => p.Deadline)
                        .GreaterThan(DateTime.Today)
                        .WithMessage("Deadline date must be in future.")
                        .LessThan(DateTime.Now.AddYears(5))
                        .WithMessage("Deadline date can't pass 5 years from today.");
                });

            RuleFor(p => p.Users)
                .NotEmpty()
                .WithMessage("At least one user must be on project.")
                .DependentRules(() =>
                    RuleForEach(u => u.Users)
                        .Must(CheckAssignedUserExistance)
                        .WithMessage("User you are trying to assign on project doesn't exist.")
                    )
                .Must(CheckAssignedUsersDuplicates)
                .WithMessage("Duplicated users are not allowed on project.");
        }

        /**
         * Check if Project name already exists in database
         */
        private bool CheckProjectNameUniqueness(ProjectDTO dto, string name)
        {
            return !_context.Projects.Any(u => u.Name == name && u.Id != dto.Id);
        }

        /**
         * Check if assigned User exists in database 
         */
        private bool CheckAssignedUserExistance(UserDTO dto)
        {
            return _context.Users.Any(p => p.Id == dto.Id);
        }

        /**
         * Check if duplicated Users are assigned on Project 
         */
        private bool CheckAssignedUsersDuplicates(IEnumerable<UserDTO> dto)
        {
            return dto.Select(x => x.Id).Distinct().Count() == dto.Count();
        }
    }
}
