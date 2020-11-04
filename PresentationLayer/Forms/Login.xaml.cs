using System.Windows;
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

                Users user = loginService.getUserByHash(UsernameTB.Text, hash);

                if (user != null)
                {
                    LoginService.userId = user.id;
                    LoginService.user = user.username;
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
