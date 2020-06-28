using Application.Commands.Project;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.ProjectCommands
{
    public class EFDeleteProjectCommand : IDeleteProjectCommand
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        public EFDeleteProjectCommand(TeamworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 15;

        public string Name => "Delete Project";

        public void Execute(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                throw new EntityNotFoundException(id);
            }

            project.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
