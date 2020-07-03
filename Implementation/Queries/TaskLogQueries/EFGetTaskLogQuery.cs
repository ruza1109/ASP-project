using Application.Commands.TaskLog;
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

namespace Implementation.Queries.TaskLogQueries
{
    public class EFGetTaskLogQuery : BaseCommand, IGetTaskLogQuery
    {
        public EFGetTaskLogQuery(TeamworkContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 21;

        public string Name => "Get Task Logs";

        public PagedResponse<TaskLogDTO> Execute(SearchTaskLogDTO dto)
        {
            var logsQuery = Context.TaskLogs.AsQueryable();

            if (dto.DateFrom != null)
            {
                logsQuery = logsQuery.Where(l => l.Date >= dto.DateFrom);
            }

            if (dto.DateTo != null)
            {
                logsQuery = logsQuery.Where(l => l.Date <= dto.DateTo);
            }

            if (dto.DateFrom != null && dto.DateTo != null)
            {
                logsQuery = logsQuery.Where(l => l.Date >= dto.DateFrom)
                    .Where(l => l.Date <= dto.DateTo);
            }

            var skipCount = dto.PerPage * (dto.Page - 1);

            var logs = Mapper.Map<List<TaskLogDTO>>(logsQuery.Skip(skipCount).Take(dto.PerPage).ToList());

            var response = new PagedResponse<TaskLogDTO>
            {
                CurrentPage = dto.Page,
                ItemsPerPage = dto.PerPage,
                TotalCount = logsQuery.Count(),
                Items = logs
            };

            return response;
        }
    }
}
