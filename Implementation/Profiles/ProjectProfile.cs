using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDTO, Project>();

            CreateMap<Project, ProjectDTO>()
                .ForMember(dto => dto.Users,
                    opt => opt.MapFrom(project => project.ProjectUsers.Select(pu => new UserDTO
                    {
                        Id = pu.User.Id,
                        FullName = pu.User.FullName,
                        Username = pu.User.Username,
                        Role = new RoleDTO
                        {
                            Id = pu.User.Role.Id,
                            Name =  pu.User.Role.Name
                        }
                    })));
        }
    }
}
