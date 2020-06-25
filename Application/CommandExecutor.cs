using Application.CommandHendler;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Application
{
    public class CommandExecutor
    {
        private readonly IApplicationActor _actor;

        public CommandExecutor(IApplicationActor actor)
        {
            _actor = actor;
        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            Console.WriteLine($"Date and time: {DateTime.Now} \n Executor: {_actor.Identity} \n Command: {command.Name}");

            if(!_actor.AllowedCommands.Contains(command.Id))
            {
                throw new UnauthorizedCommandException(command, _actor);
            }

            command.Execute(request);
        }
    }
}
