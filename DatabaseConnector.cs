using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW2
{
    public class DatabaseManager
    {
        private MySqlConnection connection;

        public DatabaseManager(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
        }

        public void Connect()
        {
            connection.Open();
        }

        public void Disconnect()
        {
            connection.Close();
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

        public void ExecuteNonQuery(string query)
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        // Метод для добавления пациента в таблицу Patients
        public void AddPatient(Patient patient)
        {
            string query = $"INSERT INTO Patients (FirstName, FathersName, LastName, DateOfBirth, Address, PhoneNumber) " +
                $"VALUES ('{patient.FirstName}', '{patient.FathersName}', '{patient.LastName}', '{patient.DateOfBirth.ToString("yyyy-MM-dd")}', '{patient.Address}', '{patient.PhoneNumber}')";

            ExecuteNonQuery(query);
        }

        public void AddDoctor(Doctor doctor)
        {
            string query = $"INSERT INTO Doctors (FirstName, FathersName, LastName, Specialization) " +
                $"VALUES ('{doctor.FirstName}', '{doctor.FathersName}', '{doctor.LastName}', '{doctor.Specialization}')";

            ExecuteNonQuery(query);
        }

        public void AddDepartment(Department department)
        {
            string query = $"INSERT INTO Departments (DepartmentName) " +
                $"VALUES ('{department.DepartmentName}')";

            ExecuteNonQuery(query);
        }

        public void AddMedicalProcedure(MedicalProcedure procedure)
        {
            string query = $"INSERT INTO MedicalProcedures (ProcedureName, DDescription) " +
                $"VALUES ('{procedure.ProcedureName}', '{procedure.DDescription}')";

            ExecuteNonQuery(query);
        }


        public void AddAppointment(Appointment appointment)
        {
            string query = $"INSERT INTO Appointments (ID_patient, ID_doctor, ID_procedure, DDescription, AppointmentDate) " +
                $"VALUES ('{appointment.PatientID}', '{appointment.DoctorID}', '{appointment.ProcedureID}', '{appointment.DDescription}', '{appointment.AppointmentDate.ToString("yyyy-MM-dd")}')";

            ExecuteNonQuery(query);
        }

        public void AddVisit(Visit visit)
        {
            string query = $"INSERT INTO Visits (ID_patient, ID_department, VisitDate) " +
                 $"VALUES ({visit.PatientID}, {visit.DepartmentID}, '{visit.VisitDate.ToString("yyyy-MM-dd")}')";

            ExecuteNonQuery(query);
        }

        public void AddMedication(Medication medication)
        {
            string query = $"INSERT INTO Medications (MedicationName, QuantityInStock) " +
                $"VALUES ('{medication.MedicationName}', {medication.QuantityInStock})";

            ExecuteNonQuery(query);
        }

        public DataTable GetPatients()
        {
            return ExecuteQuery("SELECT ID_patient, FirstName, LastName FROM Patients");
        }

        public DataTable GetDepartments()
        {
            return ExecuteQuery("SELECT ID_department, DepartmentName FROM Departments");
        }

        public DataTable GetMedicalProcedures()
        {
            return ExecuteQuery("SELECT * FROM MedicalProcedures");
        }

        public DataTable GetDoctors()
        {
            return ExecuteQuery("SELECT * FROM Doctors");
        }

        public void DeleteRecord(string tableName, string idColumnName, int id)
        {
            string deleteQuery = $"DELETE FROM {tableName} WHERE {idColumnName} = {id};";
            ExecuteNonQuery(deleteQuery);
        }
        public void UpdateRecord(string tableName, string columnName, string currentValue, string columnID, int id)
        {
            string query = $"UPDATE `{tableName}` SET `{columnName}` = '{currentValue}' WHERE `{columnID}` = {id}";
            ExecuteNonQuery(query);
        }
    }
}
