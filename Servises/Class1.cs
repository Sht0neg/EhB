using MySql.Data.MySqlClient;
using System.Data;

namespace Servises
{
    public class Initialisation
    {
        public DataTable Init(MySqlConnection conn)
        {
            conn.Open();
            string strCommand = "SELECT * FROM Dish";
            MySqlCommand cmd = new MySqlCommand(strCommand, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }
    }
}
