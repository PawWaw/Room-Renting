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
        string mode = "";

        public Login(string mode)
        {
            InitializeComponent();

            if (mode.Equals("Edit"))
            {
                TextLabel.Text = "Authenticate to modify account";
                this.mode = mode;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTB.Text.Equals("") || PasswordTB.Password.Equals(""))
            {
                Message msg = new Message("Missing data");
                msg.ShowDialog();
                return;
            }

            string salt = loginService.getSalt(UsernameTB.Text);

            if (salt != null)
            {
                string hash = hashing.GenerateHash(PasswordTB.Password, salt);

                Users user = loginService.getUserByHash(UsernameTB.Text, hash);

                if (user != null)
                {
                    if (this.mode.Equals("Edit"))
                    {
                        this.Close();
                        PersonalData data = loginService.getPersonalData(user.personal_id);
                        Register register = new Register(user, data);
                        register.ShowDialog();
                    }
                    else
                    {
                        LoginService.userId = user.id;
                        LoginService.user = user.username;
                        if (user.acc_type.Contains("R"))
                        {
                            LoginService.isRenter = true;
                        }

                        this.Close();
                    }
                }
                else
                {
                    Message msg = new Message("Wrong username or password");
                    msg.ShowDialog();
                }
            }
        }
    }
}
