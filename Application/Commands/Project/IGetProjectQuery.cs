using Application.CommandHаndler;
using Application.DTO;
using Application.DTO.Pagination;
using Application.DTO.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Project
{
    public interface IGetProjectQuery : IQuery<SearchProjectDTO,PagedResponse<ProjectDTO>>
    {
    }
}
