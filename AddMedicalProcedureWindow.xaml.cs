using System.Windows;

namespace CW2
{
    /// <summary>
    /// Interaction logic for AddMedicalProcedureWindow.xaml
    /// </summary>
    public partial class AddMedicalProcedureWindow : Window
    {
        private DatabaseManager databaseManager;

        public AddMedicalProcedureWindow(DatabaseManager dbManager)
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
            if (string.IsNullOrWhiteSpace(txtProcedureName.Text) || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Необхідно заповнити всі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MedicalProcedure procedure = new MedicalProcedure();
            procedure.ProcedureName = txtProcedureName.Text;
            procedure.DDescription = txtDescription.Text;
            databaseManager.AddMedicalProcedure(procedure);
            this.Close();
        }
    }
}
