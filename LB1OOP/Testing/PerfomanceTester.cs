using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP
{
    public static class PerfomanceTester
    {
        private const int Count = 500_000;

        public static TestResults RunTests()
        {
            var results = new TestResults();
            var dataList = GenerateData(Count);

            var sw = Stopwatch.StartNew();
            var test_list = new List<InternetProvider>();
            foreach (var p in dataList) test_list.Add(p);
            sw.Stop();
            results.ListAddTime = sw.ElapsedMilliseconds;

            sw.Restart();
            float sum = 0f;
            foreach (var p in test_list) sum += p.TarifCoast;
            sw.Stop();
            results.ListSeqReadTime = sw.ElapsedMilliseconds;

            sw.Restart();
            var rand = new Random();
            for (int i = 0; i < Count; i++)
            {
                var idx = rand.Next(Count);
                var p = test_list[idx];
            }
            sw.Stop();
            results.ListRandomReadTime = sw.ElapsedMilliseconds;

            sw.Restart();
            var array = new InternetProvider[Count];
            for (int i = 0; i < Count; i++) array[i] = dataList[i];
            sw.Stop();
            results.ArrayAddTime = sw.ElapsedMilliseconds;

            sum = 0;
            sw.Restart();
            foreach (var p in array) sum += p.TarifCoast;
            sw.Stop();
            results.ArraySeqReadTime = sw.ElapsedMilliseconds;

            sw.Restart();
            for (int i = 0; i < Count; i++)
            {
                var idx = rand.Next(Count);
                var p = array[idx];
            }
            sw.Stop();
            results.ArrayRandomReadTime = sw.ElapsedMilliseconds;

            return results;
        }

        private static List<InternetProvider> GenerateData(int count)
        {
            var list = new List<InternetProvider>();
            var rand = new Random();

            for (int i = 0; i < count; i++)
            {
                string name = $"Provider_{i}";
                float cost = rand.Next(100, 10000);
                int users = rand.Next(100, 10000);
                float speed = rand.Next(50, 1000);
                float area = rand.Next(10, 1000);
                int contract = 100_000_000 + i;
                string tarif = $"Tarif_{i % 10}";

                list.Add(new InternetProvider(name, cost, users, speed, area, contract, tarif));
            }
            return list;
        }
    }

    public class TestResults
    {
        public long ListAddTime { get; set; }
        public long ListSeqReadTime { get; set; }
        public long ListRandomReadTime { get; set; }
        public long ArrayAddTime { get; set; }
        public long ArraySeqReadTime { get; set; }
        public long ArrayRandomReadTime { get; set; }
    }
}
