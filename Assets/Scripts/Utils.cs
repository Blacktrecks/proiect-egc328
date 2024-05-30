using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public static class Utils
    {
        private static readonly Random _random = new();

        public static T RandomElement<T>(this IList<T> list)
        {
            return list[_random.Next(list.Count)];
        }
    }
}
