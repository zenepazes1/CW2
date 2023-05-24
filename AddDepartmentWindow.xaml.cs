using System.Windows;

namespace CW2
{
    /// <summary>
    /// Interaction logic for AddDepartmentWindow.xaml
    /// </summary>
    public partial class AddDepartmentWindow : Window
    {
        private DatabaseManager databaseManager;

        public AddDepartmentWindow(DatabaseManager dbManager)
        {
            InitializeComponent();
            databaseManager = dbManager;
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDepartmentName.Text))
            {
                MessageBox.Show("Необхідно заповнити всі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Department department = new Department();
            department.DepartmentName = txtDepartmentName.Text;
            databaseManager.AddDepartment(department);
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
