
using System.Threading;

namespace spp_lab7
{
    public class Parallel
    {
        public static void WaitAll(WaitCallback[] tasks)
        {
            foreach (var task in tasks)
            {
                ThreadPool.QueueUserWorkItem(task);
            }

            bool readyToStop;
            do
            {
                ThreadPool.GetMaxThreads(out int mWorkers, out int mIoThreads);
                ThreadPool.GetAvailableThreads(out int aWorkers, out int aIoThreads);
                readyToStop = mWorkers == aWorkers;
            } while (!readyToStop);
        }
    }
}