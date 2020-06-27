using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Implementation.Validations
{
    public class CreateProjectValidation : AbstractValidator<ProjectDTO>
    {
        private readonly TeamworkContext _context;

        public CreateProjectValidation(TeamworkContext context)
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
                        .Must(user => _context.Users.Any(p => p.Id == user.Id))
                        .WithMessage("User you are trying to assign on project doesn't exist.")
                    )
                .Must(u => u.Select(x => x.Id).Distinct().Count() == u.Count())
                .WithMessage("Duplicated users are not allowed on project.");
        }

        // Checking if project name already exists in database
        private bool CheckProjectNameUniqueness(string name)
        {
            return !_context.Projects.Any(p => p.Name == name);
        }

    }
}
