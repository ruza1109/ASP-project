using Application.Commands.User;
using Application.DTO;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.UserCommands
{
    public class EFCreateUserCommand : ICreateUserCommand
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;
        private readonly CreateUserValidation _validation;

        public EFCreateUserCommand(TeamworkContext context, IMapper mapper, CreateUserValidation validation)
        {
            _context = context;
            _mapper = mapper;
            _validation = validation;
        }

        public int Id => 8;

        public string Name => "Create User";

        public void Execute(UserDTO request)
        {
            _validation.ValidateAndThrow(request);

            var user = _mapper.Map<User>(request);

            //  Set Principal Entity to null in order to prevent creating it
            user.Role = null;
            user.RoleId = request.Role.Id;

            _context.Users.Add(user);

            _context.SaveChanges();
        }
    }
}
