using System;
using System.Data;
using System.Windows;

namespace CW2
{
    /// <summary>
    /// Interaction logic for AddVisitWindow.xaml
    /// </summary>
    public partial class AddVisitWindow : Window
    {
        private DatabaseManager databaseManager;

        public AddVisitWindow(DatabaseManager dbManager)
        {
            InitializeComponent();
            databaseManager = dbManager;
            FillComboBoxes();
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbPatientID.SelectedItem == null || cbDepartmentID.SelectedItem == null || dpVisitDate.SelectedDate == null)
            {
                MessageBox.Show("Необхідно заповнити всі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int patientId = (int)((dynamic)cbPatientID.SelectedItem).Value;
            int departmentId = (int)((dynamic)cbDepartmentID.SelectedItem).Value;
            DateTime visitDate = dpVisitDate.SelectedDate.Value;
            Visit visit = new Visit();
            visit.PatientID = (int)cbPatientID.SelectedValue;
            visit.DepartmentID = (int)cbDepartmentID.SelectedValue;
            visit.VisitDate = visitDate;
            databaseManager.AddVisit(visit);
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

            DataTable departments = databaseManager.GetDepartments();
            foreach (DataRow row in departments.Rows)
            {
                cbDepartmentID.Items.Add(new { Text = row["DepartmentName"].ToString(), Value = row["ID_department"] });
            }


            cbPatientID.DisplayMemberPath = "Text";
            cbPatientID.SelectedValuePath = "Value";

            cbDepartmentID.DisplayMemberPath = "Text";
            cbDepartmentID.SelectedValuePath = "Value";

        }

    }
}
