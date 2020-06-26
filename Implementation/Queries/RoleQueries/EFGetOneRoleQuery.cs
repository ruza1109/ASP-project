using Application.Commands.Role;
using Application.DTO;
using Application.DTO.Pagination;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Queries.RoleQueries
{
    public class EFGetOneRoleQuery : IGetOneRoleQuery
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        public EFGetOneRoleQuery(TeamworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Get One Role";

        public RoleDTO Execute(int id)
        {
            var role = _context.Roles.Find(id);

            if (role == null)
            {
                throw new EntityNotFoundException(id);
            }

            return _mapper.Map<RoleDTO>(role);
        }
    }
}
