using System;

namespace AutoUpdate.Core.Model
{
    public class VersionNumber : IComparable<VersionNumber>, IEquatable<VersionNumber>
    {
        private const int THIS_SMALL_THAN_OTHER = -1;
        private const int THIS_GREATER_THAN_OTHER = 1;
        private const int THIS_EQUAL_OTHER = 0;

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
            if (other == null)
            {
                return THIS_GREATER_THAN_OTHER;
            }
            else if (Major != other.Major)
            {
                return Major > other.Major ? THIS_GREATER_THAN_OTHER : THIS_SMALL_THAN_OTHER;
            }
            else if (Minor != other.Minor)
            {
                return Minor > other.Minor ? THIS_GREATER_THAN_OTHER : THIS_SMALL_THAN_OTHER;
            }
            else if (Build != other.Build)
            {
                return Build > other.Build ? THIS_GREATER_THAN_OTHER : THIS_SMALL_THAN_OTHER;
            }
            else if (Revision != other.Revision)
            {
                return Revision > other.Revision ? THIS_GREATER_THAN_OTHER : THIS_SMALL_THAN_OTHER;
            }
            else
            {
                return 0;
            }
        }

        public bool Equals(VersionNumber other)
            => CompareTo(other) == 0;

        public override bool Equals(object obj)
            => obj is VersionNumber &&
               Equals((VersionNumber) obj);

        public static bool operator <(VersionNumber a, VersionNumber b)
        {
            if (a == null) return true;
            return a.CompareTo(b) < 0;
        }

        public static bool operator >(VersionNumber a, VersionNumber b)
        {
            if (a == null) return false;
            return a.CompareTo(b) > 0;
        }

        public override string ToString()
            => $"{Major}.{Minor}.{Build}.{Revision}";

        public override int GetHashCode()
        {
            var hashCode = -1452750829;
            hashCode = hashCode * -1521134295 + Major.GetHashCode();
            hashCode = hashCode * -1521134295 + Minor.GetHashCode();
            hashCode = hashCode * -1521134295 + Build.GetHashCode();
            hashCode = hashCode * -1521134295 + Revision.GetHashCode();
            return hashCode;
        }
    }
}
