using System;

namespace Todol
{

    /// <summary>
    /// Hold data for each Task on the calendar
    /// </summary>
    public class Task
    {
        public static event EventHandler? TaskUpdated;

        private string _title;
        private string _description;

        private DateTime? _dueDate;
        private DateTime _dateCreated;
        private DateTime _startDate;

        private bool _isComplete;

        /// <summary>
        /// Short name of the task
        /// </summary>
        public string Title { get { return _title; } set { _title = value; OnTaskUpdated(); } }
        /// <summary>
        /// Description of the task
        /// </summary>
        public string Description { get { return _description; } set { _description = value; } }
        /// <summary>
        /// Date the task was created
        /// </summary>
        public DateTime DateCreated { get { return _dateCreated; } }
        /// <summary>
        /// The day that work on the task is to be started
        /// </summary>
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (value >= DateTime.Now.Date)
                {
                    _startDate = value;
                }
                else
                {
                    _startDate = DateTime.Now.Date;
                }

            }
        }
        /// <summary>
        /// Date the task is to be completed
        /// </summary>
        public DateTime? DueDate
        {
            get { return _dueDate; }
            set
            {
                if (value is null || value >= DateTime.Now)
                {
                    _dueDate = value;
                }
                else
                {
                    _dueDate = DateTime.Now.Date.AddDays(1);
                }

            }
        }


        public bool IsComplete { get { return _isComplete; } set { _isComplete = value; } }

        public Task()
        {
            _title = "Task title";
            _description = "Description";
            _dueDate = null;
            _dateCreated = DateTime.Now.Date;
            _startDate = DateTime.Now.Date;
            _dueDate = null;
            _isComplete = false;
        }

        /// <summary>
        /// Create task without due date
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="startDate"></param>
        public Task(string title, string description, DateTime startDate)
            : this()
        {
            _title = title;
            _description = description;
            StartDate = startDate;

        }

        /// <summary>
        /// create Task with due date
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="startDate"></param>
        /// <param name="dueDate"></param>
        public Task(string title, string description, DateTime startDate, DateTime dueDate)
            : this(title, description, startDate)
        {
            DueDate = dueDate;
        }

        /// <summary>
        /// Time between now and due date
        /// </summary>
        public TimeSpan TimeRemaining
        {
            get
            {
                if (DueDate is DateTime dueDate)
                {
                    return dueDate - DateTime.Now;
                }
                else
                {
                    return TimeSpan.Zero;
                }
            }
        }

        /// <summary>
        /// Overrides ToString(), returning the Title property
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string dueDateString = "";
            if (DueDate is not null)
            {
                dueDateString = "Due: " + ((DateTime)DueDate).ToString();
            }
            return Title + dueDateString;
        }



        public void MarkComplete()
        {
            _isComplete = true;
            DueDate = null;

            OnTaskUpdated();
        }

        /// <summary>
        /// Invokes TaskUpdated method whenever the Title property is changed
        /// </summary>
        private static void OnTaskUpdated()
        {
            TaskUpdated?.Invoke(null, EventArgs.Empty);
        }

        public bool Equals(Task other)
        {
            bool isEqual = true;
            if(other != null)
            {
                if (DueDate != other.DueDate) { 
                    isEqual = false;
                } else if (StartDate != other.StartDate)
                {
                    isEqual = false;
                } else if (DateCreated != other.DateCreated)
                {
                    isEqual = false;
                } else if (Title.Equals(other.Title) == false)
                {
                    isEqual = false;
                } else if (Description.Equals(other.Description) == false)
                {
                    isEqual = false;
                } else if (IsComplete != other.IsComplete)
                {
                    isEqual = false;
                }
                
            }
            return isEqual;
        }

    }

}
