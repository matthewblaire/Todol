using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todol.Helpers;
using Todol.Models;
namespace Todol.ViewModels
{
    public class ViewTaskWindowViewModel
    {
        /// <summary>
        /// Keeps track of whether of not user is editing
        /// </summary>
        private bool _isEditing = false;
        /// <summary>
        /// The task that this window is attempting to show
        /// </summary>
        private Task _task;
        /// <summary>
        /// Toggles the _isEditing flag
        /// </summary>
        
        public Task Task { get { return _task; } set {  _task = value; } }
        public bool IsEditing { get { return _isEditing;} }

        public ViewTaskWindowViewModel() {
            _isEditing = false;
        }
        public void ToggleEditing()
        {
            _isEditing = !_isEditing;
        }

        


    }
}
