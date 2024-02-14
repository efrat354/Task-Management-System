using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Windows.Controls;


namespace PL.Task;

/// <summary>
/// Interaction logic for TaskWindow.xaml
/// </summary>
/// 
public partial class TaskWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    int state = 0;
    
    //public BO.Task CurrentTask
    //{
    //    get { return (BO.Task)GetValue(CurrentTaskProperty); }
    //    set { SetValue(CurrentTaskProperty, value); }
    //}

    //public static readonly DependencyProperty CurrentTaskProperty =
    //    DependencyProperty.Register("CurrentTask", typeof(BO.Task),
    //        typeof(ViewModel), new PropertyMetadata(null));


    public ViewModel ViewModelInstance { get; set; }
        List<BO.TaskInList>dependencies= new List<BO.TaskInList>();
    public TaskWindow(int id = 0)
    {
   
        InitializeComponent();
        BO.Task task;
        ViewModelInstance = new ViewModel();
        DataContext = ViewModelInstance;
        ViewModelInstance.Tasks = new ObservableCollection<BO.Task>(s_bl?.Task.ReadAll());


        if (id != 0)
        {
            task = s_bl.Task.Read(id);
            state = 1;
            ViewModelInstance.CurrentTask = task;
        }
        else
        {
            state = 0;
             ViewModelInstance.CurrentTask = new BO.Task()
             { Id = 0, Alias = "", Description = "", CreatedAtDate = DateTime.Today, Engineer = new BO.EngineerInTask() { Id = 0, Name = " " } } ;

            // ViewModelInstance.CurrentTask = new ObservableCollection<BO.Task> { new BO.Task() { Id = 0, Alias = "", Description = "", CreatedAtDate = DateTime.Today, Engineer = new BO.EngineerInTask() { Id = 0, Name = " " } } };
        }

    }
   

    public BO.Status Status { get; set; } = BO.Status.None;

    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        BO.Task Task = ViewModelInstance.CurrentTask;    
        if (state == 0)
        {
            try
            {

                s_bl.Task.Create(Task);
                MessageBox.Show("Task successfully created");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        else
        {
            try
            {
                BO.Task task = Task;
                task.Dependencies = dependencies;
                s_bl.Task.Update(task);
                MessageBox.Show("Task successfully updated");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


    private void MouseDoubleClick_dependency(object sender, RoutedEventArgs e)
    {
        BO.Task? dep= (sender as ListView)?.SelectedItem as BO.Task;
        BO.TaskInList taskInListDep=new BO.TaskInList() { Id=dep.Id,Alias=dep.Alias,Description=dep.Description,Status=dep.Status};
        dependencies.Add(taskInListDep); 
    }
}
