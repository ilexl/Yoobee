using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TicketingSystem.Framework;

namespace TicketingSystem.Frames
{
    /// <summary>
    /// Interaction logic for CreateTicket.xaml
    /// </summary>
    public partial class CreateTicket : Page
    {
        /// <summary>
        /// constructor for create ticket page
        /// </summary>
        public CreateTicket()
        {
            User current = MainWindow.user;
            InitializeComponent();
            CreatedBy.Text = current.ID.ToString(); // sets created by to this user
            CreatedFor.Text = current.ID.ToString(); // sets created for to this user by default (can be changed while in application)
        }

        /// <summary>
        /// creates a ticket if required fields are valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CreateTicket(object sender, RoutedEventArgs e)
        {
            User current = MainWindow.user; // get current user logged in

            // get all variables from XAML inputs
            string title = TitleInput.Text;
            int urgency = Urgency.SelectedIndex + 1; // 1 2 3 for high medium low
            string creatorID = current.ID.ToString();
            string createdFor = CreatedFor.Text;
            string description = Description.Text;

            // check all required values are valid (stop if invalid)
            if (TitleInput.Text == "Title" || TitleInput.Text == "")    //  IF THE USER HAS NOT ENTERED A TITLE
            {
                MessageBox.Show("Please enter a title");
                return;
            }
            else if (Description.Text == "Description" || Description.Text == "")   //  IF THE USER HAS NOT ENTERED A DESCRIPTION
            {
                MessageBox.Show("Please enter a description");
                return;
            }

            // create ticket
            Ticket t = Ticket.CreateNew(createdFor, creatorID, title, urgency, DateTime.Now);
            t.AddComment(description);

            // change the window to view the newly created ticket
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            SpecificTicket.target = t;
            window.ChangeWindow("SpecificTicket.xaml");

        }

        /// <summary>
        /// resets all input boxes to default ghost text
        /// </summary>
        public void ResetText()
        {
            TitleInput.Text = "Username";
            Description.Text = "Description";
            TitleInput.Foreground = Brushes.Gray;
            Description.Foreground = Brushes.Gray;

            SubmitButton.Focus();
        }

        /// <summary>
        /// code when textbox is unfocused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Description_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Description";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }

        /// <summary>
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Description_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Description")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Set the desired text color
            }
        }

        /// <summary>
        /// code when textbox is focused to display correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleInput_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Title")
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
        private void TitleInput_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Title";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }
    }
}
