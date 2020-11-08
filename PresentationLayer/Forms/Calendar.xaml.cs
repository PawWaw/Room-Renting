using BizzLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using DataLayer;
using BizzLayer.Windows;
using DataLayer.Classes;
using System.Linq;
using System.Windows.Controls;

namespace Room_Renting.Forms
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : Window
    {
        CalendarService calendarService = new CalendarService();
        List<Rents> rents;

        public Calendar()
        {
            InitializeComponent();
            if (!LoginService.isRenter)
            {
                RenterToggle.IsEnabled = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoginService.userId == 0)
            {
                Message msg = new Message("You are not logged in.\nPlease log in to use this panel.");
                msg.ShowDialog();
                this.Close();
            }

            if (!LoginService.isRenter)
            {
                loadTenantColumns();
            }
            else
            {
                loadLandlordColumns();
            }
        }

        private void LoadGrid(List<WSCalendar> rents)
        {
            for (int i = 0; i < rents.Count; i++)
            {
                DataGridBox.Items.Add(rents[i]);
            }
        }

        private void RenterToggle_Click(object sender, RoutedEventArgs e)
        {
            DataGridBox.ClearValue(ItemsControl.ItemsSourceProperty);
            DataGridBox.Items.Clear();
            if (RenterToggle.IsChecked == true)
            {
                RenterCombobox.IsEnabled = true;
                loadLandlordColumns();
            }
            else
            {
                RenterCombobox.IsEnabled = false;
                loadTenantColumns();
            }
        }

        private void loadLandlordColumns()
        {
            RenterCombobox.IsEnabled = true;

            List<Addresses> userAddresses = calendarService.getUserAddresses(LoginService.userId);
            List<long> addressIds = userAddresses.Select(c => c.id).ToList();
            rents = calendarService.getRenterFutureRents(addressIds);

            for (int i = 0; i < userAddresses.Count; i++)
            {
                RenterCombobox.Items.Add(userAddresses[i].street);
                DateTime date = new DateTime(2020, 10, 10);
            }

            List<WSCalendar> calendarList = new List<WSCalendar>();
            for (int i = 0; i < rents.Count; i++)
            {
                WSCalendar temp = new WSCalendar();
                temp.startDate = rents[i].startDate.ToShortDateString();
                temp.endDate = rents[i].endDate.ToShortDateString();
                temp.address = calendarService.getAddress(rents[i].address_id);
                temp.client = calendarService.getPerson(rents[i].user_id);
                calendarList.Add(temp);
            }
            LoadGrid(calendarList);
        }

        private void loadTenantColumns()
        {
            RenterCombobox.IsEnabled = false;

            rents = calendarService.getUserFutureRents(LoginService.userId);
            List<WSCalendar> calendarList = new List<WSCalendar>();

            for (int i = 0; i < rents.Count; i++)
            {
                WSCalendar temp = new WSCalendar();
                temp.startDate = rents[i].startDate.ToShortDateString();
                temp.endDate = rents[i].endDate.ToShortDateString();
                temp.address = calendarService.getAddress(rents[i].address_id);
                temp.client = calendarService.getPerson(rents[i].user_id);
                calendarList.Add(temp);
            }

            LoadGrid(calendarList);
        }
    }
}
