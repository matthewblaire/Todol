using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Todol.Models;
using Todol.Services;
using Todol.ViewModels;

namespace Todol
{
    /// <summary>
    /// Interaction logic for ViewTaskWindow.xaml
    /// </summary>
    public partial class ViewTaskWindow : Window
    {
        
        private ViewTaskWindowViewModel _viewModel;
        

        public ViewTaskWindow(Task task)
        {
            _viewModel = new ViewTaskWindowViewModel();
            _viewModel.Task = task;
            InitializeComponent();
            
            textBox_Title.Text = _viewModel.Task.Title;
            textBox_Description.Text = _viewModel.Task.Description;

            textBox_Description.Height = Double.NaN;
            
            //Only show the due date if the task has one
            if (_viewModel.Task.DueDate is not null)
            {
                labelDueDate.Content = ((DateTime)_viewModel.Task.DueDate).Date;
                datePicker.SelectedDate = _viewModel.Task.DueDate;
                labelDueDate.Visibility = Visibility.Visible;
            }
            else
            {
                labelDueDate.Content = "No Due Date";
            }

        }

        /// <summary>
        /// Called when description object is loaded by the window. Used because the object does not report correct ActualHeight until a little bit after the constructor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Description_Loaded(object sender, RoutedEventArgs e)
        {
            FitHeightToDescription();
        }

        /// <summary>
        /// Called when the Edit/save button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ToggleEditing();
            if (_viewModel.IsEditing)
            {
                // User is editing, so update objects accordingly
                ShowEditingState();
            }
            else
            {

                // User has finished editing. Show normal state
                ShowNormalState();

                
                _viewModel.Task.Description = textBox_Description.Text;
                if (datePicker.SelectedDate is not null)
                {
                    _viewModel.Task.DueDate = ((DateTime)datePicker.SelectedDate).Date;
                    labelDueDate.Content = _viewModel.Task.DueDate;
                    datePicker.SelectedDate = _viewModel.Task.DueDate;
                }
                else
                {
                    _viewModel.Task.DueDate = null;
                    labelDueDate.Content = "";
                }

                _viewModel.Task.Title = textBox_Title.Text;
            }

        }

        /// <summary>
        /// Changes the window size to accomodate the description
        /// </summary>
        private void FitHeightToDescription()
        {
            var newHeight = textBox_Description.ActualHeight + textBox_Description.Margin.Top + textBox_Description.Margin.Left + 45;

            Height = newHeight;
            MinHeight = newHeight;
            MaxHeight = newHeight;
        }

        /// <summary>
        /// Sets all objects to their editing state
        /// </summary>
        private void ShowEditingState()
        {
            textBox_Title.IsReadOnly = false; // let user change value
            textBox_Title.IsHitTestVisible = true; // let mouse affect textBox
            textBox_Title.BorderBrush = Brushes.Black; // set border to black to notify user they can edit

            textBox_Description.IsReadOnly = false;
            textBox_Description.IsHitTestVisible = true;
            textBox_Description.BorderBrush = Brushes.Black;


            if (_viewModel.Task.DueDate is not null)
                labelDueDate.Visibility = Visibility.Visible;
            else labelDueDate.Visibility = Visibility.Hidden;

            labelDueDate.Visibility = Visibility.Hidden;
            datePicker.Visibility = Visibility.Visible;
            btnEdit.Content = "Save";

            btnComplete.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Sets all objects to their normal state
        /// </summary>
        private void ShowNormalState()
        {
            textBox_Title.IsReadOnly = true; // dont let user change value
            textBox_Title.IsHitTestVisible = false; // dont let mouse affect textBox
            textBox_Title.BorderBrush = Brushes.AliceBlue; // set border to a color similar to background
            

            textBox_Description.IsReadOnly = true;
            textBox_Description.IsHitTestVisible = false;
            textBox_Description.BorderBrush = Brushes.AliceBlue;

            labelDueDate.Visibility = Visibility.Visible;
            datePicker.Visibility = Visibility.Hidden;
            btnEdit.Content = "Edit";

            btnComplete.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Called every time the description box changes size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Description_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (textBox_Description.IsLoaded)
            {
                FitHeightToDescription();
            }
        }

        /// <summary>
        /// Shows the delete confirmation and returns the result
        /// </summary>
        private bool ShowDeleteConfirmationDialog()
        {
            var w = new CustomMessageBox(messageText: "Are you sure you want to delete this task?", affirmativeText: "Yes", negativeText: "No");

            w.Left = Left+Mouse.GetPosition(this).X;
            w.Top = Top+Mouse.GetPosition(this).Y;

            w.ShowDialog();
            return w.Result;
        }



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ShowDeleteConfirmationDialog())
            {
                Close();
                TaskManager.RemoveTask(_viewModel.Task);
            }
        }

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Task.MarkComplete();
            Close();
        }
    }
}
