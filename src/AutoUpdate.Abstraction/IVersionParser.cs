using System.Collections.Generic;
using System.IO;
using AutoUpdate.Abstraction.Model;

namespace AutoUpdate.Abstraction
{
    public interface IVersionParser
    {
        IEnumerable<Version> ParseVersion(Stream content);
    }
}
