using Application.CommandHаndler;
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
        private readonly IUseCaseLogger _logger;

        public CommandExecutor(IApplicationActor actor, IUseCaseLogger logger)
        {
            _actor = actor;
            _logger = logger;
        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            _logger.Log(command, _actor, request);

            if(!_actor.AllowedCommands.Contains(command.Id))
            {
                throw new UnauthorizedCommandException(command, _actor);
            }

            command.Execute(request);
        }


        public TResult ExecuteQuery<TSearch, TResult>
            (IQuery<TSearch, TResult> query, TSearch search)
        {
            _logger.Log(query, _actor, search);

            if (!_actor.AllowedCommands.Contains(query.Id))
            {
                throw new UnauthorizedCommandException(query, _actor);
            }

            return query.Execute(search);
        }
    }
}
