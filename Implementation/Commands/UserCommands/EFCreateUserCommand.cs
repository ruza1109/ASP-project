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
    public class EFCreateUserCommand : BaseCommand, ICreateUserCommand
    {
        private readonly CreateUserValidation _validation;

        public EFCreateUserCommand(TeamworkContext context, IMapper mapper, CreateUserValidation validation) : base(context, mapper)
        {
            _validation = validation;
        }

        public int Id => 8;

        public string Name => "Create User";

        public void Execute(UserDTO request)
        {
            _validation.ValidateAndThrow(request);

            var user = Mapper.Map<User>(request);

            //  Set Principal Entity to null in order to prevent creating it
            user.Role = null;
            user.RoleId = request.Role.Id;

            Context.Users.Add(user);

            Context.SaveChanges();
        }
    }
}
