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
            if (LoginService.isRenter)
            {
                RenterToggle.IsEnabled = true;
                RenterCombobox.IsEnabled = true;

                List<Addresses> userAddresses = calendarService.getUserAddresses(LoginService.userId);

                for (int i = 0; i < userAddresses.Count; i++)
                {
                    RenterCombobox.Items.Add(userAddresses[i].street);
                }
            }
        }
    }
}
