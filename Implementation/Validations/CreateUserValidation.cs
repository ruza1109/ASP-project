using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;

namespace Implementation.Validations
{
    public class CreateUserValidation : AbstractValidator<UserDTO>
    {
        private readonly TeamworkContext _context;

        public CreateUserValidation(TeamworkContext context)
        {
            _context = context;

            RuleFor(u => u.FullName)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(50);

            RuleFor(u => u.Username)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(25)
                .Must(CheckUsernameUniqueness)
                .WithMessage(dto => $"'{dto.Username}' username already exists in database. Please, try another username.");

            RuleFor(u => u.Role)
                .Must((dto, role) => _context.Roles.Any(r => r.Id == dto.Role.Id))
                .WithMessage((dto) => $"Role with id: {dto.Role.Id} doesn't exist. Please, try with right role id.");
        }

        // Checking if username already exists in database
        private bool CheckUsernameUniqueness(string username)
        {
            return !_context.Users.Any(r => r.Username == username);
        }
       
    }
}
