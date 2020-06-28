using Application.Commands.User;
using Application.DTO;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using FluentValidation;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.UserCommands
{
    public class EFUpdateUserCommand : IUpdateUserCommand
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;
        private readonly UpdateUserValidation _validation;

        public EFUpdateUserCommand(TeamworkContext context, IMapper mapper, UpdateUserValidation validation)
        {
            _context = context;
            _mapper = mapper;
            _validation = validation;
        }

        public int Id => 9;

        public string Name => "Update User";

        public void Execute(UserDTO dto)
        {
            _validation.ValidateAndThrow(dto);

            var user = _context.Users.Find(dto.Id);

            if (user == null)
            {
                throw new EntityNotFoundException(dto.Id);
            }

            _mapper.Map(dto, user);

            //  Set Principal Entity to null in order to prevent creating it
            user.Role = null;

            _context.SaveChanges();
        }
    }
}
