# Todol
Portable, Minimalistic task management/productivity app written in C# using WPF. 

## Installation
No install required, just download and extract [todol.zip](https://github.com/matthewblaire/Todol/files/12188637/todol.zip) to a folder and run Todol.exe


![MainWindow](https://github.com/matthewblaire/Todol/assets/49771927/69702f5b-599b-49e9-a90f-7faf21cbd4c2)

## New Task Window
Here you can input the task title, a more detailed description of the task, set when you will start, and you can decide whether or not this task has a due date. 

This window is reached by clicking the New Task button on the main window. *Opening this window disables functionality on the main window until task creation is completed or canceled.*

![NewTaskWindow](https://github.com/matthewblaire/Todol/assets/49771927/08bd0550-6913-4139-ad20-ba2038252718)
![NewTaskWindow_FilledOut](https://github.com/matthewblaire/Todol/assets/49771927/66ab566b-f351-44f3-9fd2-ce919b9d7f11)

After you create the task, it will be added to the task list automatically. *If you set a start date in the future and have the* "Today Only" *filter active, the task will not be shown in the list, but it will be added to memory.*

![MainWindow_TaskAdded](https://github.com/matthewblaire/Todol/assets/49771927/ace29d7c-87b4-4ecc-9e95-2b0998487093)

## View Task Window
This window displays task data in a small window fitted to the size of the description. User can mark the task as complete, and edit the title, description, and due date if desired. Window will persist after main window is closed, allowing user to clear clutter and focus on one or more task with their description readily available.

This window is opened by double clicking on a task in the task list.

![ViewTaskWindow](https://github.com/matthewblaire/Todol/assets/49771927/3ff03f8a-bac9-413b-9d4b-1285185ee086)
![MainWindow_TaskCompleted](https://github.com/matthewblaire/Todol/assets/49771927/d76c1504-9143-45df-be36-cb63349763fa)

## Filter Modes
By pressing the filter button, you can access various sorting and filtering modes
* Today Only: When chekced, task list will only display tasks with a start date of today or before (date can be changed to the left of the filter button
* Show Completed: When checked, task list will display completed tasks. When unchecked, completed tasks will be hidden.
* Ealiest Due Date: Task list will be sorted by earliest due date. Tasks with no due date will be shown below.
* Earliest Start Date: Task list will be sorted by earliest start date.
* Most Recent: Tasks will be sorted with newest tasks at the top.
  
![FilterModes](https://github.com/matthewblaire/Todol/assets/49771927/70144d24-ee1a-4471-b5b2-582d22185c21)



