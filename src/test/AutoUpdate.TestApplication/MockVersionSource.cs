using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Model;

namespace AutoUpdate.TestApplication
{
    public class MockVersionSource : IVersionSource
    {
        private readonly string _updateFile;

        public MockVersionSource(string updateFile)
        {
            _updateFile = updateFile;
        }

        public Version[] LoadAvailableVersions()
            => new Version[]
            {
                new Version
                {
                    ChangeLog = "N/A",
                    Mandatory = true,
                    Source = new FileVersionDownloadSource()
                    {
                        FilePath = _updateFile,
                        IsZipFile = true
                    },
                    VersionNumber = new VersionNumber(9, 9, 9, 9)
                }
            };
    }
}
