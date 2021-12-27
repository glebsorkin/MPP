using System;
using System.Threading;

namespace spp_lab7
{
    class Program
    {
        public static void Main(string[] args)
        {
            var tasks = CreateTasks();
            Parallel.WaitAll(tasks);
        }

        private static WaitCallback[] CreateTasks()
        {
            WaitCallback[] tasks = new WaitCallback[100];
            for (var i = 0; i < tasks.Length; i++)
            {
                int index = i;
                tasks[i] = o =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"Task {index + 1} completed.");
                };
            }

            return tasks;
        }
    }
}