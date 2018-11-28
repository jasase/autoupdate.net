using System.Collections.Generic;
using System.IO;
using AutoUpdate.Core.Model;

namespace AutoUpdate.Core.Abstraction
{
    public interface IVersionParser
    {
        IEnumerable<Version> ParseVersion(Stream content);
    }
}
