using Sahurjt.Signalr.Dashboard.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahurjt.Signalr.Dashboard
{
    class LongRunningTaskQueue
    {
        //  wait for 1 ms before checking if a new task is available to execute
        private static readonly int _loopWaitTime = 1;
        private static bool _isQueueEmpty = true;
        private static readonly Queue<Task> _tasks = new Queue<Task>();

        public static void Enqueue(Action operation)
        {
            if (operation == null) throw new ArgumentNullException("Operation delegate must not be null");

            _tasks.Enqueue(new Task(operation));

            LogHelper.Log(" task enqueued");

            if (_isQueueEmpty)
            {
                _isQueueEmpty = false;
                ProcessTaskQueue();
            }
        }

        private static void ProcessTaskQueue()
        {
            while (!_isQueueEmpty)
            {
                if (_tasks.Count > 0)
                {
                    var currentTask = _tasks.Dequeue();

                    currentTask.RunSynchronously();
                    currentTask.Wait();
                }
                else
                {
                    _isQueueEmpty = true;
                }
                Task.Delay(_loopWaitTime);
            }
        }
    }
}
