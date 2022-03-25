using System.Collections.Generic;

namespace Utils
{
    public static class ListExtensions {
        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static List<T> Shuffle<T>(this List<T> ts) {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var r = UnityEngine.Random.Range(i, count);
                (ts[i], ts[r]) = (ts[r], ts[i]);
            }
            return ts;
        }

        public static T PopFirst<T>(this List<T> ts)
        {
            var firstItem = ts[0];
            ts.Remove(firstItem);
            return firstItem;
        }
    }
}