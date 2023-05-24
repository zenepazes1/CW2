using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CW2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DatabaseManager databaseManager;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Create an instance of DatabaseManager and establish the connection
            string connectionString = "server=localhost;port=3306;database=HospitalDB;uid=user1;password=lovbyou123;";

            databaseManager = new DatabaseManager(connectionString);
            databaseManager.Connect();

            // Create an instance of the main application window and display it
            MainWindow mainWindow = new MainWindow(databaseManager);
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            // Disconnect from the database
            databaseManager.Disconnect();
        }
    }
}
