using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Todol.Controls;
using Todol.Services;

namespace Todol.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly FilterWindow _filterWindow;

        public bool FilterIsShowing
        {
            get
            {
                return _filterWindow.Visibility == System.Windows.Visibility.Visible;
            }
        }
        public MainWindowViewModel()
        {
            _filterWindow = new FilterWindow();
        }

        public void PopulateListBox(ListBox listBox)
        {
            if (TaskManager.GetTaskList().Count > 0)
            {
                foreach (var task in TaskManager.GetTaskList(_filterWindow.Filter))
                {

                    if (task.IsComplete && task.DueDate is null)
                    {
                        var insertPosition = listBox.Items.Count >= 0 ? listBox.Items.Count : 0;
                        var grid = new TaskGrid(task);
                        listBox.Items.Insert(insertPosition, grid);
                    }
                    else
                    {
                        var grid = new TaskGrid(task);
                        listBox.Items.Insert(0, grid);
                    }

                }
            }
        }

        public void OpenFilterWindow(double left, double top)
        {
            _filterWindow.Left = left;
            _filterWindow.Top = top;
            _filterWindow.Show();
        }

        public void HideFilterWindow()
        {
            _filterWindow.Hide();
        }

        public void CloseFilterWindow()
        {
            _filterWindow.Close();
        }

        /// <summary>
        /// Task deletion function that works with the selectedItems IList
        /// </summary>
        /// <param name="selectedItems"></param>
        public void DeleteTasks(System.Collections.IList selectedItems)
        {
            var tasksToRemove = new List<Models.Task>();
            foreach (var item in selectedItems)
            {
                if (item is TaskGrid grid)
                {
                    Models.Task task = grid.ParentTask;
                    tasksToRemove.Add(task);
                }
            }

            //Delete the tasks from the TaskManager
            foreach (var task in tasksToRemove)
            {
                TaskManager.RemoveTask(task);
            }
        }




    }
}
