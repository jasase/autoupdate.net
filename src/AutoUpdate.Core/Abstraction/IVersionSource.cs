using System;

namespace AutoUpdate.Core.Abstraction
{
    public interface IVersionSource
    {
        Version[] LoadAvailableVersions();
    }

}
