using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TicketingSystem.Framework;

namespace TicketingSystem.Frames
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Page
    {
        private bool newPasswordFocused = false;
        private bool confirmPasswordFocused = false;
        private bool emailFocused = false;

        /// <summary>
        /// constructor for create account page
        /// </summary>
        public CreateAccount()
        {
            InitializeComponent();
        }

        /// <summary>
        /// returns to previous page in navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        /// <summary>
        /// creates the account if the data is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {

            if (FirstName.Text == "First Name")
            {
                MessageBoxResult invalidFirstName = MessageBox.Show("Please enter a valid value for first name!");
                return;
            }
            if (LastName.Text == "Last Name")
            {
                MessageBoxResult invalidLastName = MessageBox.Show("Please enter a valid value for last name!");
                return;
            }
            if (NewPasswordGhostText.IsVisible || ConfPasswordGhostText.IsVisible)
            {
                MessageBoxResult invalidPassword = MessageBox.Show("Please enter password in both fields!");
                return;
            }
            if (!(NewPassword.Password == ConfPassword.Password))
            {
                MessageBoxResult invalidPassword = MessageBox.Show("Passwords do not match! Please try again!");
                return;
            }
            if (!User.ValidateEmail(EmailAddress.Text))
            {
                MessageBoxResult invalidEmail = MessageBox.Show("Email address already in use!");
                return;
            }
            if (EmailAddress.Text == "Enter Email Address")
            {
                MessageBoxResult invalidEmail = MessageBox.Show("Invalid email address!");
                return;
            }
            
            User u = User.CreateNew(FirstName.Text, LastName.Text, EmailAddress.Text, (User.Type)AccountType.SelectedIndex + 1, NewPassword.Password);
            SpecifcAccount.target = u;
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.ChangeWindow("SpecifcAccount.xaml");
        }

        /// <summary>
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            newPasswordFocused = true;
            NewPasswordGhostText.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// code when textbox is unfocused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(textBox.Password))
            {
                newPasswordFocused = false;
                NewPasswordGhostText.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            confirmPasswordFocused = true;
            ConfPasswordGhostText.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// code when textbox is unfocused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(textBox.Password))
            {
                confirmPasswordFocused = false;
                ConfPasswordGhostText.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPasswordGhostText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewPassword.Focus();
        }

        /// <summary>
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfPasswordGhostText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ConfPassword.Focus();
        }

        private void EmailAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text) || textBox.Text == "Enter Email Address")
            {
                emailFocused = true;
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void EmailAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                emailFocused = true;
                textBox.Foreground = Brushes.Black;
            }
            else if (string.IsNullOrEmpty(textBox.Text))
            {
                emailFocused = false;
                textBox.Text = "Enter Email Address";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void LastName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text) || textBox.Text == "Last Name")
            {
                emailFocused = true;
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void LastName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                emailFocused = true;
                textBox.Foreground = Brushes.Black;
            }
            else if (string.IsNullOrEmpty(textBox.Text))
            {
                emailFocused = false;
                textBox.Text = "Last Name";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void FirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text) || textBox.Text == "First Name")
            {
                emailFocused = true;
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void FirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                emailFocused = true;
                textBox.Foreground = Brushes.Black;
            }
            else if (string.IsNullOrEmpty(textBox.Text))
            {
                emailFocused = false;
                textBox.Text = "First Name";
                textBox.Foreground = Brushes.Gray;
            }
        }
    }
}
