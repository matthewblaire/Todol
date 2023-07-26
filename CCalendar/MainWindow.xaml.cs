using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Todol
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static FilterWindow filterWindow;

        private DateTime? _selectedDate;
        public MainWindow()
        {
            InitializeComponent();
            TaskManager.TaskListSizeChanged += UpdateListBox;
            Task.TaskUpdated += UpdateListBox;
            filterWindow = new FilterWindow();
            FilterWindow.FilterUpdated += UpdateListBox;
        }

        /// <summary>
        /// Update the listBox with all tasks whenever the task list changes size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateListBox(object? sender, EventArgs e)
        {
            listBox.Items.Clear();

            if (TaskManager.GetTaskList().Count > 0)
            {
                foreach (var task in TaskManager.GetTaskList(filterWindow.Filter))
                {

                    if (task.IsComplete && task.DueDate is null)
                    {
                        var insertPosition = listBox.Items.Count >= 0 ? listBox.Items.Count : 0;
                        listBox.Items.Insert(insertPosition, TaskGridCreator.CreateTaskGrid(task));
                    }
                    else
                    {
                        listBox.Items.Insert(0, TaskGridCreator.CreateTaskGrid(task));
                    }

                }

                

            }
        }


        /// <summary>
        /// Clear the current 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datePicker_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            _selectedDate = datePicker.SelectedDate;
            UpdateListBox(sender, e);

        }



        /// <summary>
        /// This method resets the DatePicker to the current day
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetDateToToday_Click(object sender, RoutedEventArgs e)
        {
            datePicker.SelectedDate = DateTime.Now.Date;
            
        }

        /// <summary>
        /// Open and show New Task window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewTask_Click(object sender, RoutedEventArgs e)
        {
            
            NewTaskWindow newTaskWindow = new NewTaskWindow();
            var offsetX = 0;
            var offsetY = 0;
            newTaskWindow.Left = this.Left + offsetX;
            newTaskWindow.Top = this.Top + offsetY;
            newTaskWindow.ShowDialog();
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TaskGrid grid = (TaskGrid)listBox.SelectedItem;
            var offsetX = 50;
            var offsetY = 50;
            

            var selectedTask = grid.ParentTask;
            var viewTask = new ViewTaskWindow(selectedTask);

            viewTask.Left = this.Left + offsetX;
            viewTask.Top = this.Top + offsetY;
            viewTask.Show();
        }

        private void btnExportTaskList_Click(object sender, RoutedEventArgs e)
        {
            TaskManager.SaveTasks();
        }

        private void btnImportTaskList_Click(object sender, RoutedEventArgs e)
        {
            TaskManager.LoadTasks();
            UpdateListBox(sender, e);
        }

        private void datePicker_Loaded(object sender, RoutedEventArgs e)
        {
            datePicker.SelectedDate = DateTime.Now.Date;
        }

        private void listBox_Loaded(object sender, RoutedEventArgs e)
        {
            TaskManager.LoadTasks();
            UpdateListBox(sender, e);  
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            TaskManager.SaveTasks();
            filterWindow.Close();
        }

        private void ContextMenu_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ShowDeleteConfirmationDialog())
            {
                List<Task> tasksToRemove = new List<Task>();
                //Add selected tasks to their own list, that way we don't run into overflow issues when we change the size of the listBox list.
                foreach (var item in listBox.SelectedItems)
                {
                    TaskGrid grid = (TaskGrid)item;
                    Task task = grid.ParentTask;
                    tasksToRemove.Add(task);
                }

                //Delete the tasks from the TaskManager
                foreach (var t in tasksToRemove)
                {
                    TaskManager.RemoveTask(t);
                }
            }
        }



        /// <summary>
        /// Shows the delete confirmation and returns the result
        /// </summary>
        private bool ShowDeleteConfirmationDialog()
        {
            var w = new CustomMessageBox(messageText: "Are you sure you want to delete these tasks?", affirmativeText: "Yes", negativeText: "No");

            w.Left = Left + Mouse.GetPosition(this).X;
            w.Top = Top + Mouse.GetPosition(this).Y;

            w.ShowDialog();
            return w.Result;
        }

        /// <summary>
        /// Called when filter button is hit on main window
        /// \nShow the filter window, set new filter choices, display task list with updated filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            
            filterWindow.Left = Left + Mouse.GetPosition(this).X;
            filterWindow.Top = Top + Mouse.GetPosition(this).Y;
            
            //Subscribe to filter event

            filterWindow.Show();
            
        }

        

        private void Window_MouseEntered(object sender, RoutedEventArgs e)
        {
            if (filterWindow != null) { filterWindow.Hide(); }

        }

       

    }
}
