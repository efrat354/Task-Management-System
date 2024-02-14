using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;


namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    int state = 0;
    /// <summary>
    /// Constructor initializing the Engineer Window
    /// </summary>
    /// <param name="id">The ID of the engineer (get a value only in update state)</param>
    public EngineerWindow(int id = 0)
    {
        InitializeComponent();
        if (id != 0)// Set state to update
        {
            state = 1;
            CurrentEngineer = new ObservableCollection<BO.Engineer> { s_bl.Engineer.Read(id) };
        }
        else// Set state to create
        {
            state = 0;
            CurrentEngineer = new ObservableCollection<BO.Engineer> { new BO.Engineer() { Id = 0, Name = "", Email = "", Task = new BO.TaskInEngineer() { Id = 0, Alias = "default" },Level = 0, Cost = 0} };
        }
    }
    /// <summary>
    /// The current engineer being displayed in the window
    /// </summary>
    public ObservableCollection<BO.Engineer> CurrentEngineer
    {
        get { return (ObservableCollection<BO.Engineer>)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }

    public static readonly DependencyProperty CurrentEngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerWindow), new PropertyMetadata(null));

    public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;
    /// <summary>
    /// Event handler for the Add/Update button click
    /// </summary>
    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        BO.Engineer engineer = CurrentEngineer[0];
        if (state == 0)// Create
        {
            try
            {
                s_bl.Engineer.Create(engineer);
                MessageBox.Show("Engineer successfully created");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        else// Update
        {
            try
            {
                s_bl.Engineer.Update(engineer);
                MessageBox.Show("Engineer successfully updated");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    /// <summary>
    /// Event handler for the Close button click-close the window
    /// </summary>
    private void close_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
