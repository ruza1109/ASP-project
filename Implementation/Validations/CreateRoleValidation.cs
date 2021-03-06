﻿using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;

namespace Implementation.Validations
{
    public class CreateRoleValidation : AbstractValidator<RoleDTO>
    {
        private readonly TeamworkContext _context;

        public CreateRoleValidation(TeamworkContext context)
        {
            _context = context;

            RuleFor(dto => dto.Name)
                .NotEmpty()
                .Must(CheckNameUniqueness)
                .WithMessage(dto => $"{dto.Name} already exists in in database. Please, try another role name.");
        }

        // Checking if role name already exists in database
        private bool CheckNameUniqueness(string name)
        {
            return !_context.Roles.Any(r => r.Name == name);
        }
    }
}
