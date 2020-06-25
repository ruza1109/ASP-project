using Application;
using Application.CommandHendler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Loggers
{
    public class FileLogger : IUseCaseLogger
    {
        private readonly string _pathToFileLogger = @"C:\Users\Milos\source\repos\Teamwork\FileLogger.txt";

        public void Log(IUseCase useCase, IApplicationActor actor, object data)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(_pathToFileLogger, true))
            {
                file.WriteLine($"\n Date and time: {DateTime.Now} \n " +
                $"Executor: {actor.Identity} \n " +
                $"Command: {useCase.Name} \n " +
                $"Data: {JsonConvert.SerializeObject(data)} \n");
            }
        }
    }
}
