using AutoUpdate.Core.Model;

namespace AutoUpdate.Core.Abstraction
{
    public interface ICurrentVersionDeterminer
    {
        VersionNumber DetermineCurrentVersionNumber();
    }
}
