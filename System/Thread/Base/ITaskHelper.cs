using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.UI.Thread.Base
{
    public interface ITaskHelper
    {
        public bool IsRunning { get; }
        public TaskFactory CTXTaskFactory { get;  }

  
        public void StopTaskJob();

        public void AddTask(Func<Task> task);
        public Task CraetTask(Action task);
    }
}
