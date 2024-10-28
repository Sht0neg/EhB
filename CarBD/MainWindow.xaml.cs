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
using MySql.Data.MySqlClient;

namespace CarBD
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
        public void LoadData() {
            string strConnection = "Server=127.0.0.1;Database=Car;Uid=root;Pwd=12345";
            using (MySqlConnection conn = new MySqlConnection(strConnection)) {
                try
                {
                    conn.Open();
                    string strCommand = "SELECT * FROM car";
                    MySqlCommand cmd = new MySqlCommand(strCommand, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    table.ItemsSource = dataTable.DefaultView;

                }
                catch (Exception ex) { }
            }
        }
    }
}