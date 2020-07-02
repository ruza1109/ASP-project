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
    public class EFCreateRoleCommand : BaseCommand, ICreateRoleCommand
    {
        private readonly CreateRoleValidation _validation;

        public EFCreateRoleCommand(TeamworkContext context, IMapper mapper, CreateRoleValidation validation) : base(context, mapper)
        {
            _validation = validation;
        }

        public int Id => 3;

        public string Name => "Create Role";

        public void Execute(RoleDTO dto)
        {
            _validation.ValidateAndThrow(dto);

            var role = Mapper.Map<Role>(dto);

            Context.Roles.Add(role);
            Context.SaveChanges();

        }
    }
}
