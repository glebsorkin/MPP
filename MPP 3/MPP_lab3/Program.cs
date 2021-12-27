using System;
using System.Threading;

namespace MPP_lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Mutex m = new Mutex();
            for (int i = 0; i < 7; i++)
            {
                new Thread(() =>
                    {
                        m.Lock();
                        Console.WriteLine("ID of thread that locked others: {0}",
                            Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(1000);
                        m.Unlock();
                        Console.WriteLine("ID of thread that unlocked others: {0}",
                            Thread.CurrentThread.ManagedThreadId);

                    }
                ).Start();
            }
        }
    }
}