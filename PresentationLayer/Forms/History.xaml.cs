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
using Room_Renting.WS;

namespace Room_Renting.Forms
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Window
    {
        HistoryService historyService = new HistoryService();

        public History()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoginService.userId != 0)
            {
                this.IsEnabled = true;

                List<WSRents> rents  = historyService.getUserRents(LoginService.userId);

                LoadGrid(rents);
            }
        }

        private void LoadGrid(List<WS.WSRents> rents)
        {
            for (int i = 0; i < rents.Count; i++)
            {
                DataGridBox.Items.Add(rents[i]);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
