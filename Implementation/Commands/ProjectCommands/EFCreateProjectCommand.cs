using Application.Commands.Project;
using Application.DTO;
using AutoMapper;
using DataAccess;
using FluentValidation;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.ProjectCommands
{
    public class EFCreateProjectCommand : ICreateProjectCommand
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;
        private readonly CreateProjectValidation _valdation;

        public EFCreateProjectCommand(TeamworkContext context, IMapper mapper, CreateProjectValidation valdation)
        {
            _context = context;
            _mapper = mapper;
            _valdation = valdation;
        }

        public int Id => 13;

        public string Name => "Create Project";

        public void Execute(ProjectDTO dto)
        {
            _valdation.ValidateAndThrow(dto);
        }
    }
}
