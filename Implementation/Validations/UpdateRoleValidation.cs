using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;

namespace Implementation.Validations
{
    public class UpdateRoleValidation : AbstractValidator<RoleDTO>
    {
        private readonly TeamworkContext _context;

        public UpdateRoleValidation(TeamworkContext context)
        {
            _context = context;

            RuleFor(dto => dto.Name)
               .NotEmpty()
               .Must((dto, name) => !_context.Roles.Any(r => r.Name == name && r.Id != dto.Id))
               .WithMessage(dto => $"'{dto.Name}' role name already exists in database. Please, try another role name.");
        }
    }
}
