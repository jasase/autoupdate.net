using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.TestCore
{
    [TestClass]
    public abstract class Specification
    {
        private readonly Dictionary<Type, ITestModule> _modules;
        private readonly ILoggerFactory _loggerFactory;

        public ILoggerFactory LoggerFactory => _loggerFactory;
        public ILogger Logger { get; private set; }
        public Exception ThrownException { get; private set; }

        public virtual bool ExpectException => false;

        public Specification()
        {
            _modules = new Dictionary<Type, ITestModule>();
            _loggerFactory = new LoggerFactory();
        }

        [TestInitialize]
        public void Setup()
        {
            _loggerFactory.AddConsole(LogLevel.Trace, true);
            Logger = _loggerFactory.CreateLogger<Specification>();

            Logger.LogDebug("Starting [Establishing context]");
            EstablishContext();
            Logger.LogDebug("Finished [Establishing context]");

            Logger.LogDebug("Starting [Because]");
            try
            {
                Because();
            }
            catch (Exception ex)
            {
                ThrownException = ex;
                if (!ExpectException) throw ex;
            }
            Logger.LogDebug("Finished [Because]");
        }

        public virtual void EstablishContext()
        { }

        public virtual void Because()
        { }

        public TModule Module<TModule>()
            where TModule : ITestModule, new()
        {
            var moduleType = typeof(TModule);
            lock (_modules)
            {
                if (!_modules.ContainsKey(moduleType))
                {
                    Logger.LogDebug("Init test module [{0}]", moduleType.FullName);
                    var newModule = new TModule();
                    newModule.Setup(this);
                    _modules[moduleType] = newModule;
                }
            }

            return (TModule) _modules[moduleType];
        }
    }
}
