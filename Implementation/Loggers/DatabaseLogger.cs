using Application;
using Application.CommandHаndler;
using DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Implementation.Loggers
{
    public class DatabaseLogger : IUseCaseLogger
    {
        private readonly TeamworkContext _context;

        public DatabaseLogger(TeamworkContext context)
        {
            _context = context;
        }

        public void Log(IUseCase useCase, IApplicationActor actor, object data)
        {
            _context.UseCaseLoggers.Add(new Domain.UseCaseLogger
            {
                Actor = actor.Identity,
                UseCase = useCase.Name,
                Data = JsonConvert.SerializeObject(data),
                DateTime = DateTime.Now
            });

            _context.SaveChanges();
        }
    }
}
