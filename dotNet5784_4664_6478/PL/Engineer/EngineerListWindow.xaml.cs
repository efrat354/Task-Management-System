
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        /// <summary>
        /// Constructor initializing the window
        /// </summary>
        public EngineerListWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// The list of engineers displayed in the window
        /// </summary>
        public ObservableCollection<BO.Engineer> EngineerList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }
        /// <summary>
        /// The property of the engineer list, allowing updating in the GUI
        /// </summary>
        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;
        /// <summary>
        /// Function called when a value is selected in the engineer's experience comboBox
        /// </summary>
        private void ExperienceSelected(object sender, SelectionChangedEventArgs e)
        {
            var temp = Experience == BO.EngineerExperience.None ?
           s_bl?.Engineer.ReadAll() :
           s_bl?.Engineer.ReadAll(item => (BO.EngineerExperience)item!.Level == Experience);
            EngineerList = temp == null ? new() : new(temp!);

        }
        /// <summary>
        /// Function called when the add engineer button is clicked-open the single engineer page
        /// </summary>
        private void AddEngineer_Click(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
        }
        /// <summary>
        /// Function called when an engineer is double-clicked to update-open the single engineer page
        /// </summary>
        private void UpdateEngineer_DoubleClick(object sender, RoutedEventArgs e)
        {
            BO.Engineer? engineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
            new EngineerWindow(engineerInList!.Id).ShowDialog();
        }
        /// <summary>
        /// Function called when the user entire to the window-refresh the engineers details
        /// </summary>
        private void RefreshEngineerList_Activated(object sender, System.EventArgs e)
        {
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp == null ? new() : new(temp!);
            Experience = BO.EngineerExperience.None;
        }
    }
}
