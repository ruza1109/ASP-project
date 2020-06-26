﻿using Application.Commands.User;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.UserCommands
{
    public class EFDeleteUserCommand : IDeleteUserCommand
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        public EFDeleteUserCommand(TeamworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 10;

        public string Name => "Delete User";

        public void Execute(int id)
        {
            var user = _context.Users.Find(id);

            if(user == null)
            {
                throw new EntityNotFoundException(id);
            }

            user.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
