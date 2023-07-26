using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Todol
{
    /// <summary>
    /// Interaction logic for FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        private readonly string filepath = "filters.json";

        TaskManager.TaskListFilter _taskListFilter;
        
        public TaskManager.TaskListFilter Filter 
        { 
            get 
            { 
                return _taskListFilter; 
            }
        }

        public static event EventHandler? FilterUpdated;
        public FilterWindow()
        {
            InitializeComponent();
            LoadSettings();
            MatchCheckBoxesToSettings();
            
        }

        private void MatchCheckBoxesToSettings()
        {
            checkBox_CreationDate.IsChecked = _taskListFilter.EarliestCreationDate;
            checkBox_DueDate.IsChecked = _taskListFilter.EarliestDueDate;
            checkBox_StartDate.IsChecked = _taskListFilter.EarliestStartDate;
            checkBox_ShowCompleted.IsChecked = _taskListFilter.ShowCompleted;
            checkBox_TodayOnly.IsChecked = _taskListFilter.OnlyShowTasksForToday;
        }

        /// <summary>
        /// Tries to load filter settings from file, if no file or error, set filter settings to default
        /// </summary>
        private void LoadSettings()
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);
                try
                {
                    _taskListFilter = JsonConvert.DeserializeObject<TaskManager.TaskListFilter>(json);
                    return;
                } catch
                {
                    MessageBox.Show("Error loading filters.json");
                }
                
               
            }
            
            _taskListFilter = new TaskManager.TaskListFilter();
            
        }

        private void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(Filter);
            File.WriteAllText(filepath, json);
        }

        public static void OnFilterUpdated()
        {
            FilterUpdated?.Invoke(null, EventArgs.Empty);
        }

        private void checkBox_CreationDate_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBox_StartDate.IsChecked)
            {
                checkBox_StartDate.IsChecked = false;
            }
            if ((bool)checkBox_DueDate.IsChecked)
            {
                checkBox_DueDate.IsChecked = false;
            }
            _taskListFilter.EarliestCreationDate = true;
            OnFilterUpdated();
        }
       
        private void checkBox_StartDate_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBox_CreationDate.IsChecked)
            {
                checkBox_CreationDate.IsChecked = false;
            }
            if ((bool)checkBox_DueDate.IsChecked)
            {
                checkBox_DueDate.IsChecked = false;
            }
            _taskListFilter.EarliestStartDate = true;
            OnFilterUpdated();
        }
        private void checkBox_DueDate_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBox_CreationDate.IsChecked)
            {
                checkBox_CreationDate.IsChecked = false;
            }
            if ((bool)checkBox_StartDate.IsChecked)
            {
                checkBox_StartDate.IsChecked = false;
            }
            _taskListFilter.EarliestDueDate = true;
            OnFilterUpdated();
        }

        private void checkBox_StartDate_Unchecked(object sender, RoutedEventArgs e)
        {
            _taskListFilter.EarliestStartDate= false;
            OnFilterUpdated();
        }

        
        private void checkBox_DueDate_Unchecked(object sender, RoutedEventArgs e)
        {
            _taskListFilter.EarliestDueDate = false;
            OnFilterUpdated();
        }

        private void checkBox_CreationDate_Unchecked(object sender, RoutedEventArgs e)
        {
            _taskListFilter.EarliestCreationDate = false;
            OnFilterUpdated();
        }

        private void checkBox_ShowCompleted_Checked(object sender, RoutedEventArgs e)
        {
            _taskListFilter.ShowCompleted = true;
            OnFilterUpdated();
        }

        private void checkBox_TodayOnly_Checked(object sender, RoutedEventArgs e)
        {
            _taskListFilter.OnlyShowTasksForToday = true;
            OnFilterUpdated();
        }

        private void checkBox_ShowCompleted_Unchecked(object sender, RoutedEventArgs e)
        {
            _taskListFilter.ShowCompleted = false;
            OnFilterUpdated();
        }

        private void checkBox_TodayOnly_Unchecked(object sender, RoutedEventArgs e)
        {
            _taskListFilter.OnlyShowTasksForToday = false;
            OnFilterUpdated();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
