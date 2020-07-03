using Application.CommandHаndler;
using Application.Commands;
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

namespace Implementation.Queries
{
    public class EFGetLogs : BaseCommand, IGetLogsQuery
    {
        public EFGetLogs(TeamworkContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 26;

        public string Name => "Get Logs";

        public PagedResponse<LogDTO> Execute(SearchLogDTO dto)
        {
            var logsQuery = Context.UseCaseLoggers.AsQueryable();

            if (!string.IsNullOrEmpty(dto.Actor) && !string.IsNullOrWhiteSpace(dto.Actor))
            {
                logsQuery = logsQuery.Where(l => l.Actor.ToLower().Contains(dto.Actor.ToLower()));
            }

            if (!string.IsNullOrEmpty(dto.UseCase) && !string.IsNullOrWhiteSpace(dto.UseCase))
            {
                logsQuery = logsQuery.Where(l => l.UseCase.ToLower().Contains(dto.UseCase.ToLower()));
            }

            if (dto.DateFrom != null)
            {
                logsQuery = logsQuery.Where(l => l.DateTime >= dto.DateFrom);
            }

            if (dto.DateTo != null)
            {
                logsQuery = logsQuery.Where(l => l.DateTime <= dto.DateTo);
            }

            if (dto.DateFrom != null && dto.DateTo != null)
            {
                logsQuery = logsQuery.Where(l => l.DateTime >= dto.DateFrom)
                    .Where(l => l.DateTime <= dto.DateTo);
            }

            var skipCount = dto.PerPage * (dto.Page - 1);

            var logs = Mapper.Map<List<LogDTO>>(logsQuery.Skip(skipCount).Take(dto.PerPage).ToList());

            var response = new PagedResponse<LogDTO>
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
