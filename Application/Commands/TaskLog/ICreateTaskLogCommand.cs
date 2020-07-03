using Application.CommandHаndler;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.TaskLog
{
    public interface ICreateTaskLogCommand : ICommand<TaskLogDTO>
    {
    }
}
