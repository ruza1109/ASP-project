using Application.Commands.User;
using Application.DTO;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.UserQueries
{
    public class EFGetOneUserQuery : IGetOneUserQuery
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        public EFGetOneUserQuery(TeamworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 7;

        public string Name => "Get One User";

        public UserDTO Execute(int id)
        {
            var user = _context.Users.Where(u => u.Id == id).Include(u => u.Role).FirstOrDefault();

            if (user == null)
            {
                throw new EntityNotFoundException(id);
            }

            return _mapper.Map<UserDTO>(user);
        }
    }
}
