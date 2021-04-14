using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var l = new List<string>() { "1", "2", "3", "4", "5" };
            var x = l.Skip(3).Take(1).ToList();
        }
    }
}