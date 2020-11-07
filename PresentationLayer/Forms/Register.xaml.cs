using System;
using System.Windows;
using BizzLayer;
using DataLayer;

namespace Room_Renting.Forms
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private RegisterService registerService = new RegisterService();
        private LoginService loginService = new LoginService();
        
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextbox.Text.Equals("") || SurnameTextbox.Text.Equals("") || PhoneTextbox.Text.Equals("") || UsernameTextbox.Text.Equals("") || PasswordTextbox.Password.Equals("") || MailTextbox.Text.Equals(""))
            {
                Message msg = new Message("Missing data");
                msg.ShowDialog();
                return;
            }

            Hashing hashing = new Hashing();
            Users user = new Users();
            PersonalData personalData = new PersonalData();
            long personalId = 0;

            personalData.name = NameTextbox.Text;
            personalData.surname = SurnameTextbox.Text;
            personalData.phone_number = long.Parse(PhoneTextbox.Text);

            bool validatedPersonal = validatePersonalData(personalData);
            if (validatedPersonal)
            {
                personalId = registerService.createPersonalData(personalData);
            }

            user.username = UsernameTextbox.Text;
            user.create_date = DateTime.Now;
            user.email_addr = MailTextbox.Text;
            user.salt = hashing.CreateSalt(10);
            user.password = hashing.GenerateHash(PasswordTextbox.Password, user.salt);
            user.personal_id = personalId;

            if (IsRenterCheckbox.IsChecked == true)
            {
                user.acc_type = "R";
            } 
            else
            {
                user.acc_type = "C";
            }

            if (validatedPersonal && validateUserData(user))
            {
                registerService.createUser(user);
            }

            this.Close();
        }

        private bool validateUserData(Users user)
        {
            if (!user.email_addr.Contains("@"))
            {
                Message msg = new Message("Wrong email address!");
                msg.ShowDialog();
                return false;
            }
            return true;
        }

        private bool validatePersonalData(PersonalData personalData)
        {
            if (personalData.phone_number.ToString().Length != 9)
            {
                Message msg = new Message("Wrong phone number length!");
                msg.ShowDialog();
                return false;
            }
            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
