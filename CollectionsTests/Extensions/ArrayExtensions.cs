using System;
using System.Collections.Generic;

namespace CollectionsTests.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Shuffle<T>(this T[] array)
        {
            var random = new Random();
            var len = array.Length;

            var res = new T[len];

            for (var i = 0; i < len; i++)
            {
                var pos = random.Next(0, len);
                while(!EqualityComparer<T>.Default.Equals(res[pos], default))
                {
                    pos++;
                    if(pos >= len)
                    {
                        pos = 0;
                    }
                }

                res[pos] = array[i];
            }

            return res;

        }
    }
}
