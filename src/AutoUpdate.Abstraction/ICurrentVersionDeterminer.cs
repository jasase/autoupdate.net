using AutoUpdate.Abstraction.Model;

namespace AutoUpdate.Abstraction
{
    public interface ICurrentVersionDeterminer
    {
        VersionNumber DetermineCurrentVersionNumber();
    }
}
