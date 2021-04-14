using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace chunkifier
{
    public class ListProxy<T> : IEnumerable, IEnumerable<T>
    {
        private readonly List<T> _l;
        private readonly TextWriter _tw;

        public ListProxy(List<T> l,TextWriter tw)
        {
            _l = l;
            _tw = tw;
        }

        public T this[int index]
        {
            get
            {
                _tw.WriteLine(_l[index]);
                return _l[index];
            }
        }


        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new LListEnumerator<T>(_l.GetEnumerator(), _tw);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LListEnumerator<T>(_l.GetEnumerator(), _tw);
        }

        private sealed class LListEnumerator<T> : IEnumerator, IEnumerator<T>
        {
            private readonly IEnumerable<T> _enumerable;
            private readonly TextWriter _tw;
            private IEnumerator<T> _enumerator;
            private static int instanceShared = 0;
            private static int _globaliterations = 0;
            private int _instance;
            private int x = 0;

            T IEnumerator<T>.Current => _enumerator.Current;

            object IEnumerator.Current => _enumerator.Current;

            public LListEnumerator(IEnumerator<T> enumerator, TextWriter tw)
            {
                _instance = instanceShared++;
                _enumerator = enumerator;
                _tw = tw;
                _tw.WriteLine($"create LListEnumerator instance {_instance}");
            }

            void System.IDisposable.Dispose()
            {
                _enumerator.Dispose();
            }

            bool IEnumerator.MoveNext()
            {
                _tw.WriteLine($"instance {_instance} move next {x++}; global iterations {_globaliterations++}");
                return _enumerator.MoveNext();
            }

            void IEnumerator.Reset()
            {
                _enumerator.Reset();
            }
        }
    }
}