using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.UI.Thread.EXtension
{
    /// <summary>
    /// 对task的一些拓展封装
    /// </summary>
    public static class TaskExtension
    {
        /// <summary>
        /// 异常信息捕捉
        /// </summary>
        /// <param name="task"></param>
        /// <param name="act"></param>
        public static void  AddCaptureException(this Task task,Action<Exception> act)
        {
			try
			{
                task.Start();
			}
			catch (Exception ex)
			{

                act.Invoke(ex);

            }
        }
    }
}
