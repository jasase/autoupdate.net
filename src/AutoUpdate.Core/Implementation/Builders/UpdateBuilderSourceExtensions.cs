using System;
using AutoUpdate.Abstraction;
using AutoUpdate.Core.Implementation.VersionParsers;
using AutoUpdate.Core.Implementation.VersionSources;

namespace AutoUpdate.Core.Implementation.Builders
{
    public static class UpdateBuilderSourceExtensions
    {
        //public static UpdateBuilderSourceParser UseHttpSource(this UpdateBuilder updateBuilder, string url)
        //{
        //    var t = 0;
        //    return new UpdateBuilderSourceParser();
        //}

        public static UpdateBuilderSourceParser UseHttpSource(this UpdateBuilder updateBuilder, IHttpClientFactory httpClientFactory)
            => new UpdateBuilderSourceParser(updateBuilder,
                                             p => new HttpVersionSource(updateBuilder.LoggerFactory,
                                                                        p,
                                                                        httpClientFactory)
                                             );

        //public static UpdateBuilderSourceParser UseFileSource(this UpdateBuilder updateBuilder, string url)
        //{

        //    return new UpdateBuilderSourceParser(updateBuilder, );
        //}
    }

    public class UpdateBuilderSourceParser
    {
        private readonly UpdateBuilder _updateBuilder;
        private readonly Func<IVersionParser, IVersionSource> _creator;

        public UpdateBuilderSourceParser(UpdateBuilder updateBuilder, Func<IVersionParser, IVersionSource> creator)
        {
            _updateBuilder = updateBuilder;
            _creator = creator;
        }

        public UpdateBuilder UseDefaultXmlParser()
        {
            _updateBuilder.UseSource(_creator(new XmlVersionParser(_updateBuilder.LoggerFactory)));
            return _updateBuilder;
        }

        //public UpdateBuilder UseDefaultJsonParser()
        //{
        //    throw new NotImplementedException();
        //    return _updateBuilder;
        //}

        public UpdateBuilder UseParser(IVersionParser versionParser)
        {
            _updateBuilder.UseSource(_creator(versionParser));
            return _updateBuilder;
        }
    }
}
