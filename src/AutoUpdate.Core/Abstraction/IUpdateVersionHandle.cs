using AutoUpdate.Core.Model;

namespace AutoUpdate.Core.Abstraction
{
    public interface IUpdateVersionHandle
    {
        bool HasNewVersion { get; }
        Version NewVersion { get; }

        void UpdateToNewVersion();
    }
}
