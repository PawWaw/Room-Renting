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
                RenterCombobox.IsEnabled = false;

                List<Addresses> userAddresses = calendarService.getUserAddresses(LoginService.userId);
                rents = calendarService.getUserFutureRents(LoginService.userId);

                for (int i = 0; i < userAddresses.Count; i++)
                {
                    RenterCombobox.Items.Add(userAddresses[i].street);
                    DateTime date = new DateTime(2020, 10, 10);
                }

                loadLandlordColumns(rents);
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
                loadLandlordColumns(rents);
            }
            else
            {
                RenterCombobox.IsEnabled = false;
                loadTenantColumns();
            }
        }

        private void loadLandlordColumns(List<Rents> rents)
        {
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

        }
    }
}
