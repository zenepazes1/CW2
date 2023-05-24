using System.Data;
using System.Windows;

namespace CW2
{
    /// <summary>
    /// Interaction logic for AddAppointmentWindow.xaml
    /// </summary>
    public partial class AddAppointmentWindow : Window
    {
        private DatabaseManager databaseManager;

        public AddAppointmentWindow(DatabaseManager dbManager)
        {
            InitializeComponent();
            databaseManager = dbManager;
            FillComboBoxes();
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbPatientID.SelectedItem == null || cbDoctorID.SelectedItem == null ||
        cbProcedureID.SelectedItem == null || dpAppointmentDate.SelectedDate == null)
            {
                MessageBox.Show("Необхідно заповнити всі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Appointment appointment = new Appointment();
            appointment.PatientID = (int)((dynamic)cbPatientID.SelectedItem).Value;
            appointment.DoctorID = (int)((dynamic)cbDoctorID.SelectedItem).Value;
            appointment.ProcedureID = (int)((dynamic)cbProcedureID.SelectedItem).Value;
            appointment.AppointmentDate = dpAppointmentDate.SelectedDate.Value;
            appointment.DDescription = txtDDescription.Text;
            databaseManager.AddAppointment(appointment);
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void FillComboBoxes()
        {
            DataTable patients = databaseManager.GetPatients();
            foreach (DataRow row in patients.Rows)
            {
                cbPatientID.Items.Add(new { Text = $"{row["FirstName"]}, {row["LastName"]}", Value = row["ID_patient"] });
            }

            DataTable procedures = databaseManager.GetMedicalProcedures();
            foreach (DataRow row in procedures.Rows)
            {
                cbProcedureID.Items.Add(new { Text = row["ProcedureName"].ToString(), Value = row["ID_procedure"] });
            }

            DataTable doctors = databaseManager.GetDoctors();
            foreach (DataRow row in doctors.Rows)
            {
                cbDoctorID.Items.Add(new { Text = $"{row["FirstName"]}, {row["LastName"]}", Value = row["ID_doctor"] });
            }

            cbPatientID.DisplayMemberPath = "Text";
            cbPatientID.SelectedValuePath = "Value";

            cbProcedureID.DisplayMemberPath = "Text";
            cbProcedureID.SelectedValuePath = "Value";

            cbDoctorID.DisplayMemberPath = "Text";
            cbDoctorID.SelectedValuePath = "Value";
        }


    }
}
