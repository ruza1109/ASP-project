using Application.CommandHаndler;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.User
{
    public interface IGetOneUserQuery : IQuery<int,UserDTO>
    {
    }
}
