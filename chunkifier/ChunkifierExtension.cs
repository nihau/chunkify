using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace chunkifier
{

    public static class ChunkifierExtension
    {
        public static IEnumerable<T> Chuggy<T>(this IEnumerator<T> enumerator, int size)
        {
            while (size-- > 0 && enumerator.MoveNext())
            {
                var c = enumerator.Current;

                if (c is null) yield break;

                yield return c;
            }
        }

        public static IEnumerable<IEnumerable<T>> Chunkify<T>(this IEnumerable<T> input, int chunkSize)
        {
            var leader = new MyEnumerator<T>(input);

            for (;;)
            {
                var chunk = Chuggy(leader, chunkSize);

                yield return chunk;

                if (!leader.HasCurrent) yield break;
            }
        }
    }

    public class MyEnumerator<T> : IEnumerator<T>, IEnumerator
    {
        private readonly IEnumerator<T> _enumerator;
        public bool HasCurrent { get; private set; }

        public MyEnumerator(IEnumerable<T> enumeration)
        {
            _enumerator = enumeration.GetEnumerator();
        }

        public void Dispose()
        {
            _enumerator.Dispose();
        }

        public bool MoveNext()
        {
            var result = _enumerator.MoveNext();

            HasCurrent = result;

            return result;
        }

        public void Reset()
        {
            _enumerator.Reset();
        }

        public T Current => _enumerator.Current;

        object IEnumerator.Current => _enumerator.Current;
    }
}