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
        }
    }
}
