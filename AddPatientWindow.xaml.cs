using System;
using System.Windows;

namespace CW2
{
    /// <summary>
    /// Interaction logic for AddPatientWindow.xaml
    /// </summary>
    public partial class AddPatientWindow : Window
    {
        private DatabaseManager databaseManager;

        public AddPatientWindow(DatabaseManager dbManager)
        {
            InitializeComponent();
            databaseManager = dbManager;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text) ||
        string.IsNullOrWhiteSpace(txtFathersName.Text) || dpDateOfBirth.SelectedDate == null ||
        string.IsNullOrWhiteSpace(txtPhoneNumber.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Необхідно заповнити всі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var patient = new Patient();
            patient.FirstName = txtFirstName.Text;
            patient.LastName = txtLastName.Text;
            patient.FathersName = txtFathersName.Text;

            patient.DateOfBirth = Convert.ToDateTime(dpDateOfBirth.Text);
            patient.PhoneNumber = txtPhoneNumber.Text;
            patient.Address = txtAddress.Text;

            databaseManager.AddPatient(patient);
            this.Close();
        }
    }
}
