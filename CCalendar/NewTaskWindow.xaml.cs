using System;
using System.Windows;
using System.Windows.Controls;

namespace Todol
{

    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        /// <summary>
        /// default string for task name text box
        /// </summary>
        private readonly string DEFAULT_TEXT_TASK_NAME = "Enter task name...";
        /// <summary>
        /// default string for task description text box
        /// </summary>
        private readonly string DEFAULT_TEXT_TASK_DESCRIPTION = "Add a little more detail...";
        /// <summary>
        /// Reference to the TaskCreationHelper
        /// </summary>
        private NewTaskWindow_TaskCreationHelper _helper;
        public NewTaskWindow()
        {

            InitializeComponent();

            //Initialize fields 
            datePicker_TaskStartDate.SelectedDate = DateTime.Now.Date;
            datePicker_TaskDueDate.SelectedDate = DateTime.Now.Date.AddDays(1);
            _helper = new NewTaskWindow_TaskCreationHelper();



        }

        /// <summary>
        /// Makes sure the date coming in isn't null
        /// </summary>
        /// <param name="nullableDate"></param>
        /// <returns></returns>
        private DateTime getDate(DateTime? nullableDate)
        {
            if (nullableDate is DateTime selectedDate)
            {
                return selectedDate.Date;
            }
            else
            {
                return DateTime.Now.Date;
            }
        }

        /// <summary>
        /// Creates Task object and sends it to the DataManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            string title = textBox_TaskName.Text;
            string description = textBox_TaskDescription.Text;
            DateTime startDate = getDate(datePicker_TaskStartDate.SelectedDate);

            Task t;

            if (checkBox.IsChecked == true)
            {
                DateTime dueDate = getDate(datePicker_TaskDueDate.SelectedDate);
                t = new Task(title, description, startDate, dueDate);
            }
            else
            {
                t = new Task(title, description, startDate);
            }

            TaskManager.AddTask(t);

            Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// When the "Has Due Date" checkbox is checked, reset the size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            int newHeight = 283;
            int newWidth = 300;
            SetWindowSize(newHeight, newWidth);

            _helper.TaskRequiresDueDate = true;
            btnSaveTask.IsEnabled = _helper.IsReadyToSave;

            labelTaskDueDate.Visibility = Visibility.Visible;
            datePicker_TaskDueDate.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// When the "Has Due Date" checkbox is unchecked, reset the size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            int newHeight = 257;
            int newWidth = 300;
            SetWindowSize(newHeight, newWidth);
            _helper.TaskRequiresDueDate = false;
            btnSaveTask.IsEnabled = _helper.IsReadyToSave;
            labelTaskDueDate.Visibility = Visibility.Hidden;
            datePicker_TaskDueDate.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Sets the window size to newHeight and newWidth. Also sets min/max height/width to ensure window is not scalable.
        /// </summary>
        /// <param name="newHeight"></param>
        /// <param name="newWidth"></param>
        private void SetWindowSize(int newHeight, int newWidth)
        {
            MinHeight = newHeight;
            MaxHeight = newHeight;
            MinWidth = newWidth;
            MaxWidth = newWidth;
            Height = newHeight;
            Width = newWidth;
        }

        /// <summary>
        /// When the user changes the task name, flag it as changed. If the updated text is empty or default, do not flag as changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_TaskName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_helper is not null && btnSaveTask is not null)
            {
                var text = textBox_TaskName.Text;

                _helper.UserHasChangedTitle = !text.Equals(DEFAULT_TEXT_TASK_NAME) && !text.Equals(string.Empty);
                btnSaveTask.IsEnabled = _helper.IsReadyToSave;
            }

        }
        /// <summary>
        /// When the user changes the task description, flag it as changed. If the updated text is empty or default, do not flag as changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_TaskDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_helper is not null && btnSaveTask is not null)
            {
                var text = textBox_TaskDescription.Text;

                _helper.UserHasChangedDescription = !text.Equals(DEFAULT_TEXT_TASK_DESCRIPTION) && !text.Equals(string.Empty);

                btnSaveTask.IsEnabled = _helper.IsReadyToSave;
            }
        }

        private void datePicker_TaskStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            if (_helper is not null && btnSaveTask is not null)
            {
                _helper.UserHasSelectedStartDate = true;
                btnSaveTask.IsEnabled = _helper.IsReadyToSave;
            }
        }

        private void datePicker_TaskDueDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            if (_helper is not null && btnSaveTask is not null)
            {
                _helper.UserHasSelectedDueDate = true;
                btnSaveTask.IsEnabled = _helper.IsReadyToSave;
            }
        }

        private void textBox_TaskDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox_TaskDescription.Text = string.Empty;
        }

        private void textBox_TaskName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBox_TaskName.Text.Equals("Enter task name..."))
            {
                textBox_TaskName.Text = string.Empty;
            }

        }

        private void textBox_TaskDescription_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void textBox_TaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox_TaskName.Text == string.Empty)
            {
                _helper.UserHasChangedTitle = false;
                textBox_TaskName.Text = "Enter task name...";
            }
        }




        Point startPoint;
        bool isDragging = false;
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                isDragging = true;
                startPoint = e.GetPosition(this);

            }
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isDragging && e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Point currentPoint = e.GetPosition(this);
                double deltaX = currentPoint.X - startPoint.X;
                double deltaY = currentPoint.Y - startPoint.Y; 
                this.Left += deltaX;
                this.Top += deltaY;

            }
        }

        private void Window_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ( e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                isDragging = false;
                //startPoint = e.GetPosition(this);  
            }
           
        }
    }
}
