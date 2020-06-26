using Application.CommandHаndler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Role
{
    public interface IDeleteRoleCommand : ICommand<int>
    {
    }
}
