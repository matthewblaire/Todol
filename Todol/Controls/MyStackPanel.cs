using System.Windows.Controls;
using Todol.Models;

namespace Todol.Controls
{
    /// <summary>
    /// Literally just the regular StackPanel, but I added a property called ParentTask that points to the task that created the StackPanel
    /// </summary>
    public class MyStackPanel : StackPanel
    {
        private Task _parentTask;
        public Task ParentTask { get { return _parentTask; } }
        public MyStackPanel(Task parentTask)
        {
            _parentTask = parentTask;
        }
    }
}
