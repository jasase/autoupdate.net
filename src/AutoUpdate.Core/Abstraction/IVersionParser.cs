using System;
using System.Collections.Generic;
using System.IO;

namespace AutoUpdate.Core.Abstraction
{
    public interface IVersionParser
    {
        IEnumerable<Version> ParseVersion(Stream content);
    }
}
