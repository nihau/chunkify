using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace chunkifier
{
    public interface IChunkifier<out T>
    {
        IEnumerable<IEnumerable<T>> Chunks { get; }
    }

    public class Chunkifier<T> : IChunkifier<T>
    {
        private readonly IEnumerable<T> _enumerable;
        private readonly int _chunkSize;

        public Chunkifier(IEnumerable<T> enumerable, int chunkSize)
        {
            _enumerable = enumerable;
            _chunkSize = chunkSize;
        }

        public IEnumerable<IEnumerable<T>> Chunks => _enumerable.Chunkify(_chunkSize);
    }

    class Program
    {
        static void Main(string[] args)
        {
            using var fs = new FileStream("log.log", FileMode.Create);
            using var sw = new StreamWriter(fs);

            //var l = new ListProxy<int>(Enumerable.Range(1, 17).ToList(), sw);
            var l = Enumerable.Range(1, 17)
                .Select(i =>
                {
                    sw.WriteLineAsync(i.ToString());
                    return i;
                })
                .Select(i => i * i);
            const int chunkSize = 7;


            var chunkIterator = 0;
            var iterator = 0;

            /*


            IEnumerable<int> a = l;

            for (var i = 0; i <= 20; ++i)
            {
                a = a.Chunkify(20).Unwrap();
            }

            a = a.ToList();
            */

            foreach (var chunk in l.Chunkify(chunkSize))
            {
                Console.WriteLine();
                Console.Write($"{iterator/chunkSize}) ");
                
                foreach (var chunkElement in chunk)
                {
                    ++chunkIterator;
                    //Console.Write(l[iterator]);
                    Console.Write(' ');
                    //Debug.Assert(chunkElement == l[iterator++]);
                }
                
                Debug.Assert(chunkIterator <= chunkSize);
                chunkIterator = 0;
            }

            /*

            int passes = 0;
            var a = Enumerable.Range(0,6);
            a = a.Select(item =>
            {
                if (item == 0)
                {
                    passes++; sw.WriteLine(passes);
                }
                return item;
            });

            // layer OddsThenEvens 20 times
            for (var xx = 1; xx <= 5; xx++)
            {
                a = a.OddsThenEvens($"branch {xx}", sw); //1
            }

            var asd = a.Last();
            sw.WriteLine(asd);
            sw.WriteLine(passes);
            //Console.WriteLine(asd);
            //Console.WriteLine(passes);*/
        }
    }
}