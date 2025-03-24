using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Uebung4
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<StringBenchmark>();
        }
    }
    
    public class StringBenchmark
    {
        [Params(100, 1000, 10000)]
        public int Count;

        [Params("abc", "abcdef", "abcdefghijkl")]
        public string Word;

        [Benchmark]
        public void StringConcat()
        {
            string result = "";
            for (int i = 0; i < Count; i++)
            {
                result += Word;
            }
        }

        [Benchmark]
        public void StringBuild()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Count; i++)
            {
                result.Append(Word);
            }
        }
    }
}