using System.Collections.Generic;

namespace EntityFrameworkCoreDemo.Helpers
{
    public static class CommonHelpers
    {
        public static T2 GetValue<T1, T2>(this Dictionary<T1, T2> dict, T1 key)
        {
            T2 result;
            dict.TryGetValue(key, out result);
            return result;
        }
    }
}