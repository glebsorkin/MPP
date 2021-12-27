using System;
using System.Collections.Generic;
using System.Threading;

namespace laba2
{
    public class TaskQueue
    {
		public delegate void TaskDelegate(string sourceFile, string destFile); 

		static List<Thread> threads = new List<Thread>();
		static List<TaskDelegate> TaskDelegates = new List<TaskDelegate>();
		static List<string> filePaths = new List<string>();
		static List<string> destPaths = new List<string>();

		public TaskQueue(int threadsCount)
		{
			threadsCount = Math.Abs(threadsCount);
			
			if (threadsCount == 0)
			{
				threadsCount = 1;
			}
    
			for (int i = 0; i < threadsCount; i++)
			{
				Thread thread = new Thread(ThreadProc);
				thread.IsBackground = true;
				thread.Start();
				threads.Add(thread);
			}
		}

		public void EnqueueTask(TaskDelegate task, string p1, string p2)
		{
			lock (this)
			{
				filePaths.Add(p1);
				destPaths.Add(p2);
				TaskDelegates.Add(task);
			}
		}
              
        public void ThreadProc()
		{
			TaskDelegate del = null;
			string file = "";
			string dest = "";

			while (true)
			{
				lock (this)
				{
					if (TaskDelegates.Count > 0)
					{                  
						del = TaskDelegates[0];
						file = filePaths[0];
                        dest = destPaths[0];

						TaskDelegates.RemoveAt(0);
						filePaths.RemoveAt(0);
						destPaths.RemoveAt(0);
      				}
				}

				if (del != null)
				{
					del(file, dest);
					file = "";
					dest = "";
					del = null;
				}
			}
		}
        

    }
}
