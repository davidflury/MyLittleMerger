using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLittleMerger.Helpers
{
    public static class ListHelper
    {
        public static void AddFirst<T>(this List<List<T>> list, T item)
        {
            list.Add(new List<T>(1) { item });
        }

        public static void AddFirst<TK, TV>(this List<Dictionary<TK, TV>> list, TK key, TV value)
        {
            list.Add(new Dictionary<TK, TV>(1) { { key, value } });
        }

        public static void AddFirst<TK, TV>(this Dictionary<TK, List<TV>> dictionary, TK key, TV value)
        {
            dictionary.Add(key, new List<TV>(1) { value });
        }

        public static int GetHashCodeAggregated<T>(this IEnumerable<T> list)
        {
            return list.Aggregate(0, (x, y) => x.GetHashCode() ^ y.GetHashCode());
        }

        public static bool TryGetByIndex<T>(this IEnumerable<T> array, int index, out T result)
        {
            try
            {
                result = array.ElementAt(index);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static T Second<T>(this IEnumerable<T> list)
        {
            return list.ElementAt(1);
        }

        public static T SecondOrDefault<T>(this IEnumerable<T> list)
        {
            list.TryGetByIndex(1, out var result);
            return result;
        }
    }
}
