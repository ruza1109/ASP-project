using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Profiles
{
    public class TaskLogProfile : Profile
    {
        public TaskLogProfile()
        {
            CreateMap<TaskLogDTO, TaskLog>();

            CreateMap<TaskLog, TaskLogDTO>()
                .ForMember(dto => dto.Task,
                    opt => opt.MapFrom(log => new TaskDTO
                    {
                        Id = log.Task.Id,
                        Name = log.Task.Name,
                        Description = log.Task.Description,
                        StoryPoints = log.Task.StoryPoints,
                        Status = log.Task.Status,
                        Priority = log.Task.Priority
                    }));

        }
    }
}
