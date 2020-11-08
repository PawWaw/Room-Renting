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
        private bool isRegister = true;
        private long id = 0;
        
        public Register()
        {
            InitializeComponent();
        }

        public Register(Users user, PersonalData data)
        {
            InitializeComponent();
            this.isRegister = false;
            this.id = data.id;
            this.Title = "Modify";
            RegisterButton.Content = "Update";
            TextLabel.Text = "Update your data:";
            UsernameTextbox.IsEnabled = false;
            PasswordTextbox.IsEnabled = false;
            UsernameTextbox.Text = user.username;
            PasswordTextbox.Password = user.password;
            MailTextbox.Text = user.email_addr;
            NameTextbox.Text = data.name;
            SurnameTextbox.Text = data.surname;
            PhoneTextbox.Text = data.phone_number.ToString();
            IsRenterCheckbox.IsChecked = user.acc_type.Contains("R") ? true : false;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (isRegister)
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

                user.acc_type = IsRenterCheckbox.IsChecked == true ? "R" : "C";

                if (validatedPersonal && validateUserData(user))
                {
                    registerService.createUser(user);
                }

                this.Close();
            }
            else
            {
                PersonalData personalData = new PersonalData();
                personalData.id = this.id;
                personalData.name = NameTextbox.Text;
                personalData.surname = SurnameTextbox.Text;
                personalData.phone_number = long.Parse(PhoneTextbox.Text);
                Users user = new Users();
                user.username = UsernameTextbox.Text;
                user.acc_type = IsRenterCheckbox.IsChecked == true ? "R" : "C";
                user.email_addr = MailTextbox.Text;

                if (validatePersonalData(personalData) && validateUserData(user))
                {
                    registerService.updateUser(user);
                    registerService.updatePersonalData(personalData);
                }

                this.Close();
            }
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
