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
    public ViewModel ViewModelInstance { get; set; }
        List<BO.TaskInList>dependencies= new List<BO.TaskInList>();
    /// <summary>
    /// Initializes a new instance of the TaskWindow class.
    /// </summary>
    /// <param name="id">The ID of the task to be displayed- get only when it is update state.</param>
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

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public BO.Status Status { get; set; } = BO.Status.None;

    /// <summary>
    /// Event handler for the click event of the "Add/Update" button.
    /// </summary>
    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        BO.Task Task = ViewModelInstance.CurrentTask;    
        if (state == 0)
        {
            try
            {
                BO.Task task = Task;
                task.Dependencies = dependencies;
                s_bl.Task.Create(task);
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
    /// <summary>
    /// Event handler for the double-click event on a dependency in the list-choosing dependencies.
    /// </summary>
    private void MouseDoubleClick_dependency(object sender, RoutedEventArgs e)
    {
        BO.Task? dep= (sender as ListView)?.SelectedItem as BO.Task;
        BO.TaskInList taskInListDep=new BO.TaskInList() { Id=dep.Id,Alias=dep.Alias,Description=dep.Description,Status=dep.Status};
        dependencies.Add(taskInListDep); 
    }
}
