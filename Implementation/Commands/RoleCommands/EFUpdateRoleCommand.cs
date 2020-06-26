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

        public int Id => 4;

        public string Name => "Update Role";

        public void Execute(RoleDTO request)
        {
            _validation.ValidateAndThrow(request);

            var role = _context.Roles.Find(request.Id);

            _mapper.Map(request, role);

            _context.SaveChanges();

        }
    }
}
