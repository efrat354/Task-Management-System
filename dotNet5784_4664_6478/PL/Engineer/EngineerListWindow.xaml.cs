using System.Collections.Generic;
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

        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;

        private void ExperienceSelected(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (Experience == BO.EngineerExperience.None) ?
            s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => (BO.EngineerExperience)item.Level == Experience)!;
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
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }
    }
}
