using BizzLayer;
using BizzLayer.Windows;
using DataLayer;
using System.Windows;

namespace Room_Renting.Forms
{
    /// <summary>
    /// Interaction logic for AddRoom.xaml
    /// </summary>
    public partial class AddRoom : Window
    {
        RoomService roomService = new RoomService(); 

        public AddRoom()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CountryTB.Text != "" && CityTB.Text != "" && StreetTB.Text != "" && HouseTB.Text != "" && 
                PostalTB.Text != "" && BedTB.Text != "" && PriceTB.Text != "")
            {
                Addresses address = new Addresses();
                address.country = CountryTB.Text;
                address.city = CityTB.Text;
                address.street = StreetTB.Text;
                address.house = HouseTB.Text;
                if (FlatTB.Text != "")
                {
                    address.flat = FlatTB.Text;
                }
                address.postal_code = PostalTB.Text;
                address.type = IsFlatCB.IsChecked == true ? "F" : "R";
                long addrId = roomService.addAddress(address);

                UserAddresses userAddresses = new UserAddresses();
                userAddresses.addr_id = addrId;
                userAddresses.animals = (bool)AnimalsCB.IsChecked;

                int parsedBeds;
                if (int.TryParse(BedTB.Text, out parsedBeds))
                {
                    if (parsedBeds <= 0)
                    {
                        Message msg = new Message("Wrong bed number to rent");
                        msg.ShowDialog();
                    }
                    else
                    {
                        userAddresses.bed_count = parsedBeds;
                    }
                }

                userAddresses.description = DescriptionTB.Text;
                userAddresses.kitchen = (bool)KitchenCB.IsChecked;
                userAddresses.parking = (bool)ParkingCB.IsChecked;

                int parsedPrice;
                if (int.TryParse(PriceTB.Text, out parsedPrice))
                {
                    if (parsedPrice <= 0)
                    {
                        Message msg = new Message("Wrong bed number to rent");
                        msg.ShowDialog();
                    }
                    else
                    {
                        userAddresses.price = parsedPrice;
                    }
                }

                userAddresses.usr_id = LoginService.userId;

                roomService.addUserAddress(userAddresses);
                this.Close();
            }
            else
            {
                Message msg = new Message("Fill required fields!");
                msg.ShowDialog();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoginService.userId == 0)
            {
                Message msg = new Message("You are not logged in.\nPlease log in to use this panel.");
                msg.ShowDialog();
                this.Close();
            }
            else if (!LoginService.isRenter)
            {
                MainGrid.IsEnabled = false;
                Message msg = new Message("You are not renter.\nYou cannot use this panel.");
                msg.ShowDialog();
            }
        }
    }
}
