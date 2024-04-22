using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TicketingSystem.Framework;

namespace TicketingSystem.Frames
{
    /// <summary>
    /// Interaction logic for MyAccount.xaml
    /// </summary>
    public partial class MyAccount : Page
    {
        // bools for checking focus status

        private bool oldPasswordFocused = false;
        private bool newPasswordFocused = false;
        private bool confirmPasswordFocused = false;
        private bool emailFocused = false;

        /// <summary>
        /// constructor for my account page
        /// </summary>
        public MyAccount()
        {
            InitializeComponent();
            ResetText();
        }

        /// <summary>
        /// applys changes made by the user to their account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick_Apply(object sender, RoutedEventArgs e)
        {
            UpdateChanges();
        }

        /// <summary>
        /// resets the text to ghost text or current users data
        /// </summary>
        public void ResetText()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            OldPassword.Password = string.Empty;
            NewPassword.Password = string.Empty;
            ConfPassword.Password = string.Empty;

            PasswordGhostText.Visibility = Visibility.Visible;
            NewPasswordGhostText.Visibility = Visibility.Visible;
            ConfPasswordGhostText.Visibility = Visibility.Visible;

            if (MainWindow.user.email == null)
            {
                Email.Text = "No e-mail address found.";
            }
            else
            {
                Email.Text = MainWindow.user.email;
            }

            AccountIDTextBlock.Text = "#" + MainWindow.user.ID;
            NameTextBlock.Text = MainWindow.user.firstName + " " + MainWindow.user.lastName;

            ApplyButton.Focus();
        }

        /// <summary>
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OldPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            oldPasswordFocused = true;
            PasswordGhostText.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// code when textbox is unfocused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OldPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(textBox.Password))
            {
                oldPasswordFocused = false;
                PasswordGhostText.Visibility = Visibility.Visible;
            }
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
        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "No e-mail address found.")
            {
                emailFocused = true;
                textBox.Text = string.Empty;
            }
            else if(textBox.Text == MainWindow.user.email)
            {
                emailFocused = true;
                textBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// code when textbox is unfocused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                emailFocused = false;
                if (MainWindow.user.email != "")
                {
                    if (MainWindow.user.email == null)
                    {
                        textBox.Text = "No e-mail address found.";
                    }
                    else
                    {
                        textBox.Text = MainWindow.user.email;
                    }
                }
                else
                {
                    textBox.Text = MainWindow.user.email;
                }
            }
        }

        /// <summary>
        /// checks keyboard input when typing in a text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {

                UpdateChanges();
            }
        }

        private void UpdateChanges()
        {
            if (oldPasswordFocused && newPasswordFocused && confirmPasswordFocused)
            {
                if (NewPassword.Password == ConfPassword.Password)
                {
                    if (!MainWindow.user.ChangePassword(OldPassword.Password, NewPassword.Password))
                    {
                        ResetText();
                        MessageBoxResult wrongOldPass = MessageBox.Show("Old password is incorrect!");
                    }
                    else
                    {
                        MessageBoxResult successfullyChangedPassword = MessageBox.Show("Successfully updated password!");
                    }
                }
                else
                {
                    ResetText();
                    MessageBoxResult nonMatchNewPass = MessageBox.Show("New password does not match in both fields!");
                }
            }
            if (emailFocused)
            {
                if (Email.Text != MainWindow.user.email)
                {
                    if (User.ValidateEmail(Email.Text))
                    {
                        MainWindow.user.ChangeEmail(Email.Text);
                        ResetText();
                        MessageBoxResult successfulEmail = MessageBox.Show("E-mail address successfully updated!");
                    }
                    else
                    {
                        ResetText();
                        MessageBoxResult invalidEmail = MessageBox.Show("E-mail address already in use by other account!");
                    }
                }
            }

            ResetText();
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
        private void PasswordGhostText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            OldPassword.Focus();
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
    }
}
