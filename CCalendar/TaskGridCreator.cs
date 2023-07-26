using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Todol
{


    public static class TaskGridCreator
    {



        /// <summary>
        /// Returns a grid with two TextBox objects reflecting Task.Title and Task.DueDate
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static TaskGrid CreateTaskGrid(Task t)
        {
            var grid = new TaskGrid(t);
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            var titleTextBox = new TextBox();
            titleTextBox.Text = t.Title;
            titleTextBox.HorizontalAlignment = HorizontalAlignment.Left;
            titleTextBox.IsReadOnly = true;
            titleTextBox.IsHitTestVisible = false;
            grid.Children.Add(titleTextBox);


            var dueDateTextBox = new TextBox();
            dueDateTextBox.HorizontalAlignment = HorizontalAlignment.Right;
            dueDateTextBox.HorizontalContentAlignment = HorizontalAlignment.Right;
            dueDateTextBox.IsHitTestVisible = false;
            dueDateTextBox.IsReadOnly = true;
            

            if (t.DueDate is not null)
            {
                dueDateTextBox.Text = "Due: "+t.DueDate?.Date.ToString("d");
                grid.Children.Add(dueDateTextBox);
            }
            else if (t.IsComplete)
            {
                dueDateTextBox.Text = "DONE";
                dueDateTextBox.Background = Brushes.Green;
                dueDateTextBox.Foreground = Brushes.AliceBlue;
                titleTextBox.Foreground = Brushes.Gray;
                grid.Children.Add(dueDateTextBox);
            }


            
            


            return grid;
        }

    }

}
