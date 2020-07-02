using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskDTO>()
                .ForMember(dto => dto.Project,
                    opt => opt.MapFrom(task => new ProjectDTO
                    {
                        Name = task.Project.Name,
                        Description = task.Project.Description,
                        Deadline = task.Project.Deadline,
                        Leader = new UserDTO
                        {
                            FullName = task.Project.Leader.FullName,
                            Username = task.Project.Leader.Username,
                            Email = task.Project.Leader.Email
                        }
                    }));

            CreateMap<TaskDTO, Task>();
        }
    }
}
