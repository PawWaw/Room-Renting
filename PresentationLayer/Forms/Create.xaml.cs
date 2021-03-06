﻿using BizzLayer;
using BizzLayer.Windows;
using DataLayer;
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
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        private long address_id;
        private DateTime? startDate;
        private DateTime? endDate;
        private int beds;
        CreateService createService = new CreateService();

        public Create()
        {
            InitializeComponent();
        }

        public Create(long address_id, DateTime? startDate, DateTime? endDate, int beds)
        {
            InitializeComponent();
            this.address_id = address_id;
            this.startDate = startDate;
            this.endDate = endDate;
            this.beds = beds;
        }

        private void RentButton_Click(object sender, RoutedEventArgs e)
        {
            if (!BedCountTB.Equals(""))
            {
                int parsed;
                if (int.TryParse(BedCountTB.Text, out parsed))
                {
                    if (parsed > beds || parsed < 0)
                    {
                        Message msg = new Message("Wrong bed number to reservation");
                        msg.ShowDialog();
                    }
                    else
                    {
                        Rents rent = new Rents();
                        rent.startDate = (DateTime)this.startDate;
                        rent.endDate = (DateTime)this.endDate;
                        rent.is_actual = true;
                        rent.pending = false;
                        rent.isRated = false;
                        rent.user_id = LoginService.userId;
                        rent.address_id = address_id;

                        createService.addRent(rent);
                        this.Close();
                    }
                }
                else
                {
                    Message msg = new Message("Wrong bed number to reservation");
                    msg.ShowDialog();
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
