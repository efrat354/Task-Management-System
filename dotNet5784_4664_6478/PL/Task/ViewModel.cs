using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;



namespace PL.Task

{
    public class ViewModel : DependencyObject
    {

        public ObservableCollection<BO.Task> Tasks { get; set; }
        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        public static readonly DependencyProperty CurrentTaskProperty =
                DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(ViewModel), new PropertyMetadata(null));

        public ViewModel()
        {
            Tasks = new ObservableCollection<BO.Task>();
            
        }
    }
}
