using System.Collections.Generic;
using System.Threading;

namespace MPP_lab1
{
    public class TaskQueue
    {
        public delegate void TaskDelegate();

        private readonly Queue<TaskDelegate> _tasks = new();

        private volatile bool _isStopped;

        public TaskQueue(int threadsNum)
        {
            var threadPool = new Thread[threadsNum];
            for (int i = 0; i < threadsNum; i++)
            {
                threadPool[i] = new Thread(Run);
                threadPool[i].Start();
            }
        }

        public void EnqueueTask(TaskDelegate task)
        {
            lock (_tasks)
            {
                _tasks.Enqueue(task);
            }
        }

        private void Run()
        {
            TaskDelegate task = null;
            var tasksToDo = true;

            while (!_isStopped || tasksToDo)
            {
                lock (_tasks)
                {
                    tasksToDo = _tasks.Count != 0;
                    if (tasksToDo)
                    {
                        task = _tasks.Dequeue();
                    }
                }

                if (task != null)
                {
                    task();
                    task = null;
                }
            }
        }

        public void Stop()
        {
            _isStopped = true;
        }
    }
}