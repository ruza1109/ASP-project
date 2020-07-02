using Application.CommandHаndler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Application.Commands.Task
{
    public interface IDeleteTaskCommand : ICommand<int>
    {
    }
}
