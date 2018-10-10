using System.Collections.Generic;
using System.Linq;

namespace MvvmCross.Plugin.Validation
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> s)
        {
            return s == null || !s.Any();
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> s)
        {
            return !s.IsNullOrEmpty();
        }

        public static string Collect<T>(this IEnumerable<KeyValuePair<T, T>> pairs)
        {
            return string.Join("\r\n", pairs.Select(e => e.Value));
        }
    }
}