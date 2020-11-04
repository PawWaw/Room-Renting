using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using BizzLayer;
using BizzLayer.Windows;
using DataLayer.Classes;
using MaterialDesignThemes.Wpf;
using Room_Renting.Forms;
using Room_Renting.WS;

namespace Room_Renting
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FlatsService flatsService = new FlatsService();
        private List<WSAddresses> rents = new List<WSAddresses>();

        public MainWindow()
        {
            InitializeComponent();
            if (LoginService.userId != 0)
            {
                RentButton.IsEnabled = true;
            }
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
            Create create = new Create();
            create.ShowDialog();
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
            LoginService.user = "";
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            DataGridBox.ClearValue(ItemsControl.ItemsSourceProperty);
            DataGridBox.Items.Clear();
            FlatSearchCriteria criteria = new FlatSearchCriteria();
            criteria.startDate = StartDateDP.SelectedDate.GetValueOrDefault();
            criteria.endDate = EndDateDP.SelectedDate.GetValueOrDefault();
            if (!BedCountTB.Text.Equals(""))
            {
                int parsed;
                if (int.TryParse(BedCountTB.Text, out parsed))
                {
                    criteria.bedCount = parsed;
                }
            }
            if (!PriceTB.Text.Equals(""))
            {
                int parsed;
                if (int.TryParse(PriceTB.Text, out parsed))
                {
                    criteria.price = parsed;
                    if (PriceCB.SelectedIndex == 0)
                    {
                        criteria.over = true;
                    }
                    else
                    {
                        criteria.over = false;
                    }
                }
            }
            criteria.city = CityTB.Text;
            criteria.animals = AnimalsCB.IsChecked.GetValueOrDefault();
            criteria.kitchen = KitchenCB.IsChecked.GetValueOrDefault();
            criteria.parking = ParkingCB.IsChecked.GetValueOrDefault();

            rents = flatsService.getAvailableRooms(criteria);

            LoadGrid(rents);
        }

        private void LoadGrid(List<WSAddresses> rents)
        {
            for (int i = 0; i < rents.Count; i++)
            {
                DataGridBox.Items.Add(rents[i]);
            }
        }

        private void ItemReviews_Selected(object sender, RoutedEventArgs e)
        {
            Reviews reviews = new Reviews();
            reviews.ShowDialog();
        }

        private void RentButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartDateDP.SelectedDate != null && EndDateDP.SelectedDate != null && DataGridBox.SelectedItem != null)
            {
                Create create = new Create(rents[DataGridBox.SelectedIndex].address_id, StartDateDP.SelectedDate, EndDateDP.SelectedDate);
                create.ShowDialog();
            }
            else
            {
                MessageBox.Show("Choose start and end date", "Error", MessageBoxButton.OK);
            }
        }

        private void Room_Renter_Activated(object sender, System.EventArgs e)
        {
            if (LoginService.userId != 0)
            {
                RentButton.IsEnabled = true;
            }
        }
    }
}
