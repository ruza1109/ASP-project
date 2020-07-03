using Application.CommandHаndler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.TaskLog
{
    public interface IDeleteTaskLogCommand : ICommand<int>
    {
    }
}
