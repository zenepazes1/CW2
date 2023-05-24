using System.Windows;

namespace CW2
{
    /// <summary>
    /// Interaction logic for AddDoctorWindow.xaml
    /// </summary>
    public partial class AddDoctorWindow : Window
    {
        private DatabaseManager databaseManager;

        public AddDoctorWindow(DatabaseManager dbManager)
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
        string.IsNullOrWhiteSpace(txtFathersName.Text) || string.IsNullOrWhiteSpace(txtSpecialization.Text))
            {
                MessageBox.Show("Необхідно заповнити всі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Doctor doctor = new Doctor();
            doctor.FirstName = txtFirstName.Text;
            doctor.LastName = txtLastName.Text;
            doctor.FathersName = txtFathersName.Text;
            doctor.Specialization = txtSpecialization.Text;

            databaseManager.AddDoctor(doctor);

            this.Close();
        }
    }

}
