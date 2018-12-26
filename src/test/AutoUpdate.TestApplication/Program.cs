using System;
using AutoUpdate.Core.Implementation.Builders;

namespace AutoUpdate.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var builder = new UpdateBuilder();

            var servive = builder.UseSource(new MockVersionSource())
                                 .UseAssemblyCurrentVersionDeterminer<Program>()
                                 .ConfigureManualCheck()
                                 .Build();


            servive.Start();

            var handle = servive.SearchVersion().Result;


            if (handle.HasNewVersion)
            {
                handle.UpdateToNewVersion();
            }

        }
    }
}
