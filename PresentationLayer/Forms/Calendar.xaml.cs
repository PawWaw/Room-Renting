using BizzLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataLayer;
using BizzLayer.Windows;

namespace Room_Renting.Forms
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : Window
    {
        CalendarService calendarService = new CalendarService();

        public Calendar()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoginService.userId == 0)
            {
                this.IsEnabled = false;
            }

            Cal.SelectionMode = CalendarSelectionMode.MultipleRange;

            if (LoginService.isRenter)
            {
                RenterToggle.IsEnabled = true;
                RenterCombobox.IsEnabled = false;

                List<Addresses> userAddresses = calendarService.getUserAddresses(LoginService.userId);

                for (int i = 0; i < userAddresses.Count; i++)
                {
                    RenterCombobox.Items.Add(userAddresses[i].street);
                    DateTime date = new DateTime(2020, 10, 10);
                    Cal.SelectedDate = date;
                }
            }
        }

        private void RenterToggle_Click(object sender, RoutedEventArgs e)
        {
            if (RenterToggle.IsChecked == true)
            {
                RenterCombobox.IsEnabled = true;
            }
            else
            {
                RenterCombobox.IsEnabled = false;
            }
        }
    }
}
