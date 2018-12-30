using System.Reflection;
using AutoUpdate.Abstraction;
using AutoUpdate.Abstraction.Model;

namespace AutoUpdate.Core.Implementation.CurrentVersionDeterminers
{
    public class AssemblyCurrentVersionDeterminer : ICurrentVersionDeterminer
    {
        private readonly Assembly _assembly;

        public AssemblyCurrentVersionDeterminer(Assembly assembly)
        {
            _assembly = assembly;
        }

        public VersionNumber DetermineCurrentVersionNumber()
        {
            var versionAssembly = _assembly.GetName().Version;
            return new VersionNumber(versionAssembly.Major,
                                     versionAssembly.Minor,
                                     versionAssembly.Build,
                                     versionAssembly.Revision);
        }

    }
}
