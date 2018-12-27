using System;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Executor.Steps
{
    public abstract class ExecutorStep
    {
        protected ILogger Logger { get; }

        public ExecutorStep(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(GetType());
        }

        public abstract void Execute();
    }
}
