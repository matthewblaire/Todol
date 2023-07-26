namespace Todol.Models
{
    public class TaskListFilter
    {
        private bool _earliestDueDate;
        private bool _earliestStartDate;
        public bool ShowCompleted { get; set; }
        public bool OnlyShowTasksForToday { get; set; }

        public bool EarliestCreationDate { get; set; }

        public TaskListFilter()
        {
            EarliestDueDate = false;
            EarliestStartDate = false;
            ShowCompleted = false;
            OnlyShowTasksForToday = false;
            EarliestCreationDate = false;
        }

        /// <summary>
        /// Property for the Earliest Due Date SORT filter. Cannot be true when other SORT filters are true
        /// </summary>
        public bool EarliestDueDate
        {
            get { return _earliestDueDate; }
            set
            {
                if (value == true && EarliestStartDate == true)
                {
                    _earliestDueDate = true;
                    EarliestStartDate = false;
                }
                else
                {
                    _earliestDueDate = value;
                }
            }
        }

        /// <summary>
        /// Property for the Earliest Start Date SORT filter. Cannot be true when other SORT filters are true
        /// </summary>
        public bool EarliestStartDate
        {
            get { return _earliestStartDate; }
            set
            {
                if (value == true && EarliestDueDate == true)
                {
                    _earliestStartDate = true;
                    EarliestDueDate = false;
                }
                else
                {
                    _earliestStartDate = value;
                }
            }
        }



        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="filter"></param>
        public TaskListFilter(TaskListFilter filter)
        {
            EarliestDueDate = filter.EarliestDueDate;
            EarliestStartDate = filter.EarliestStartDate;
            ShowCompleted = filter.ShowCompleted;
            OnlyShowTasksForToday = filter.OnlyShowTasksForToday;
            EarliestCreationDate = filter.EarliestCreationDate;
        }


    }

}
