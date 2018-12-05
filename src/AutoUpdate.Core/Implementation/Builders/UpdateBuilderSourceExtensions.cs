using System;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Implementation.VersionSources;

namespace AutoUpdate.Core.Implementation.Builders
{
    public static class UpdateBuilderSourceExtensions
    {
        public static UpdateBuilderSourceParser UseHttpSource(this UpdateBuilder updateBuilder, string url)
        {
            var t = 0;
            return new UpdateBuilderSourceParser();
        }

        public static UpdateBuilderSourceParser UseHttpSource(this UpdateBuilder updateBuilder, IHttpClientFactory httpClientFactory)
        {
            var t = 0;
            return new UpdateBuilderSourceParser();
        }

        public static UpdateBuilderSourceParser UseFileSource(this UpdateBuilder updateBuilder, string url)
        {
            var t = 0;
            return new UpdateBuilderSourceParser();
        }
    }

    public class UpdateBuilderSourceParser
    {
        public UpdateBuilder UseDefaultXmlParser()
        {
            var t = 0;
            throw new NotImplementedException();
        }

        public UpdateBuilder UseDefaultJsonParser()
        {
            var t = 0;
            throw new NotImplementedException();
        }

        public UpdateBuilder UseParser(IVersionParser versionParser)
        {
            var t = 0;
            throw new NotImplementedException();
        }
    }
}
