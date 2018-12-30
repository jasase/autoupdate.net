using AutoUpdate.Abstraction.Model;

namespace AutoUpdate.Abstraction
{
    public interface IVersionSource
    {
        Version[] LoadAvailableVersions();
    }

}
