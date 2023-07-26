using System;
namespace Todol
{
    public class TaskListUpdateEventArgs : EventArgs
    {
        public Task NewTask { get; set; }
        public bool UserCreatedTask { get; set; }

        public TaskListUpdateEventArgs(bool userCreatedTask, Task newTask)
        {
            NewTask = newTask;
            UserCreatedTask = userCreatedTask;
        }
    }
}
