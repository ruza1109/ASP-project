using Application.Commands.Project;
using Application.DTO;
using Application.DTO.Pagination;
using Application.DTO.Search;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.ProjectQueries
{
    public class EFGetProjectQuery : IGetProjectQuery
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        public EFGetProjectQuery(TeamworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 11;

        public string Name => "Get Projects";

        public PagedResponse<ProjectDTO> Execute(SearchProjectDTO dto)
        {
            var projectQuery = _context.Projects.AsQueryable();

            if (!string.IsNullOrEmpty(dto.Name) || !string.IsNullOrWhiteSpace(dto.Name))
            {
                projectQuery = _context.Projects.Where(p => p.Name.ToLower().Contains(dto.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(dto.Deadline) || !string.IsNullOrWhiteSpace(dto.Deadline))
            {
                projectQuery = _context.Projects.Where(p => p.Deadline.ToString().Contains(dto.Deadline));
            }

            /**
             * Returns Projects that are assigned to searched User
             */

            if (!string.IsNullOrEmpty(dto.User) || !string.IsNullOrWhiteSpace(dto.User))
            {
                projectQuery = _context.Projects.
                    Where(p => p.ProjectUsers.
                    Select(pu => pu.User.FullName.ToLower())
                    .Contains(dto.User.ToLower()));
            }

            var proba = projectQuery.ToList();

            var skipCount = dto.PerPage * (dto.Page - 1);

            var projects = _mapper.Map<List<ProjectDTO>>(projectQuery.Skip(skipCount).Take(dto.PerPage).ToList());

            var response = new PagedResponse<ProjectDTO>
            {
                CurrentPage = dto.Page,
                ItemsPerPage = dto.PerPage,
                TotalCount = projectQuery.Count(),
                Items = projects
            };

            return response;
            
        }
    }
}
