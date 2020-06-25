using Application;
using Application.CommandHendler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Loggers
{
    public class ConsoleLogger : IUseCaseLogger
    {
        public void Log(IUseCase useCase, IApplicationActor actor, object data)
        {
            Console.WriteLine($"\n Date and time: {DateTime.Now} \n " +
                $"Executor: {actor.Identity} \n " +
                $"Command: {useCase.Name} \n " +
                $"Data: {JsonConvert.SerializeObject(data)}");
        }
    }
}
