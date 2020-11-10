using System.Windows;

namespace Room_Renting.Forms
{
    /// <summary>
    /// Interaction logic for Warning.xaml
    /// </summary>
    public partial class Warning : Window
    {
        public Warning(string text)
        {
            InitializeComponent();
            MessageLabel.Text = text;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Calendar.isRemoved = true;
            this.Close();
        }
    }
}
