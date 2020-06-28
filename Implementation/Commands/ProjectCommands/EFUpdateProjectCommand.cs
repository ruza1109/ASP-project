using Application.Commands.Project;
using Application.DTO;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands.ProjectCommands
{
    public class EFUpdateProjectCommand : IUpdateProjectCommand
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;
        private readonly UpdateProjectValidation _validation;

        public EFUpdateProjectCommand(TeamworkContext context, IMapper mapper, UpdateProjectValidation validation)
        {
            _context = context;
            _mapper = mapper;
            _validation = validation;
        }

        public int Id => 14;

        public string Name => "Update Project";

        public void Execute(ProjectDTO dto)
        {
            _validation.ValidateAndThrow(dto);

            var project = _context.Projects
                .Include(p => p.ProjectUsers)
                .FirstOrDefault(p => p.Id == dto.Id);

            if(project == null)
            {
                throw new EntityNotFoundException(dto.Id);
            }

            _mapper.Map(dto,project);

            project.ProjectUsers.Where(up => up.ProjectId == project.Id)
                .ToList()
                .ForEach(x => project.ProjectUsers.Remove(x));
            
            foreach (var item in dto.Users)
            {
                project.ProjectUsers.Add(new ProjectUser
                {
                    ProjectId = project.Id,
                    UserId = item.Id
                });
            }

            _context.SaveChanges();
        }
    }
}
