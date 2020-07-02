using DataAccess;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;

namespace Implementation.Validations
{
    public class UpdateUserValidation : AbstractValidator<UserDTO>
    {
        private readonly TeamworkContext _context;

        public UpdateUserValidation(TeamworkContext context)
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

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20);

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Role)
                .Must(CheckRoleExistance)
                .WithMessage((dto) => $"Role with id:{dto.Role.Id} doesn't exist. Please, try with an existing role id.");
        }

        /**
         * Check if username already exists in database
         */
        private bool CheckUsernameUniqueness(UserDTO dto, string username)
        {
            return !_context.Users.Any(u => u.Username == username && u.Id != dto.Id);
        }

        /**
         * Check if assigned Role exists in database
         */
        private bool CheckRoleExistance(UserDTO dto, RoleDTO role)
        {
            return _context.Roles.Any(r => r.Id == dto.Role.Id);
        }
    }
}
