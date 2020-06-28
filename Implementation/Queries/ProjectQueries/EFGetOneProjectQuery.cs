using Application.Commands.Project;
using Application.DTO;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.ProjectQueries
{
    public class EFGetOneProjectQuery : IGetOneProjectQuery
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        public EFGetOneProjectQuery(TeamworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 12;

        public string Name => "Get One Project";

        public ProjectDTO Execute(int id)
        {
            var project = _context.Projects
                .Where(p => p.Id == id)
                .Include(p => p.ProjectUsers)
                    .ThenInclude(pu => pu.User)
                        .ThenInclude(pu => pu.Role)
                .FirstOrDefault();

            if (project == null)
            {
                throw new EntityNotFoundException(id);
            }

            return _mapper.Map<ProjectDTO>(project);

        }
    }
}
