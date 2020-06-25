using Application.CommandHendler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class UnauthorizedCommandException : Exception
    {
        public UnauthorizedCommandException(IUseCase useCase, IApplicationActor actor)
            :base($"Actor: ({actor.Id}) {actor.Identity} \n Command: {useCase.Name}")
        {

        }
    }
}
