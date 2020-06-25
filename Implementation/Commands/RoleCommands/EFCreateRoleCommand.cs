using Application;
using Application.Commands.Role;
using Application.DTO;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.RoleCommands
{
    public class EFCreateRoleCommand : ICreateRoleCommand
    {
        private readonly TeamworkContext _context;
        private readonly CreateRoleValidation _validation;
        private readonly IMapper _mapper;

        public EFCreateRoleCommand(TeamworkContext context, CreateRoleValidation validation, IMapper mapper)
        {
            _context = context;
            _validation = validation;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Create Role";

        public void Execute(RoleDTO dto)
        {
            _validation.ValidateAndThrow(dto);

            //var role = _mapper.Map<Role>(dto);

            var role = new Role
            {
                Name = dto.Name
            };

            _context.Roles.Add(role);
            _context.SaveChanges();

        }
    }
}
