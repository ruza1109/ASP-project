using Application;
using Application.Commands.Task;
using Application.DTO;
using Application.DTO.Pagination;
using Application.DTO.Search;
using AutoMapper;
using DataAccess;
using Implementation.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.TaskQuery
{
    public class EFGetTaskQuery : BaseCommand, IGetTaskQuery
    {
        public EFGetTaskQuery(TeamworkContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 16;

        public string Name => "Get Tasks";

        public PagedResponse<TaskDTO> Execute(SearchTaskDTO dto)
        {
            var taskQuery = Context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(dto.Name) && !string.IsNullOrWhiteSpace(dto.Name))
            {
                taskQuery = taskQuery.Where(t => t.Name.ToLower().Contains(dto.Name.ToLower()));
            }

            if (dto.StoryPoints > 0)
            {
                taskQuery = taskQuery.Where(t => t.StoryPoints == dto.StoryPoints);
            }

            if (!string.IsNullOrEmpty(dto.Project) && !string.IsNullOrWhiteSpace(dto.Project))
            {
                taskQuery = taskQuery.Where(t => t.Project.Name.ToLower().Contains(dto.Project.ToLower()));
            }

            var skipCount = dto.PerPage * (dto.Page - 1);

            var tasks = Mapper.Map<List<TaskDTO>>(taskQuery.Skip(skipCount).Take(dto.PerPage).ToList());

            var response = new PagedResponse<TaskDTO>
            {
                CurrentPage = dto.Page,
                ItemsPerPage = dto.PerPage,
                TotalCount = taskQuery.Count(),
                Items = tasks
            };

            return response;
        }
    }
}
