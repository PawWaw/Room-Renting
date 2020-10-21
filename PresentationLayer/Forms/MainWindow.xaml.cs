using System.Windows;
using System.Windows.Controls;
using BizzLayer;
using MaterialDesignThemes.Wpf;
using Room_Renting.Forms;

namespace Room_Renting
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    //usc = new UserControlHome();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCreate":
                    //usc = new UserControlCreate();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dąbrowa Górnicza, 2020\n Paweł Wawszczyk", "About", MessageBoxButton.OK);
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
        }

        private void ItemCalendar_Selected(object sender, RoutedEventArgs e)
        {
            Forms.Calendar calendar = new Forms.Calendar();
            calendar.ShowDialog();
        }

        private void ItemCreate_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ItemHome_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void ItemHistory_Selected(object sender, RoutedEventArgs e)
        {
            History history = new History();
            history.ShowDialog();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginService.userId = 0;
        }
    }
}
