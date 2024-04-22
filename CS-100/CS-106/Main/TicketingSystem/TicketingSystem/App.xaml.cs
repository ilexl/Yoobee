using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TicketingSystem.Framework;

namespace TicketingSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += UnhandledException;
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            MessageBox.Show("A fatal error has occured, more details in log.txt", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Debug.LogError("UnhandledException caught : " + ex.Message);
            Debug.LogError("UnhandledException StackTrace : " + ex.StackTrace);
        }
    }
}
