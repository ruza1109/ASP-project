using Application.CommandHаndler;
using Application.DTO;
using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Role
{
    public interface IGetOneRoleQuery : IQuery<int, RoleDTO>
    {
    }
}
