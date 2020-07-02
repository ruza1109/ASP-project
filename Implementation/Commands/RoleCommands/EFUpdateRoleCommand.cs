using System;
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
    public class EFUpdateRoleCommand : BaseCommand, IUpdateRoleCommand
    {
        private readonly UpdateRoleValidation _validation;

        public EFUpdateRoleCommand(TeamworkContext context, IMapper mapper, UpdateRoleValidation validation) : base(context, mapper)
        {
            _validation = validation;
        }

        public int Id => 4;

        public string Name => "Update Role";

        public void Execute(RoleDTO request)
        {
            _validation.ValidateAndThrow(request);

            var role = Context.Roles.Find(request.Id);

            Mapper.Map(request, role);

            Context.SaveChanges();

        }
    }
}
