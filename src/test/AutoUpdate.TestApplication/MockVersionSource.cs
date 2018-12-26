using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Model;

namespace AutoUpdate.TestApplication
{
    public class MockVersionSource : IVersionSource
    {
        public Core.Model.Version[] LoadAvailableVersions()
            => new Core.Model.Version[]
            {
                new Core.Model.Version
                {
                    ChangeLog = "N/A",
                    Mandatory = true,
                    Source = null,
                    VersionNumber = new VersionNumber(9, 9, 9, 9)
                }
            };
    }
}
