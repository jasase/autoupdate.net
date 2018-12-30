using System;
using AutoUpdate.Abstraction.Configurations;
using AutoUpdate.Abstraction.Configurations.Steps;
using AutoUpdate.Executor.Steps;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Executor
{
    public class ExecutorStepFactory : IExecutorStepConfigurationVisitor<ExecutorStep>
    {
        private readonly ILoggerFactory _loggerFactory;

        public ExecutorStepFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public ExecutorStep Handle(ExchangeFilesStepConfiguration configuration)
            => new ExchangeFilesStep(_loggerFactory, configuration);
    }
}
