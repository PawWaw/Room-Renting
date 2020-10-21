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
using BizzLayer;
using DataLayer;

namespace Room_Renting.Forms
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        LoginService loginService = new LoginService();
        Hashing hashing = new Hashing();

        public Login()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTB.Text.Equals("") || PasswordTB.Password.Equals(""))
            {
                MessageBox.Show("Missing data", "Error", MessageBoxButton.OK);
                return;
            }

            string salt = loginService.getSalt(UsernameTB.Text);

            if (salt != null)
            {
                string hash = hashing.GenerateHash(PasswordTB.Password, salt);

                Users user = loginService.getUserByHash(hash);

                if (user != null)
                {
                    LoginService.userId = user.id;
                    if (user.acc_type.Contains("R"))
                    {
                        LoginService.isRenter = true;
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong username or password", "Error", MessageBoxButton.OK);
                }
            }
        }
    }
}
