using System.Reflection;
using AutoUpdate.Core.Implementation.CurrentVersionDeterminers;

namespace AutoUpdate.Core.Implementation.Builders
{
    public static class UpdateBuilderCurrentVersionDeterminerExtensions
    {
        public static UpdateBuilder UseAssemblyCurrentVersionDeterminer(this UpdateBuilder updateBuilder, Assembly assembly)
        {
            updateBuilder.UseCurrentVersionDetermine(new AssemblyCurrentVersionDeterminer(assembly));
            return updateBuilder;
        }

        public static UpdateBuilder UseAssemblyCurrentVersionDeterminer<TType>(this UpdateBuilder updateBuilder)
            => UseAssemblyCurrentVersionDeterminer(updateBuilder, typeof(TType).Assembly);
    }
}
