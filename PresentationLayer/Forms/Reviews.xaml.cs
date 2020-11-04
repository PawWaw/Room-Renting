using BizzLayer;
using BizzLayer.Windows;
using DataLayer;
using DataLayer.Classes;
using Room_Renting.WS;
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

namespace Room_Renting.Forms
{
    /// <summary>
    /// Interaction logic for Reviews.xaml
    /// </summary>
    public partial class Reviews : Window
    {
        ReviewService reviewService = new ReviewService();
        DbSetters dbSetters = new DbSetters();
        List<long> ids = new List<long>();

        public Reviews()
        {
            InitializeComponent();
            if (LoginService.userId != 0)
            {
                RentCB.IsEnabled = true;
                RateRB.IsEnabled = true;
                List<WSRents> rents = reviewService.getUserRents(LoginService.userId);
                for (int i = 0; i < rents.Count; i++)
                {
                    RentCB.Items.Add(rents[i].address + ", " + rents[i].startDate.ToShortDateString() + "-" + rents[i].endDate.ToShortDateString());
                    ids.Add(rents[i].addressId);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if ((RentCB.SelectedItem != null))
            {
                dbSetters.rateAddress(RateRB.Value, ids[RentCB.SelectedIndex]);
                dbSetters.isRated(ids[RentCB.SelectedIndex], RateRB.Value);
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong data!", "Error", MessageBoxButton.OK);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
