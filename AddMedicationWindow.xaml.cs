using System;
using System.Windows;

namespace CW2
{
    /// <summary>
    /// Interaction logic for AddMedicationWindow.xaml
    /// </summary>
    public partial class AddMedicationWindow : Window
    {
        private DatabaseManager databaseManager;

        public AddMedicationWindow(DatabaseManager dbManager)
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
            if (string.IsNullOrWhiteSpace(txtMedicationName.Text) || string.IsNullOrWhiteSpace(txtQuantityInStock.Text))
            {
                MessageBox.Show("Необхідно заповнити всі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Medication medication = new Medication();
            medication.MedicationName = txtMedicationName.Text;
            medication.QuantityInStock = Convert.ToInt32(txtQuantityInStock.Text);
            databaseManager.AddMedication(medication);
            this.Close();
        }
    }
}
