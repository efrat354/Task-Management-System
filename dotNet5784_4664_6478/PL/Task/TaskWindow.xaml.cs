using PL.Task;
using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace PL.Task;

/// <summary>
/// Interaction logic for TaskWindow.xaml
/// </summary>
public partial class TaskWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    int state = 0;
    public TaskWindow(int id = 0)
    {
        InitializeComponent();
        if (id != 0)
        {
            state = 1;
            CurrentTask = new ObservableCollection<BO.Task> { s_bl.Task.Read(id) };
        }
        else
        {
            state = 0;
            CurrentTask = new ObservableCollection<BO.Task> { new BO.Task() { Id = 0, Alias = "", Description = "",CreatedAtDate=DateTime.Today} };
        }
    }

    public ObservableCollection<BO.Task> CurrentTask
    {
        get { return (ObservableCollection<BO.Task>)GetValue(CurrentTaskProperty); }
        set { SetValue(CurrentTaskProperty, value); }
    }

    public static readonly DependencyProperty CurrentTaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(ObservableCollection<BO.Task>), typeof(TaskWindow), new PropertyMetadata(null));

    public BO.Status Status { get; set; } = BO.Status.None;

    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        BO.Task Task = CurrentTask[0];
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
                s_bl.Task.Update(Task);
                MessageBox.Show("Task successfully updated");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
    }
}
