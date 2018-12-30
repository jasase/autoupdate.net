using AutoUpdate.Abstraction.Model;

namespace AutoUpdate.Abstraction
{
    public interface IUpdateVersionHandle
    {
        bool HasNewVersion { get; }
        Version NewVersion { get; }

        void UpdateToNewVersion();
    }
}
