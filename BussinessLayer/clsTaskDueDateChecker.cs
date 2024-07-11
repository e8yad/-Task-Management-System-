using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using BusinessLayer;
// separate this  logic from  task class to easy to maintain tas class and to avoid affect on performance of task class   
namespace BusinessLayer
{
    // very important to review again 
    // core of program
    // static to avid making object form this class
    public  static class clsTaskDueDateChecker
    {
        public static event EventHandler<TaskOverDueEventArgs> TaskOverDueEvent;
        //timer make new thread to call function after interval pass
        // system.threading timer 
        // select one of empty thread pool to execute function on it 
        private static Timer _Timer;
        private static object _lock = new object();

        // you can send cancellation token using object t 
        public static async Task Start(List<clsTask> TasksList,TimeSpan Interval)
        {
            // after interval it will call   _CheckDueDate
            _Timer = new Timer( async (object t )=> await _CheckDueDate(TasksList),null,TimeSpan.Zero, Interval);
        }

        public static void Stop()
        {
            _Timer?.Dispose();
        }

        /// <summary>
        /// logic to check is there any task over due 
        /// </summary>
        /// <param name="TasksList">List of tasks you want to check </param>
        /// <returns></returns>

        private static async Task _CheckDueDate(List<clsTask> TasksList)
        {
            if (TasksList == null) return;
            List<clsTask> overDueTasks = new List<clsTask>();
            lock(_lock)
            {
                foreach (var task in TasksList)
                {

                    if ((task.status==clsTask.enStatus.Normal)&&(task.DueDate != null && task.DueDate < DateTime.Now))
                    {
                        task.status=clsTask.enStatus.Overdue;
                        overDueTasks.Add(task);
                    }


                 }
            }
            if (overDueTasks == null || overDueTasks.Count == 0)
                return;

            foreach (var task in overDueTasks)
            {
                // non blocking waiting
                // thread will go to execute update if any waiting it will go out and execute RaiseOnOverDueTask  async 
                Task t = task.UpdateTaskStateAsync();
                _RaiseOnOverDueTask(task);
                await t;
            }
            
        }



        private static void _RaiseOnOverDueTask(clsTask Task)
        {
            TaskOverDueEvent?.Invoke( null,new TaskOverDueEventArgs(Task));
        }
    }
    // Package for event handler 
    public class TaskOverDueEventArgs : EventArgs
    {
        public clsTask Task { get; }
        public TaskOverDueEventArgs(clsTask task)
        {
            this.Task = task;
        }
    }
}
