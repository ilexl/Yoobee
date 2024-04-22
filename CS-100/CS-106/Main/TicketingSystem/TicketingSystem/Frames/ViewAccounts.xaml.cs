using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TicketingSystem.Framework;

namespace TicketingSystem.Frames
{
    /// <summary>
    /// Interaction logic for ViewTickets.xaml
    /// </summary>
    public partial class ViewAccounts : Page
    {
        public static ViewAccounts current; // the current instance of the view accounts page
        
        /// <summary>
        /// constructor for view accounts page
        /// </summary>
        public ViewAccounts()
        {
            current = this; // set static current as this
            InitializeComponent();
            Refresh();
        }

        /// <summary>
        /// clears and adds the all the accounts in case of any change
        /// </summary>
        public void Refresh()
        {
            AllAccounts.Children.Clear();
            List<int> allIds = User.GetAllAccountIds();
            foreach (int id in allIds)
            {
                AddAccountToMenu(id);
            }
        }

        /// <summary>
        /// adds an account to the list in wpf format
        /// </summary>
        /// <param name="id">the account to add to the list</param>
        private void AddAccountToMenu(int id)
        {
            User data = User.GetUserFromID(id);
            StackPanel main = AllAccounts;

            // wpf button code
            #region button

            Button button = new Button();
            button.BorderThickness = new Thickness(0);
            button.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            button.Margin = new Thickness(20);

            button.Click += (sender, e) =>
            {
                SpecifcAccount.target = data;
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.ChangeWindow("SpecifcAccount.xaml");
            };

            Style buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(249, 249, 249))));
            ControlTemplate buttonTemplate = new ControlTemplate(typeof(Button));
            FrameworkElementFactory buttonBorder = new FrameworkElementFactory(typeof(Border));
            Binding backgroundBinding = new Binding("Background") { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) };
            buttonBorder.SetBinding(Border.BackgroundProperty, backgroundBinding);
            FrameworkElementFactory contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            buttonBorder.AppendChild(contentPresenter);
            buttonTemplate.VisualTree = buttonBorder;
            buttonStyle.Setters.Add(new Setter(Button.TemplateProperty, buttonTemplate));
            Trigger buttonTrigger = new Trigger();
            buttonTrigger.Property = Button.IsMouseOverProperty;
            buttonTrigger.Value = true;
            buttonTrigger.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(204, 238, 255))));
            buttonStyle.Triggers.Add(buttonTrigger);
            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(10)));
            button.Resources.Add(typeof(Border), borderStyle);
            button.Resources.Add(typeof(Button), buttonStyle);
            #endregion

            // wpf grid code
            #region grid
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(230) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(300) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(100) });
            #endregion

            // wpf label code
            #region label
            Label label1 = new Label();
            label1.FontWeight = FontWeights.SemiBold;
            label1.HorizontalContentAlignment = HorizontalAlignment.Left;
            label1.Content = "Test";
            label1.HorizontalAlignment = HorizontalAlignment.Left;
            label1.Margin = new Thickness(10, 0, 0, 0);
            label1.VerticalAlignment = VerticalAlignment.Center;
            label1.Width = 200;
            label1.Foreground = new SolidColorBrush(Color.FromRgb(17, 17, 34));
            label1.FontSize = 32;
            label1.FontFamily = new FontFamily("Epilogue");
            Grid.SetColumn(label1, 0);

            Label label2 = new Label();
            label2.FontWeight = FontWeights.SemiBold;
            label2.HorizontalContentAlignment = HorizontalAlignment.Left;
            label2.Content = "Test";
            label2.HorizontalAlignment = HorizontalAlignment.Left;
            label2.Margin = new Thickness(0, 0, 0, 5);
            label2.VerticalAlignment = VerticalAlignment.Center;
            label2.Width = 400;
            label2.Foreground = new SolidColorBrush(Color.FromRgb(17, 17, 34));
            label2.FontSize = 32;
            label2.FontFamily = new FontFamily("Epilogue");
            Grid.SetColumn(label2, 1);
            Grid.SetColumnSpan(label2, 2);

            Label label3 = new Label();
            label3.FontWeight = FontWeights.SemiBold;
            label3.HorizontalContentAlignment = HorizontalAlignment.Center;
            label3.Content = "3";
            label3.HorizontalAlignment = HorizontalAlignment.Center;
            label3.Margin = new Thickness(0, 0, 0, 5);
            label3.VerticalAlignment = VerticalAlignment.Center;
            label3.Width = 200;
            label3.Foreground = new SolidColorBrush(Color.FromRgb(17, 17, 34));
            label3.FontSize = 32;
            label3.FontFamily = new FontFamily("Epilogue");
            Grid.SetColumn(label3, 3);

            Label label4 = new Label();
            label4.FontWeight = FontWeights.SemiBold;
            label4.HorizontalContentAlignment = HorizontalAlignment.Right;
            label4.Content = "Name";
            label4.HorizontalAlignment = HorizontalAlignment.Left;
            label4.VerticalAlignment = VerticalAlignment.Center;
            label4.Padding = new Thickness(0, 0, 20, 0);
            label4.Width = 140;
            label4.Foreground = new SolidColorBrush(Color.FromRgb(17, 17, 34));
            label4.FontSize = 32;
            label4.FontFamily = new FontFamily("Epilogue");
            Grid.SetColumn(label4, 6);

            grid.Children.Add(label1);
            grid.Children.Add(label2);
            grid.Children.Add(label3);
            grid.Children.Add(label4);
            
            label1.Content = data.ID.ToString();
            label2.Content = data.firstName + " " + data.lastName;
            label3.Content = User.TypeToString(data);
            label4.Content = data.GetActiveTicketsAmount(data);
            #endregion

            button.Content = grid;
            main.Children.Add(button);
        }

        /// <summary>
        /// create account button pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.ChangeWindow("CreateAccount.xaml");
        }
    }
}
