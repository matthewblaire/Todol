using System.Windows.Controls;

namespace Todol
{
    /// <summary>
    /// literally just a grid with a reference to the task that it's supposed to display
    /// </summary>
    public class TaskGrid : Grid
    {
        private Task _task;
        public Task ParentTask { get { return _task; } }
        public TaskGrid(Task task)
        {
            _task = task;
        }
    }

}
