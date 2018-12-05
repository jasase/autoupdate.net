using System;
using System.Reflection;

namespace AutoUpdate.Core.Implementation.Builders
{
    public static class UpdateBuilderCurrentVersionDeterminerExtensions
    {
        public static UpdateBuilder UseAssemblyCurrentVersionDeterminer(this UpdateBuilder updateBuilder, Assembly assembly)
        {
            var t = 0;
            throw new NotImplementedException();
        }

        public static UpdateBuilder UseAssemblyCurrentVersionDeterminer<TType>(this UpdateBuilder updateBuilder)
        {
            var t = 0;
            throw new NotImplementedException();
        }
    }
}
