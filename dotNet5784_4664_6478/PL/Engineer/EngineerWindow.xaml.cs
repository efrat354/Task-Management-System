using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        
        public EngineerWindow(int id = 0)
        {
            InitializeComponent();
            if (id != 0)
            {
               CurrentEngineer = new ObservableCollection<BO.Engineer> { s_bl.Engineer.Read(id) };
            }
            else
            {
                CurrentEngineer = new ObservableCollection<BO.Engineer> { new BO.Engineer() { Id = 0, Name = "", Email = "", Level = 0, Cost = 0 } };
            }
        }

        public ObservableCollection<BO.Engineer> CurrentEngineer
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerWindow), new PropertyMetadata(null));

        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            BO.Engineer engineer = CurrentEngineer[0];
            if (engineer.Id != 0)
            {
                try
                {
                    s_bl.Engineer.Create(engineer);
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
                    s_bl.Engineer.Update(engineer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
