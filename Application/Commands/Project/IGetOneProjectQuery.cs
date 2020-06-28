using Application.CommandHаndler;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Project
{
    public interface IGetOneProjectQuery : IQuery<int,ProjectDTO>
    {
    }
}
