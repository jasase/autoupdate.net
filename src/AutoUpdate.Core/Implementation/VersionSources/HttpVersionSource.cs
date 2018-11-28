using AutoUpdate.Core.Abstraction;

namespace AutoUpdate.Core.Implementation.VersionSources
{
    public class HttpVersionSource : VersionSourceWithParser
    {
        public HttpVersionSource(IVersionParser parser)
            : base(parser)
        { }
    }
}
