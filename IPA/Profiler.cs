using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace IPA
{
    class Profiler
    {
        class PerformanceSnapshot
        {
            public string name;
            public long exucutionTime;
            public long memoryFootprint;

            internal Stopwatch stopwatch;
        }

        private Dictionary<string, PerformanceSnapshot> entries;

        public Profiler()
        {
            entries = new Dictionary<string, PerformanceSnapshot>();
        }

        public void begin(string id)
        {
            PerformanceSnapshot snapshot = new PerformanceSnapshot();
            snapshot.stopwatch = new Stopwatch();
            snapshot.name = id;
            snapshot.exucutionTime = 0;
            snapshot.memoryFootprint = GC.GetTotalMemory(true);

            entries.Add(id, snapshot);

            snapshot.stopwatch.Start();
        }

        public void end(string id)
        {
            PerformanceSnapshot entry = entries[id];

            entry.stopwatch.Stop();
            entry.exucutionTime = entries[id].stopwatch.ElapsedMilliseconds;
            entry.memoryFootprint = GC.GetTotalMemory(true) - entries[id].memoryFootprint;
        }

        public void process()
        {
            Console.WriteLine("Rezultatai:");
            foreach(PerformanceSnapshot entry in entries.Values)
            {
                Console.WriteLine("\t" + entry.name + ": " + entry.exucutionTime + "ms | " + Math.Round((double)entry.memoryFootprint / 1024.0, 3) + "KB");
            }

            List<PerformanceSnapshot> list = entries.Values.ToList();
            list.Sort((PerformanceSnapshot o, PerformanceSnapshot t) => o.exucutionTime.CompareTo(t.exucutionTime));

            PerformanceSnapshot baseline = list.First();

            Console.WriteLine("Surusiuoti rezultatai:");
            int index = 1;
            foreach (PerformanceSnapshot entry in list)
            {
                long executionTimeDiff = entry.exucutionTime - baseline.exucutionTime;
                double executionTimeDiffPer = Math.Round((((double)Math.Abs(entry.exucutionTime - baseline.exucutionTime)) / ((double)(baseline.exucutionTime + entry.exucutionTime) / 2.0)) * 100.0, 2);

                double memDiff = Math.Round((entry.memoryFootprint - baseline.memoryFootprint) / 1024.0, 3);
                double entryMem = Math.Round((double)entry.memoryFootprint / 1024.0, 3);
                double baselineMem = Math.Round((double)baseline.memoryFootprint / 1024.0, 3);
                double memDiffPer = Math.Round((((double)Math.Abs(entryMem - baselineMem)) / ((double)(baselineMem + entryMem) / 2.0)) * 100.0, 2);

                Console.WriteLine("\t" + index++ + ". " + entry.name + ": " + entry.exucutionTime + "ms [" + executionTimeDiff + "ms] [" + executionTimeDiffPer + "%] | " + Math.Round((double)entry.memoryFootprint / 1024.0, 3) + "KB [" + memDiff + "KB] [" + memDiffPer + "%]");
            }
        }
    }
}
