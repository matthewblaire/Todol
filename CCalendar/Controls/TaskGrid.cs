using System.Windows.Controls;
using System.Windows.Media;
using Todol.Models;
using System.Windows;

namespace Todol.Controls
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

           
            this.HorizontalAlignment = HorizontalAlignment.Stretch;

            var titleTextBox = new TextBox();
            titleTextBox.Text = task.Title;
            titleTextBox.HorizontalAlignment = HorizontalAlignment.Left;
            titleTextBox.IsReadOnly = true;
            titleTextBox.IsHitTestVisible = false;
            this.Children.Add(titleTextBox);


            var dueDateTextBox = new TextBox();
            dueDateTextBox.HorizontalAlignment = HorizontalAlignment.Right;
            dueDateTextBox.HorizontalContentAlignment = HorizontalAlignment.Right;
            dueDateTextBox.IsHitTestVisible = false;
            dueDateTextBox.IsReadOnly = true;


            if (task.DueDate is not null)
            {
                dueDateTextBox.Text = "Due: " + task.DueDate?.Date.ToString("d");
                this.Children.Add(dueDateTextBox);
            }
            else if (task.IsComplete)
            {
                dueDateTextBox.Text = "DONE";
                dueDateTextBox.Background = Brushes.Green;
                dueDateTextBox.Foreground = Brushes.AliceBlue;
                titleTextBox.Foreground = Brushes.Gray;
                this.Children.Add(dueDateTextBox);
            }
            
        }
    }

}
