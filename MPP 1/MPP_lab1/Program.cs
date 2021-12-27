using System;
using System.IO;
using System.Threading;

namespace MPP_lab1
{
    class Program
    {
        private static TaskQueue _taskQueue;
        private static int _numCopiedFiles = 0;

        static void Main(string[] args)
        {
            Lab1();
            //Lab2("C:\\test", "D:\\copied_test");
        }

        static void Lab1()
        {
            _taskQueue = new TaskQueue(7);

            for (int i = 0; i < 21; i++)
            {
                int j = i;
                _taskQueue.EnqueueTask(() =>
                {
                    for (int k = j; k < j + 1; k++)
                    {
                        Console.Out.WriteLine("k = {0}", k);
                        Thread.Sleep(1000);
                    }
                });
            }

            _taskQueue.Stop();
        }

        static void Lab2(string src, string dest)
        {
            _taskQueue = new TaskQueue(5);
            CopyFiles(src, dest);
            _taskQueue.Stop();
            Console.Out.WriteLine("Number of copied files: {0}", _numCopiedFiles);
        }

        static void CopyFiles(string src, string dest)
        {
            DirectoryInfo srcDirectory = new DirectoryInfo(src);
            DirectoryInfo destDirectory = new DirectoryInfo(dest);
            dest += "\\";
            if (!srcDirectory.Exists)
            {
                Console.WriteLine("Source directory doesn't exists, please check your input!");
                return;
            }

            if (!destDirectory.Exists)
            {
                Console.WriteLine("Destination directory doesn't exists, but will be created!");
                destDirectory.Create();
            }

            foreach (FileInfo file in srcDirectory.GetFiles())
            {
                string currDest = dest;
                _taskQueue.EnqueueTask(() =>
                {
                    file.CopyTo(currDest + file.Name, true);
                });
                _numCopiedFiles++;
            }

            foreach (DirectoryInfo directory in srcDirectory.GetDirectories())
            {
                CopyFiles(directory.FullName, dest + directory.Name);
            }
        }
    }
}
