using System.Windows;
using System.Windows.Controls;
using TicketingSystem.Framework;

namespace TicketingSystem.Frames
{
    
    /// <summary>
    /// interaction logic for SpecifcAccount.xaml
    /// </summary>
    public partial class SpecifcAccount : Page
    {

        public static User target; // user to display on this page
        private bool accountTypeFocused = false;
        private bool emailFocused = false;
        
        /// <summary>
        /// constructor for specifc accounts
        /// </summary>
        public SpecifcAccount()
        {
            InitializeComponent();
            if(target == null) // check there is a target to display
            {
                // log error as this page should never be opened without setting the target
                Debug.LogError("No target user found for Specific Account");
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.ChangeWindow("ViewAccounts.xaml"); // return to all account page
            }
            else 
            {
                ShowAccountDetails(target); // show the details for the target account
            }
        }

        /// <summary>
        /// shows the account details of the user
        /// </summary>
        /// <param name="user">user's details to show</param>
        private void ShowAccountDetails(User user)
        {
            AccountIDTextBlock.Text = user.ID.ToString();  // account ID
            NameTextBlock.Text = user.firstName + " " + user.lastName; // account name
            Email.Text = user.email; // account email
            AccountType.SelectedIndex = user.userType - 1; // account type
        }

        /// <summary>
        /// resets the targeted users password to a temporary password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick_ResetPassword(object sender, RoutedEventArgs e)
        {
            // prompt the admin y/n and tell the admin the new password
            MessageBoxResult confirm = MessageBox.Show("Are you sure you want to reset this accounts password?","Warning!", MessageBoxButton.YesNo , MessageBoxImage.Warning);
            if(confirm == MessageBoxResult.Yes)
            {
                string password = User.GenerateRandomPassword(); // random password
                target.AdminChangePassword(password); // changes the password
                MessageBox.Show("The new TEMPORARY password is " + password, "New Password!", MessageBoxButton.OK, MessageBoxImage.Information); // inform admin
            }
        }
        
        /// <summary>
        /// discards any changes made
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick_Discard(object sender, RoutedEventArgs e)
        {
            ShowAccountDetails(target); // re show the users data removing old data
        }

        /// <summary>
        /// saves the data changed by the admin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick_Save(object sender, RoutedEventArgs e)
        {
            if (NameTextBlock.Text != target.firstName + " " + target.lastName)
            {
                string newFirstName = "", newLastName = "";
                foreach (char c in NameTextBlock.Text)
                {
                    if (c != ' ')
                    {
                        newFirstName += c;
                    }
                    else if (c == ' ')
                    {
                        break;
                    }
                }

                if ((NameTextBlock.Text.Length <= newFirstName.Length + 1) || NameTextBlock.Text.Length == 0)
                {
                    MessageBox.Show("Can not set name to null or no last name!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                newLastName = NameTextBlock.Text.Substring(newFirstName.Length + 1);

                target.ChangeAccountName(newFirstName, newLastName);
            }

            // TODO: make name editable

            if (Email.Text != target.email)
            {
                if (User.ValidateEmail(Email.Text))
                {
                    target.ChangeEmail(Email.Text);
                }
                else
                {
                    MessageBox.Show("Email address already in use by other account!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            target.ChangeAccountType(AccountType.SelectedIndex + 1);
            MessageBox.Show("Changes saved!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        /// <summary>
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "No e-mail address found.")
            {
                emailFocused = true;
                textBox.Text = string.Empty;
            }
            else if (textBox.Text == MainWindow.user.email)
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
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountTypeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "No e-mail address found.")
            {
                accountTypeFocused = true;
                textBox.Text = string.Empty;
            }
            else if (textBox.Text == MainWindow.user.email)
            {
                accountTypeFocused = true;
                textBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// code when textbox is unfocused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountTypeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
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
        /// deletes an account - warns the admin first
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirm = MessageBox.Show("Are you sure you want to DELETE this account?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (confirm == MessageBoxResult.Yes)
            {
                User.DeleteAccount(target);
                MessageBox.Show("Account DELETED!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.ChangeWindow("ViewAccounts.xaml");
            }
        }
    }
}
