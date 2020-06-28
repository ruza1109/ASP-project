using Application.CommandHаndler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Project
{
    public interface IDeleteProjectCommand : ICommand<int>
    {
    }
}
