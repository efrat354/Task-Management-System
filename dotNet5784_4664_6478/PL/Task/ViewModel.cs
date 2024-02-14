using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PL.Task
{
    /// <summary>
    /// Represents a ViewModel class responsible for managing tasks and the current task for the task's window.
    /// </summary>
    public class ViewModel : DependencyObject
    {
        /// <summary>
        /// Gets or sets the collection of tasks.
        /// </summary>
        public ObservableCollection<BO.Task> Tasks { get; set; }
        /// <summary>
        /// Gets or sets the current selected task.
        /// </summary>
        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }
        /// <summary>
        /// Dependency property for the CurrentTask property, enabling data binding.
        /// </summary>
        public static readonly DependencyProperty CurrentTaskProperty =
                DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(ViewModel), new PropertyMetadata(null));
        /// <summary>
        /// Initializes a new instance of the ViewModel class.
        /// </summary>
        public ViewModel()
        {
            Tasks = new ObservableCollection<BO.Task>();
        }
    }
}
