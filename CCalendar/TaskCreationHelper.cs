namespace Todol
{
    /// <summary>
    /// Helper for NewTaskWindow. Keeps track of whether the user is ready to save the task or not.
    /// </summary>
    public class NewTaskWindow_TaskCreationHelper
    {
        private bool _hasChangedTitle;
        private bool _hasChangedDescription;
        private bool _hasSelectedStartDate;
        private bool _hasSelectedDueDate;
        private bool _requiresDueDate;

        public bool TaskRequiresDueDate { private get { return _requiresDueDate; } set { _requiresDueDate = value; } }

        public bool UserHasChangedTitle { set { _hasChangedTitle = value; } }
        public bool UserHasChangedDescription { set { _hasChangedDescription = value; } }
        public bool UserHasSelectedStartDate { set { _hasSelectedStartDate = value; } }
        public bool UserHasSelectedDueDate { set { _hasSelectedDueDate = value; } }

        public NewTaskWindow_TaskCreationHelper()
        {
            _hasChangedTitle = false;
            _hasChangedDescription = false;
            _hasSelectedStartDate = true;
            _hasSelectedDueDate = true;
            _requiresDueDate = false;
        }

        public bool IsReadyToSave
        {
            get
            {
                bool ready = false;
                if (TaskRequiresDueDate)
                    ready = _hasChangedTitle && _hasChangedDescription && _hasSelectedStartDate && _hasSelectedDueDate;
                else
                    ready = _hasChangedTitle && _hasChangedDescription && _hasSelectedStartDate;

                return ready;
            }
        }


    }
}
