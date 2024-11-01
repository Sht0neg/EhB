using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Menu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            string strConnection = "Server=127.0.0.1;Database=Menu;Uid=root;Pwd=12345";
            using (MySqlConnection conn = new MySqlConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    string strCommand = "SELECT * FROM Dish";
                    MySqlCommand cmd = new MySqlCommand(strCommand, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    table.ItemsSource = dataTable.DefaultView;

                }
                catch (Exception ex) { }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow add = new AddWindow(this, "add");
            bool? result = add.ShowDialog();
            LoadData();
        }

        private void ReButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow add = new AddWindow(this, "j");
            bool? result = add.ShowDialog();
            LoadData();
        }

        public int selectedDish() {
            return int.Parse(((DataRowView)table.SelectedItem)["id"].ToString());
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            string strConnection = "Server=127.0.0.1;Database=Menu;Uid=root;Pwd=12345";
            using (MySqlConnection conn = new MySqlConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"DELETE FROM Dish WHERE id = {this.selectedDish()}", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex) { }
            }
            LoadData();
        }
    }
}