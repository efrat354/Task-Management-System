using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public TaskListWindow()
        {
            InitializeComponent();
        }
        public ObservableCollection<BO.Task> TaskList
        {
            get { return (ObservableCollection<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));

        public BO.Status Status { get; set; } = BO.Status.None;

        private void StatusSelected(object sender, SelectionChangedEventArgs e)
        {
            var temp = Status == BO.Status.None ?
           s_bl?.Task.ReadAll() :
           s_bl?.Task.ReadAll().Select(item=>item.Status== Status ? item:null);
            TaskList = temp == null ? new() : new(temp!);

        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
        }

        private void UpdateTask_DoubleClick(object sender, RoutedEventArgs e)
        {
            BO.Task? TaskInList = (sender as ListView)?.SelectedItem as BO.Task;
            new TaskWindow().ShowDialog();
        }

        private void RefreshTaskList_Activated(object sender, System.EventArgs e)
        {
            var temp = s_bl?.Task.ReadAll();
            TaskList = temp == null ? new() : new(temp!);
            Status = BO.Status.None;   //למה זה לא משנה את התצוגה של הקומבומוקס? 
        }
    }
}
