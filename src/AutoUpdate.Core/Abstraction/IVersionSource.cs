using AutoUpdate.Core.Model;

namespace AutoUpdate.Core.Abstraction
{
    public interface IVersionSource
    {
        Version[] LoadAvailableVersions();
    }

}
