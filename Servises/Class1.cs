using MySql.Data.MySqlClient;
using System.Collections.Specialized;
using System.Data;

namespace Servises
{
    public class Initialisation
    {
        public DataTable Init(MySqlConnection conn, string tablename)
        {
            conn.Open();
            string strCommand = $"SELECT * FROM {tablename}";
            MySqlCommand cmd = new MySqlCommand(strCommand, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        public void AddRow(MySqlConnection conn, string tablename, string name, float price, bool vegetarian, int calories) {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"INSERT INTO {tablename}(name, price, vegetarian, calories) Values ('{name}', {price}, {vegetarian}, {calories})", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public DataTable FilterTable(MySqlConnection conn, string tablename ,string field, string keyworld)
        {
            conn.Open();
            string strCommand = $"SELECT * FROM {tablename}";
            MySqlCommand cmd = new MySqlCommand(strCommand, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            var result = from r in dataTable.AsEnumerable() where r.Field<string>(field).ToLower().StartsWith(keyworld.ToLower()) select r;
            return result.AsDataView().ToTable();
        }
    }
}
