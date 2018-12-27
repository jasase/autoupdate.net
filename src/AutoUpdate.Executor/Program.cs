using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using AutoUpdate.Shared;
using AutoUpdate.Shared.Configurations;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Executor
{
    class Program
    {
        private static ILogger<Program> _logger;

        public static ILoggerFactory LoggerFactory { get; private set; }

        static int Main(string[] args)
        {
#if DEBUG
            while (!Debugger.IsAttached)
            {
                Thread.Sleep(250);
            }
#endif

            LoggerFactory = new LoggerFactory();
            LoggerFactory.AddConsole(LogLevel.Trace);
            LoggerFactory.AddFile("Logs/executor-{Date}.txt",
                                  minimumLevel: LogLevel.Trace,
                                  isJson: true);
            _logger = LoggerFactory.CreateLogger<Program>();

            _logger.LogInformation("Reading config");
            var config = ReadConfig(args);
            if (config == null)
            {
                _logger.LogError("Reading of config not successfull");
                return -1;
            }

            _logger.LogInformation("Waiting on parent process closed");
            if (!WaitClosingOfParentProcess(config))
            {
                _logger.LogError("Waiting on closing of parent process reached timeout");
                return -2;
            }

            _logger.LogInformation("Executing update steps");
            var factory = new ExecutorStepFactory(LoggerFactory);
            foreach (var stepConfiguration in config.Steps)
            {
                _logger.LogDebug("Executing step '{0}'", stepConfiguration.GetType().FullName);
                var step = stepConfiguration.Accept(factory);
                step.Execute();
            }

            //TODO Restart old application

            return 0;
        }

        static ExecutorConfiguration ReadConfig(string[] args)
        {
            var configFilePath = ExecutorConfiguration.DEFAULT_FILENAME;
            if (args.Any())
            {
                configFilePath = args[0];
            }

            _logger.LogDebug("Checking config file at '{0}'", configFilePath);
            if (!File.Exists(configFilePath))
            {
                _logger.LogError("Could not find any config file at '{0}'", configFilePath);
                return null;
            }

            var serializer = new ConfigurationSerializer();
            try
            {
                var configContent = File.ReadAllText(configFilePath);
                var config = serializer.Deserialize(configContent);

                return config;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deserializing of config not possible");
                return null;
            }
        }

        static bool WaitClosingOfParentProcess(ExecutorConfiguration configuration)
        {
            var parentProcess = Process.GetProcessById(configuration.Application.CallingProcessId);
            if (parentProcess == null)
            {
                _logger.LogDebug("Parent process with id '{0}' not found. Continue", configuration.Application.CallingProcessId);
                return true;
            }

            _logger.LogDebug("Parent process found.");
            var waitCounter = 0;
            while (!parentProcess.HasExited)
            {
                if (waitCounter > 30)
                {
                    _logger.LogError("Waiting on closing of parent process reached timeout");
                    return false;
                }

                _logger.LogTrace("Parent process with id '{1}' still running. Waiting. Count {0}", waitCounter, parentProcess.Id);
                Thread.Sleep(1000);
                waitCounter++;
            }

            _logger.LogDebug("Parent process closed");
            return true;
        }
    }
}

