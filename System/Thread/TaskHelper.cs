using Polly.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.UI.Thread.Base;

namespace System.UI.Thread
{
    /// <summary>
    /// 线程帮助类用于线程的
    /// </summary>
    public  class TaskHelper:ITaskHelper
    {
        public bool IsRunning { get; private set; }
        public TaskFactory CTXTaskFactory{ get;private set; }

        /// <summary>
        /// TODO可以换成字典线程安全这样可以防止重复
        /// </summary>
        private ConcurrentQueue<Func<Task>> ExecuteTask=new ConcurrentQueue<Func<Task>>();
        private TaskHelper(TaskFactory ctxTaskFactory)
        {
            CTXTaskFactory=ctxTaskFactory;
            IsRunning = true;
            ctxTaskFactory.StartNew(TaskJob, TaskCreationOptions.LongRunning);
        }

        public void TaskJob()
        {
            while (IsRunning)
            {
                var val = Threading.Thread.CurrentThread.ManagedThreadId;
              if (ExecuteTask.TryDequeue(out var task))
                {
                    CTXTaskFactory.StartNew(task);
                }
                Threading.Thread.Sleep(50);
            }
        }

        public void StopTaskJob() 
        {
            IsRunning = false;
            //TODO把任务池中的任务执行完毕才真正关闭
        }

        public void AddTask(Func<Task> task)
        {
            if (IsRunning)
            {
                ExecuteTask.Enqueue(task);
            }
            else
            {
                throw new InvalidOperationException("任务线程被关闭无法进行任务执行");
            }
        }

        public Task CraetTask(Action task)
        {
           return CTXTaskFactory.StartNew(task);
        }
        public static TaskHelper Builde()
        {
            //  TaskScheduler.FromCurrentSynchronizationContext() 使用这个最终创建的Task都会回到前的线程上下文
            return new TaskHelper(new TaskFactory());
            //   return new TaskHelper(new TaskFactory());
        }
    }
}
