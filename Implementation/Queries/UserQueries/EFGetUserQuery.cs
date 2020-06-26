using Application.Commands.User;
using Application.DTO;
using Application.DTO.Pagination;
using Application.DTO.Search;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Implementation.Queries.UserQueries
{
    public class EFGetUserQuery : IGetUserQuery
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        public EFGetUserQuery(TeamworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 6;

        public string Name => "Get Users";

        public PagedResponse<UserDTO> Execute(SearchUserDTO dto)
        {
            var usersQuery = _context.Users.Include(u => u.Role).AsQueryable();

            if (!string.IsNullOrEmpty(dto.FullName) || !string.IsNullOrWhiteSpace(dto.FullName))
            {
                usersQuery = usersQuery.Where(u => u.FullName.ToLower().Contains(dto.FullName.ToLower()));
            }

            if (!string.IsNullOrEmpty(dto.Username) || !string.IsNullOrWhiteSpace(dto.Username))
            {
                usersQuery = usersQuery.Where(u => u.Username.ToLower().Contains(dto.Username.ToLower()));
            }

            if (dto.Role != null)
            {
                usersQuery = usersQuery.Where(u => u.Role.Name.ToLower().Contains(dto.Role.ToLower()));
            }

            var skipCount = dto.PerPage * (dto.Page - 1);

            var users = _mapper.Map<List<UserDTO>>(usersQuery.Skip(skipCount).Take(dto.PerPage).ToList());

            return new PagedResponse<UserDTO>
            {
                CurrentPage = dto.Page,
                ItemsPerPage = dto.PerPage,
                TotalCount = usersQuery.Count(),
                Items = users
            };
        }
    }
}
