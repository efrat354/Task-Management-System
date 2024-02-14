
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
        public EngineerListWindow()
        {
            InitializeComponent();         
        }

        public ObservableCollection<BO.Engineer> EngineerList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;

        private void ExperienceSelected(object sender, SelectionChangedEventArgs e)
        {
            var temp = Experience == BO.EngineerExperience.None ?
           s_bl?.Engineer.ReadAll() :
           s_bl?.Engineer.ReadAll(item => (BO.EngineerExperience)item!.Level == Experience);
            EngineerList = temp == null ? new() : new(temp!);

        }

        private void AddEngineer_Click(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
        }

        private void UpdateEngineer_DoubleClick(object sender, RoutedEventArgs e)
        {
            BO.Engineer? engineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
            new EngineerWindow(engineerInList!.Id).ShowDialog();
        }

        private void RefreshEngineerList_Activated(object sender, System.EventArgs e)
        {
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp == null ? new() : new(temp!);
            Experience = BO.EngineerExperience.None;   //למה זה לא משנה את התצוגה של הקומבומוקס? 
        }
    }
}
