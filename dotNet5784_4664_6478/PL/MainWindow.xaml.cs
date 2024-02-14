using PL.Engineer;
using PL.Task;
using System.Windows;


namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Constructor initializing the main window
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }
    /// <summary>
    /// Event handler for handling the click event of the "Engineers" button
    /// </summary>
    private void HandleEngineers_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }
    /// <summary>
    /// Event handler for handling the click event of the "Initialization Data" button
    /// </summary>
    private void IntializationData_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Do yow want to Intializate Data?", "Intialization Data", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            DalTest.Initialization.Do();
        }
    }
    /// <summary>
    /// Event handler for handling the click event of the "Tasks" button
    /// </summary>
    private void HandleTasks_Click(object sender, RoutedEventArgs e)
    {
        new TaskListWindow().Show();
    }
}
