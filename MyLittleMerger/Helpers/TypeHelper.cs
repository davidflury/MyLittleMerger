using System;

namespace MyLittleMerger.Helpers
{
    public static class TypeHelper
    {
        public static bool TryGetUnderlyingNullable(this Type type, out Type underlying)
        {
            underlying = Nullable.GetUnderlyingType(type);
            return underlying != null;
        }

        public static string ToStringOrNull(object value)
        {
            return value == null ? "NULL" : value.ToString();
        }
    }
}
