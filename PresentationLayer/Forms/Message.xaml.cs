using System;
using System.Windows;

namespace Room_Renting.Forms
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        public Message(String text)
        {
            InitializeComponent();
            MessageLabel.Text = text;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
