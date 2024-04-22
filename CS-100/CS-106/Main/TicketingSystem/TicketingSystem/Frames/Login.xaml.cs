using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TicketingSystem.Frames
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        /// <summary>
        /// constructor for the login page
        /// </summary>
        public Login()
        {
            InitializeComponent();
            ResetText(); // reset the text boxes to ghost text
        }

        /// <summary>
        /// resets text boxes into ghost text
        /// </summary>
        public void ResetText()
        {
            LoginUserName.Text = "Username";
            LoginPassword.Password = string.Empty;
            PasswordGhostText.Visibility = Visibility.Visible;
            LoginUserName.Foreground = Brushes.Gray;
            LoginButton.Focus();
        }

        /// <summary>
        /// trys to login the user with credentials from the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick_Login(object sender, RoutedEventArgs e)
        {
            // log the user in - if unsuccessful alert user and reset textboxes
            if(!((MainWindow)Application.Current.MainWindow).LoginActivation(LoginUserName.Text, LoginPassword.Password))
            {
                ResetText();
                MessageBoxResult wrongCredentials = MessageBox.Show("Incorrect credentials!");
            }
        }

        /// <summary>
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Username")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Set the desired text color
            }
        }

        /// <summary>
        /// code when textbox is unfocused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Username";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }

        /// <summary>
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(textBox.Password))
            {
                PasswordGhostText.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// code when textbox is unfocused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(textBox.Password))
            {
                PasswordGhostText.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// checks for enter key when typing to act as login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter) // check for enter and return
            { // act as a login button
                if (!((MainWindow)Application.Current.MainWindow).LoginActivation(LoginUserName.Text, LoginPassword.Password))
                {
                    ResetText();
                    MessageBoxResult wrongCredentials = MessageBox.Show("Incorrect credentials!");
                }
            }
        }

        /// <summary>
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordGhostText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginPassword.Focus();
        }
    }
}
