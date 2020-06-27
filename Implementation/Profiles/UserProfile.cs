using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;

namespace Implementation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>();

            CreateMap<User, UserDTO>()
                .ForMember(dto => dto.Projects,
                    opt => opt.MapFrom(user => user.UserProjects.Select(up => new ProjectDTO
                    {
                        Id = up.Project.Id,
                        Name = up.Project.Name,
                        Description = up.Project.Description,
                        Deadline = up.Project.Deadline
                    })));
        }
    }
}
