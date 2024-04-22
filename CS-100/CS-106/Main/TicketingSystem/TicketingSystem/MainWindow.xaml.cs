using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TicketingSystem.Framework;
using TicketingSystem.Frames;

namespace TicketingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool LoggedIn = false; 
        public static User user = new User(); // creates a new user when the application is opened

        /// <summary>
        /// constructor for the main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Debug.SetLogger(new Debug.LogConsole());
            }
            else
#endif
            {
                Debug.SetLogger(new Debug.LogTxt());
                // sets the logger
            }

            Debug.Log(DateTime.Now.ToString());
            Debug.Log("application started");
            // log application start

            if (LoggedIn) // check if logged in
            {
                // show main window if logged in
                MainWindowVisability(true);
                LoginWindowVisability(false);
            }
            else
            {
                // show login window if not logged in
                MainWindowVisability(false);
                LoginWindowVisability(true);
            }
        }

        /// <summary>
        /// changes the window to the specified window
        /// </summary>
        /// <param name="windowName">navigation window name</param>
        public void ChangeWindow(string windowName)
        {
            mainFrame.Navigate(new Uri("./Frames/" + windowName, UriKind.Relative));
            Debug.Log(windowName + " opened");

            if(ViewAccounts.current != null)
            {
                ViewAccounts.current.Refresh();
            }
            if(ViewTickets.current != null)
            {
                ViewTickets.current.Refresh();
            }
        }

        /// <summary>
        /// gets a solid color from a hex value for WPF backend
        /// </summary>
        /// <param name="hex">hex code starting with #</param>
        /// <returns></returns>
        public static SolidColorBrush HexColor(string hex)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
        }

        #region LOGIN-Functions
        /// <summary>
        /// logs the user out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            user = new User();
            LoggedIn = false;
            MainWindowVisability(LoggedIn);
            LoginWindowVisability(true);
            ((Login)Login.Content).ResetText();
        }

        /// <summary>
        /// hides or shows the main window and nav bar
        /// </summary>
        /// <param name="shown">bool for showing/hiding the main window</param>
        private void MainWindowVisability(bool shown)
        {
            if (shown)
            {
                ContentHolder.Visibility = Visibility.Visible;
                SideBarHolder.Visibility = Visibility.Visible;
            }
            else
            {
                ContentHolder.Visibility = Visibility.Hidden;
                SideBarHolder.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// hides or shows the login window
        /// </summary>
        /// <param name="shown">bool for showing/hiding the login window</param>
        private void LoginWindowVisability(bool shown)
        {
            if (shown)
            {
                Login.Navigate(new Uri("./Frames/Login.xaml", UriKind.Relative));
                Login.Visibility = Visibility.Visible;
            }
            else
            {
                Login.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// tries to login a user
        /// </summary>
        /// <param name="username">username to login with</param>
        /// <param name="password">password to login with</param>
        /// <returns></returns>
        public bool LoginActivation(string username, string password)
        {
            if (user.Login(username, password)) // try login - result true if success
            {
                MainWindowVisability(true);
                LoginWindowVisability(false);
                ChangeWindow("Dashboard.xaml");
                GenerateSideNavButtons((User.Type)user.userType);

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region SIDE-Nav
        /// <summary>
        /// creates the side nav buttons based on the logged in user
        /// </summary>
        /// <param name="loginType">login type to create buttons based off</param>
        private void GenerateSideNavButtons(User.Type loginType)
        {
            var sidenav = SideNavButtonsHolder;
            sidenav.Children.Clear(); // removes current buttons
            switch (loginType)
            {
                case User.Type.User:
                {
                    CreateSideNavButton("Dashboard", "Dashboard.xaml", "./Resources/Icons/Home.png");
                    CreateSideNavButtonT("View Tickets", "ViewTickets.xaml", "./Resources/Icons/File_dock_search.png", 1);
                    CreateSideNavButtonT("Closed Tickets", "ViewTickets.xaml", "./Resources/Icons/Arhives_group_docks.png", 2);
                    CreateSideNavButton("Create Ticket", "CreateTicket.xaml", "./Resources/Icons/File_dock_add.png");
                    CreateSideNavButton("My Account", "MyAccount.xaml", "./Resources/Icons/Lock.png");
                    
                    // Code for User
                    break;
                }
                case User.Type.Tech:
                {
                    CreateSideNavButton("Dashboard", "Dashboard.xaml", "./Resources/Icons/Home.png");
                    CreateSideNavButtonT("View Tickets", "ViewTickets.xaml", "./Resources/Icons/File_dock_search.png", 1);
                    CreateSideNavButtonT("Closed Tickets", "ViewTickets.xaml", "./Resources/Icons/Arhives_group_docks.png", 2);
                    CreateSideNavButtonT("All Tickets", "ViewTickets.xaml", "./Resources/Icons/Arhives_group_docks.png", 3);
                    CreateSideNavButton("Create Ticket", "CreateTicket.xaml", "./Resources/Icons/File_dock_add.png");
                    CreateSideNavButton("My Account", "MyAccount.xaml", "./Resources/Icons/Lock.png");
                    
                    // Code for Tech
                    break;
                }    
                case User.Type.Admin:
                {
                    CreateSideNavButton("Dashboard", "Dashboard.xaml", "./Resources/Icons/Home.png");
                    CreateSideNavButtonT("All Tickets", "ViewTickets.xaml", "./Resources/Icons/File_dock_search.png", 4);
                    CreateSideNavButton("All Accounts", "ViewAccounts.xaml", "./Resources/Icons/People.png");
                    CreateSideNavButton("Create Ticket", "CreateTicket.xaml", "./Resources/Icons/File_dock_add.png");
                    CreateSideNavButton("Create Account", "CreateAccount.xaml", "./Resources/Icons/User_add.png");
                    CreateSideNavButton("My Account", "MyAccount.xaml", "./Resources/Icons/Lock.png");
                    
                    // Code for Admin
                    break;
                }
                case User.Type.Test:
                default:
                {
                    CreateSideNavButton("Dashboard", "Dashboard.xaml", "./Resources/Icons/Home.png");
                    CreateSideNavButton("My Account", "MyAccount.xaml", "./Resources/Icons/Lock.png");
                    // Code for Test or No Valid Value - For Testing
                    break;
                }
            } // shows based on user
        }

        /// <summary>
        /// creates a sidebar button for navigation
        /// </summary>
        /// <param name="nameDisplay">Name of the function as displayes</param>
        /// <param name="nameFrame">Frame to show location</param>
        /// <param name="iconLocation">Icon location to display</param>
        void CreateSideNavButton(string nameDisplay, string nameFrame, string iconLocation)
        {
            #region wpf-stuff
            var sidenav = SideNavButtonsHolder;
            Button main = new Button();
            StackPanel stackPanel = new StackPanel();
            Image icon = new Image();
            TextBlock textBlock = new TextBlock();

            // button properties
            main.Margin = new Thickness(10);
            main.Background = HexColor("#00000000");
            main.BorderBrush = HexColor("#00000000");
            main.Foreground = HexColor("#FFF9F9F9");

            main.Click += (sender, e) => ChangeWindow(nameFrame);

            // stack panel properties
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Width = 305;

            // icon properties
            icon.HorizontalAlignment = HorizontalAlignment.Left;
            icon.Height = 33;
            icon.Width = 33;
            icon.Source = new BitmapImage(new Uri(iconLocation, UriKind.Relative));
            icon.Margin = new Thickness(10, 0, 3, 7);

            // text block properties
            textBlock.Text = nameDisplay;
            textBlock.FontFamily = (FontFamily)FindResource("Epilogue");
            textBlock.FontWeight = FontWeights.SemiBold;
            textBlock.Height = 38;
            textBlock.Foreground = HexColor("#FFF9F9F9");
            textBlock.FontSize = 32;
            textBlock.TextWrapping = TextWrapping.NoWrap;

            // add each element to correct parents
            stackPanel.Children.Add(icon);
            stackPanel.Children.Add(textBlock);
            main.Content = stackPanel;
            sidenav.Children.Add(main);
            #endregion
        }

        /// <summary>
        /// creates a sidebar button for navigation
        /// </summary>
        /// <param name="nameDisplay">Name of the function as displayes</param>
        /// <param name="nameFrame">Frame to show location</param>
        /// <param name="iconLocation">Icon location to display</param>
        void CreateSideNavButtonT(string nameDisplay, string nameFrame, string iconLocation, int selection)
        {
            #region wpf-stuff
            var sidenav = SideNavButtonsHolder;
            Button main = new Button();
            StackPanel stackPanel = new StackPanel();
            Image icon = new Image();
            TextBlock textBlock = new TextBlock();

            // button properties
            main.Margin = new Thickness(10);
            main.Background = HexColor("#00000000");
            main.BorderBrush = HexColor("#00000000");
            main.Foreground = HexColor("#FFF9F9F9");

            main.Click += (sender, e) =>
            {
                ViewTickets.viewType = selection;
                ChangeWindow(nameFrame);
            };
            

            // stack panel properties
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Width = 305;

            // icon properties
            icon.HorizontalAlignment = HorizontalAlignment.Left;
            icon.Height = 33;
            icon.Width = 33;
            icon.Source = new BitmapImage(new Uri(iconLocation, UriKind.Relative));
            icon.Margin = new Thickness(10, 0, 3, 7);

            // text block properties
            textBlock.Text = nameDisplay;
            textBlock.FontFamily = (FontFamily)FindResource("Epilogue");
            textBlock.FontWeight = FontWeights.SemiBold;
            textBlock.Height = 38;
            textBlock.Foreground = HexColor("#FFF9F9F9");
            textBlock.FontSize = 32;
            textBlock.TextWrapping = TextWrapping.NoWrap;

            // add each element to correct parents
            stackPanel.Children.Add(icon);
            stackPanel.Children.Add(textBlock);
            main.Content = stackPanel;
            sidenav.Children.Add(main);
            #endregion
        }
        #endregion


    }
}