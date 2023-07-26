using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Todol.Controls;
using Todol.Models;
using Todol.Services;
using Todol.ViewModels;
namespace Todol
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly MainWindowViewModel _viewModel;
        

        private DateTime? _selectedDate;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();

            TaskManager.TaskListSizeChanged += UpdateListBox;
            Task.TaskUpdated += UpdateListBox;
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
            _viewModel.PopulateListBox(listBox);
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
            var loadingMessage = new CustomMessageBox("Loading...");
            var left = Left - (loadingMessage.Width / 2) + (this.ActualWidth / 2);
            var top = Top - (loadingMessage.Height / 2) + (this.ActualHeight / 2);
            loadingMessage.Left = left;
            loadingMessage.Top = top;
            loadingMessage.Show();
            TaskManager.LoadTasks();
            UpdateListBox(sender, e);
            loadingMessage.Close();
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
            _viewModel.CloseFilterWindow();
        }

        private void ContextMenu_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ShowDeleteConfirmationDialog())
            {
                _viewModel.DeleteTasks(listBox.SelectedItems);
            }
        }



        /// <summary>
        /// Shows the delete confirmation and returns the result
        /// </summary>
        private bool ShowDeleteConfirmationDialog()
        {
            var w = new CustomMessageBox(messageText: "Are you sure you want to delete these tasks?", affirmativeText: "Yes", negativeText: "No");

            var left = Left - (w.Width/2) + (this.ActualWidth/2);
            var top = Top - (w.Height/2) + (this.ActualHeight/2);
            w.Left = left;
            w.Top = top;

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
            
            var left = Left + Mouse.GetPosition(this).X;
            var top = Top + Mouse.GetPosition(this).Y;
            
            _viewModel.OpenFilterWindow(left, top);
            
        }

        

        private void Window_MouseEntered(object sender, RoutedEventArgs e)
        {
            if (_viewModel.FilterIsShowing) 
            { 
                _viewModel.HideFilterWindow(); 
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (listBox.SelectedItems.Count > 0 && ShowDeleteConfirmationDialog())
                {
                    _viewModel.DeleteTasks(listBox.SelectedItems);
                }
            } 
        }
    }
}
