using System.Collections.Generic;
using System.IO;

namespace chunkifier
{
    public static class E
    {
        private static int x = 0;
        public static IEnumerable<T> OddsThenEvens<T>(this IEnumerable<T> items, string branch, TextWriter log) {
            log.WriteLine($"OddsThenEvens {++x}");
            log.WriteLine("enter " + branch);

            var i = 0;
            foreach (var item in items) {
                
                if (i++ == 0) yield return item;
            }

            i = 1;
            foreach (var item in items)
            {

                if (i++ == 0) yield return item;
            }
            i = 2;
            foreach (var item in items)
            {

                if (i++ == 0) yield return item;
            }

            log.WriteLine("exit " + branch);

            yield break;
        }

        public static IEnumerable<T> Unwrap<T>(this IEnumerable<IEnumerable<T>> unwrappee)
        {
            foreach (var i in unwrappee)
            {
                foreach (var j in i)
                {
                    yield return j;
                }
            }
        }
    }
}