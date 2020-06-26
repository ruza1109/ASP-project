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

        public PagedResponse<RoleDTO> Execute(SearchRoleDTO search)
        {
            var rolesQuery = _context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                rolesQuery = rolesQuery.Where(r => r.Name.ToLower().Contains(search.Name.ToLower()));
            }

            var skipCount = search.PerPage * (search.Page - 1);


            var reponse = new PagedResponse<RoleDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = rolesQuery.Count(),
                Items = rolesQuery.Skip(skipCount).Take(search.PerPage).Select(x => new RoleDTO
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return reponse;
        }
    }
}
