﻿using System;
using System.Collections.Generic;
using System.Text;
using Application.Commands;
using Application.Commands.Role;
using Application.DTO;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validations;

namespace Implementation.Commands.RoleCommands
{
    public class EFUpdateRoleCommand : IUpdateRoleCommand
    {

        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;
        private readonly UpdateRoleValidation _validation;

        public EFUpdateRoleCommand(TeamworkContext context, IMapper mapper, UpdateRoleValidation validation)
        {
            _context = context;
            _mapper = mapper;
            _validation = validation;
        }

        public int Id => 2;

        public string Name => "Update Role";

        public void Execute(RoleDTO request)
        {
            _validation.ValidateAndThrow(request);

            var role = _mapper.Map<Role>(request);


        }
    }
}