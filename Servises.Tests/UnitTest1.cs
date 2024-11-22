using MySql.Data.MySqlClient;
using System.Data;

namespace Servises.Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
            string strConn = "Server=127.0.0.1;Database=MenuTest;Uid=root;Pwd=12345";

            using (var conn = new MySqlConnection(strConn)) {
                conn.Open();
                string str = "CREATE TABLE Dish (id INTEGER PRIMARY KEY AUTO_INCREMENT, name TEXT, price FLOAT, vegetarian BOOL, calories INTEGER);" +
                    "INSERT INTO Dish (name, price, vegetarian, calories) VALUES ('Первое', 120, TRUE, 100);" +
                    "INSERT INTO Dish (name, price, vegetarian, calories) VALUES ('вубабуба', 1780, FALSE, 1000);";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = str;
                cmd.ExecuteNonQuery();
            
            }
        }

        [Test]
        public void Test3()
        {
            string strConn = "Server=127.0.0.1;Database=MenuTest;Uid=root;Pwd=12345";
            using (var conn = new MySqlConnection(strConn))
            {
                DataTable dataTable = new Initialisation().FilterTable(conn, "Dish", "name", "В");
                Assert.That(dataTable.Rows.Count == 1);

                DataRow newr = dataTable.Rows[0];
                Assert.That(newr.Field<string>("name") == "вубабуба");
            }

        }

        [Test]
        public void Test2()
        {
            string strConn = "Server=127.0.0.1;Database=MenuTest;Uid=root;Pwd=12345";
            using (var conn = new MySqlConnection(strConn))
            {
                new Initialisation().AddRow(conn, "Dish", "Второе", 120, true, 100);
                DataTable dataTable = new Initialisation().Init(conn, "Dish");
                Assert.That(dataTable.Rows.Count == 3);

                DataRow newr = dataTable.Rows[2];
                Assert.That(newr.Field<string>("name") == "Второе");
            }
        }

        [Test]
        public void Test1() {
            string strConn = "Server=127.0.0.1;Database=MenuTest;Uid=root;Pwd=12345";
            using (var conn = new MySqlConnection(strConn)) {
                DataTable dataTable = new Initialisation().Init(conn, "Dish");
                DataRow row = dataTable.Rows[0];
                Assert.That(row.Field<string>("name") == "Первое");
            }
            
        }


        [TearDown]
        public void TearDown()
        {
            using (var conn = new MySqlConnection("Server=127.0.0.1;Database=MenuTest;Uid=root;Pwd=12345")) {
                conn.Open();
                string str = "DROP TABLE Dish;";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = str;
                cmd.ExecuteNonQuery();
            }
        }
    }
}