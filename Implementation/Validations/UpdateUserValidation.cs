﻿using DataAccess;
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
                .MinimumLength(4)
                .MaximumLength(50);

            RuleFor(u => u.Username)
                .MinimumLength(4)
                .MaximumLength(25)
                .Must((dto, username) => !_context.Users.Any(u => u.Username == username && u.Id != dto.Id))
                .WithMessage(dto => $"'{dto.Username}' username already exists in database. Please, try another username.");

        }
    }
}