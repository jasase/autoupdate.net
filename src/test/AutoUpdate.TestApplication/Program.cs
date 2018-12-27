using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using AutoUpdate.Core.Implementation.Builders;
using AutoUpdate.Core.Implementation.UpdatePreparationSteps;

namespace AutoUpdate.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            while (!Debugger.IsAttached)
            {
                Thread.Sleep(250);
            }

            Console.WriteLine("Hello World!");

            var executionDirectory = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var parentDirectory = Path.GetDirectoryName(executionDirectory);
            var zipFile = Path.Combine(parentDirectory, "update.zip");
            File.Delete(zipFile);

            ZipFile.CreateFromDirectory(executionDirectory, zipFile);

            var builder = new UpdateBuilder();
            var servive = builder.UseSource(new MockVersionSource(zipFile))
                                 .UseAssemblyCurrentVersionDeterminer<Program>()
                                 .AddUpdatePreparationStep(new SimpleReplacePrepareStep(executionDirectory))
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
