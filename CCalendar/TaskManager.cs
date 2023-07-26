using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Automation.Peers;

namespace Todol
{
    /// <summary>
    /// This class is responsible for saving and loading task data.
    /// </summary>
    public static partial class TaskManager
    {

        private static bool IsFirstLoad = true;

        private static readonly string filePath = "tasks.json"; // Choose a suitable file path
        private static List<Task> _tasks = new List<Task>();
        public static event EventHandler? TaskListSizeChanged;
        public static List<Task> GetTaskList() { return _tasks; }
        public static void AddTask(Task task)
        {
            if (task is not null)
            {
                _tasks.Add(task);
                OnTaskListSizeChanged();
            }
        }

        public static void RemoveTask(Task task)
        {
            if (task is not null)
            {
                _tasks.Remove(task);
                OnTaskListSizeChanged();
            }
        }

        private static void OnTaskListSizeChanged()
        {
            TaskListSizeChanged?.Invoke(null, EventArgs.Empty);
        }

        private static void OnTaskListSizeChanged(Task task)
        {
            var eventArgs = new TaskListUpdateEventArgs(true,task);
            TaskListSizeChanged?.Invoke(null, eventArgs);
        }

        public static void SaveTasks()
        {
            string json = JsonConvert.SerializeObject(_tasks);
            File.WriteAllText(filePath, json);
        }

        public static void LoadTasks()
        {
            
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                _tasks= JsonConvert.DeserializeObject<List<Task>>(json);
                OnTaskListSizeChanged();
            }
            else
            {
                if (!IsFirstLoad)
                {
                    MessageBox.Show(filePath + " not found. Try creating and exporting some tasks.\n(Tasks will export automatically when main window is closed)", "Error", MessageBoxButton.OK);
                }
                
                //throw new FileNotFoundException(filePath + " not found");
            }

            if (IsFirstLoad)
            {
                IsFirstLoad = false;
            }

        }

        public static List<Task> GetTaskList(TaskListFilter filter)
        {
            var workingTaskList = new List<Task>(_tasks);

            if (filter.EarliestDueDate)
            {
                SortByEarliestDueDate(workingTaskList);
            } else if (filter.EarliestStartDate)
            {
                SortByEarliestStartDate(workingTaskList);
            } else if (filter.EarliestCreationDate)
            {
                SortByEarliestCreationDate(workingTaskList);
            }

            if (filter.ShowCompleted == false)
            {
               workingTaskList = RemoveCompleted(workingTaskList);
            }

            if (filter.OnlyShowTasksForToday)
            {
                removeFutureStartDateTasks(workingTaskList);
            }

            return workingTaskList;
        }

        private static void SortByEarliestDueDate(List<Task> taskList)
        {
           for (int i = 0; i < taskList.Count; i++)
            {
                int earliestDueDateIndex = i;
                Task earliestDueDate = taskList[earliestDueDateIndex];
                for (int j = i; j < taskList.Count; j++)
                {
                    if (taskList[j].DueDate is null || taskList[j].DueDate > taskList[earliestDueDateIndex].DueDate)
                    {
                        earliestDueDateIndex = j;
                        earliestDueDate = taskList[j];
                    }                    
                }
                
                taskList.RemoveAt(earliestDueDateIndex);
                taskList.Insert(i, earliestDueDate);

                
            }
            

        }

        private static void SortByEarliestStartDate(List<Task> taskList)
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                int earliestStartDateIndex = i;
                Task earliestStartDate = taskList[earliestStartDateIndex];
                for (int j = i; j < taskList.Count; j++)
                {
                    if (taskList[j].StartDate > taskList[earliestStartDateIndex].StartDate)
                    {
                        earliestStartDateIndex = j;
                        earliestStartDate = taskList[j];
                    }
                }

                taskList.RemoveAt(earliestStartDateIndex);
                taskList.Insert(i, earliestStartDate);


            }
            
        }

        private static void SortByEarliestCreationDate(List<Task> taskList)
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                int earliestCreationDateIndex = i;
                Task earliestCreationDate = taskList[earliestCreationDateIndex];
                for (int j = i; j < taskList.Count; j++)
                {
                    if (taskList[j].DateCreated > taskList[earliestCreationDateIndex].DateCreated)
                    {
                        earliestCreationDateIndex = j;
                        earliestCreationDate = taskList[j];
                    }
                }

                taskList.RemoveAt(earliestCreationDateIndex);
                taskList.Insert(i, earliestCreationDate);

            }
            
        }

        private static List<Task> RemoveCompleted(List<Task> taskList)
        {
            var workingList = new List<Task>();
            foreach (var task in taskList)
            {
                if (!task.IsComplete)
                {
                    workingList.Add(task);
                }
            }

            return workingList;  
            
        }

        private static void removeFutureStartDateTasks(List<Task> taskList)
        {
            for (int i = 0; i< taskList.Count; i++)
            {
                if (taskList[i].StartDate > DateTime.Now.Date)
                {
                    taskList.RemoveAt(i);
                }
            }
           
        }

    }

}
