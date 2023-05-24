using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CW2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseManager databaseManager;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(DatabaseManager dbManager)
        {
            InitializeComponent();
            // Save the instance of DatabaseManager for use in the main window
            databaseManager = dbManager;

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = myTabControl.SelectedItem as TabItem;
            Window addRecordWindow = null;

            switch (selectedTab.Header.ToString())
            {
                case "Пацієнти":
                    addRecordWindow = new AddPatientWindow(databaseManager);
                    break;
                case "Лікарі":
                    addRecordWindow = new AddDoctorWindow(databaseManager);
                    break;
                case "Відділення":
                    addRecordWindow = new AddDepartmentWindow(databaseManager);
                    break;
                case "Медичні процедури":
                    addRecordWindow = new AddMedicalProcedureWindow(databaseManager);
                    break;
                case "Призначення":
                    addRecordWindow = new AddAppointmentWindow(databaseManager);
                    break;
                case "Візити":
                    addRecordWindow = new AddVisitWindow(databaseManager);
                    break;
                case "Медикаменти":
                    addRecordWindow = new AddMedicationWindow(databaseManager);
                    break;
                default:
                    break;
            }

            try
            {
                if (addRecordWindow != null)
                {
                    bool? result = addRecordWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при додаванні запису.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                UpdateDataGrid();
            }

        }

        private void UpdDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
        private void UpdateDataGrid()
        {
            TabItem selectedTab = myTabControl.SelectedItem as TabItem;
            string tableName = "";
            DataGrid currentDataGrid = null;

            switch (selectedTab.Header.ToString())
            {
                case "Пацієнти":
                    tableName = "Patients";
                    currentDataGrid = dataGrid;
                    break;
                case "Лікарі":
                    tableName = "Doctors";
                    currentDataGrid = dataGrid2;
                    break;
                case "Відділення":
                    tableName = "Departments";
                    currentDataGrid = dataGrid3;
                    break;
                case "Медичні процедури":
                    tableName = "MedicalProcedures";
                    currentDataGrid = dataGrid4;
                    break;
                case "Призначення":
                    tableName = "Appointments";
                    currentDataGrid = dataGrid5;
                    break;
                case "Візити":
                    tableName = "Visits";
                    currentDataGrid = dataGrid6;
                    break;
                case "Медикаменти":
                    tableName = "Medications";
                    currentDataGrid = dataGrid7;
                    break;
                default:
                    break;
            }

            if (currentDataGrid != null)
            {
                // Execute a query to the database and retrieve the results
                DataTable dataTable = databaseManager.ExecuteQuery($"SELECT * FROM {tableName}");
                // Set the query results as the data source for the DataGrid
                currentDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = myTabControl.SelectedItem as TabItem;
            DataGrid currentDataGrid = null;

            // определение текущего DataGrid
            switch (selectedTab.Header.ToString())
            {
                case "Пацієнти":
                    currentDataGrid = dataGrid;
                    break;
                case "Лікарі":
                    currentDataGrid = dataGrid2;
                    break;
                case "Відділення":
                    currentDataGrid = dataGrid3;
                    break;
                case "Медичні процедури":
                    currentDataGrid = dataGrid4;
                    break;
                case "Призначення":
                    currentDataGrid = dataGrid5;
                    break;
                case "Візити":
                    currentDataGrid = dataGrid6;
                    break;
                case "Медикаменти":
                    currentDataGrid = dataGrid7;
                    break;
                default:
                    break;
            }

            if (currentDataGrid != null && currentDataGrid.SelectedItem is DataRowView dataRow)
            {
                string tableName = GetTableName(currentDataGrid);
                string idColumnName = GetIdColumnName(currentDataGrid);

                int id = Convert.ToInt32(dataRow[idColumnName]);
                databaseManager.DeleteRecord(tableName, idColumnName, id);
                UpdateDataGrid();
            }

        }
        private string GetIdColumnName(DataGrid cDataGrid)
        {
            if (cDataGrid == dataGrid)
                return "ID_patient";
            else if (cDataGrid == dataGrid2)
                return "ID_doctor";
            else if (cDataGrid == dataGrid3)
                return "ID_department";
            else if (cDataGrid == dataGrid4)
                return "ID_procedure";
            else if (cDataGrid == dataGrid5)
                return "ID_appointment";
            else if (cDataGrid == dataGrid6)
                return "ID_visit";
            else if (cDataGrid == dataGrid7)
                return "ID_medication";
            else
                throw new Exception("Unknown DataGrid"); // throw an exception if the DataGrid is not recognized
        }


        private string GetTableName(DataGrid cDataGrid)
        {
            if (cDataGrid == dataGrid)
                return "Patients";
            else if (cDataGrid == dataGrid2)
                return "Doctors";
            else if (cDataGrid == dataGrid3)
                return "Departments";
            else if (cDataGrid == dataGrid4)
                return "MedicalProcedures";
            else if (cDataGrid == dataGrid5)
                return "Appointments";
            else if (cDataGrid == dataGrid6)
                return "Visits";
            else if (cDataGrid == dataGrid7)
                return "Medications";
            else
                throw new Exception("Unknown DataGrid"); // throw an exception if the DataGrid is not recognized
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;

            // Get the column and row being edited
            DataGridColumn column = e.Column;
            DataGridRow row = e.Row;

            // Get the original and current values of the cell
            string originalValue = ((DataRowView)row.Item)[column.DisplayIndex].ToString();
            string currentValue = ((TextBox)e.EditingElement).Text;

            // If the original value is the same as the current value, no update is needed
            if (originalValue == currentValue)
                return;

            // Get the ID of the row being edited
            int id = Convert.ToInt32(((DataRowView)row.Item)[0]);

            // Get the name of the column being edited
            string columnName = column.Header.ToString();

            // Check if the column represents the 'DateOfBirth' field
            if (columnName == "DateOfBirth" || columnName == "AppointmentDate" || columnName == "VisitDate")
            {
                // Parse the current value as a DateTime
                if (DateTime.TryParse(currentValue, out DateTime parsedDate))
                {
                    // Convert the parsed date to the desired format
                    currentValue = parsedDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    // Invalid date format, show an error message or handle it as needed
                    // For example, display a message box with an error message
                    MessageBox.Show("Неправильний формат дати. Будь ласка, введіть правильну дату (yyyy-MM-dd).");
                    return;
                }
            }

            databaseManager.UpdateRecord(GetTableName(grid), columnName, currentValue, GetIdColumnName(grid), id);
            UpdateDataGrid();

        }
        private void textBoxSearchByName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string searchValue = textBox.Text;

            TabItem selectedTab = myTabControl.SelectedItem as TabItem;
            string tableName = "";

            switch (selectedTab.Header.ToString())
            {
                case "Пацієнти":
                    tableName = "Patients";
                    break;
                case "Лікарі":
                    tableName = "Doctors";
                    break;
                default:
                    break;
            }

            string query = $"SELECT * FROM {tableName} WHERE LastName LIKE '%{searchValue}%'";
            DataTable dataTable = databaseManager.ExecuteQuery(query);

            dataGrid.ItemsSource = dataTable.DefaultView;
        }

    }
}
