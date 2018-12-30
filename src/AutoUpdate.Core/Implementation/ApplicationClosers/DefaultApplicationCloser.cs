using System;
using AutoUpdate.Abstraction;

namespace AutoUpdate.Core.Implementation.ApplicationClosers
{
    public class DefaultApplicationCloser : IApplicationCloser
    {
        public void CloseApplication()
            => Environment.Exit(0);
    }
}
