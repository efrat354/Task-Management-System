using PL.Engineer;
using PL.Task;
using System.Windows;


namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void HandleEngineers_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }

    private void IntializationData_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Do yow want to Intializate Data?", "Intialization Data", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            DalTest.Initialization.Do();
        }
    }

    private void HandleTasks_Click(object sender, RoutedEventArgs e)
    {
        new TaskListWindow().Show();
    }
}
