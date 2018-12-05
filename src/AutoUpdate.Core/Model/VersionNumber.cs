using System;

namespace AutoUpdate.Core.Model
{
    public class VersionNumber : IComparable<VersionNumber>, IEquatable<VersionNumber>
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Revision { get; set; }

        public VersionNumber(int major, int minor, int build, int revision)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        public VersionNumber()
            : this(0, 0, 0, 0)
        { }

        public int CompareTo(VersionNumber other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(VersionNumber other)
        {
            throw new NotImplementedException();
        }

        public static bool operator <(VersionNumber a, VersionNumber b)
        {
            throw new NotImplementedException();

        }

        public static bool operator >(VersionNumber a, VersionNumber b)
        {
            if (a == null) return false;
            return b < a;
        }
    }
}
