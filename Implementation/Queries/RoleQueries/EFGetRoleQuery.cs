using Application.Commands.Role;
using Application.DTO;
using Application.DTO.Pagination;
using Application.DTO.Search;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.RoleQueries
{
    public class EFGetRoleQuery : IGetRoleQuery
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        public EFGetRoleQuery(TeamworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Get Roles";

        public PagedResponse<RoleDTO> Execute(SearchRoleDTO dto)
        {
            var rolesQuery = _context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(dto.Name) || !string.IsNullOrWhiteSpace(dto.Name))
            {
                rolesQuery = rolesQuery.Where(r => r.Name.ToLower().Contains(dto.Name.ToLower()));
            }

            var skipCount = dto.PerPage * (dto.Page - 1);

            var roles = _mapper.Map<List<RoleDTO>>(rolesQuery.Skip(skipCount).Take(dto.PerPage).ToList());

            var reponse = new PagedResponse<RoleDTO>
            {
                CurrentPage = dto.Page,
                ItemsPerPage = dto.PerPage,
                TotalCount = rolesQuery.Count(),
                Items = roles
            };

            return reponse;
        }
    }
}
