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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        MainWindow parent;
        public AddWindow()
        {
            InitializeComponent();
        }

        public AddWindow(MainWindow? parent, string a)
        {
            InitializeComponent();
            this.parent = parent;
            if (a == "add")
            {
                this.Title = "Добавление";
                AddButton.Content = "Добавить";
            }
            else {
                this.Title = "Изменение";
                AddButton.Content = "Изменить";
                ReLoadData();
            }
        }
        public void LoadData()
        {
            string strConnection = "Server=127.0.0.1;Database=Menu;Uid=root;Pwd=12345";
            using (MySqlConnection conn = new MySqlConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"INSERT INTO Dish(name, price, vegetarian, calories) Values ('{NameBox.Text}', {float.Parse(PrcieBox.Text)}, {VegBox.IsChecked}, {int.Parse(CalBox.Text)})", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex) { }
            }
        }
        public void ReLoadData()
        {
            string strConnection = "Server=127.0.0.1;Database=Menu;Uid=root;Pwd=12345";
            using (MySqlConnection conn = new MySqlConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    string strCommand = $"SELECT * FROM Dish WHERE id = {parent.selectedDish()}";
                    MySqlCommand cmd = new MySqlCommand(strCommand, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while(reader.Read())
                        {
                            NameBox.Text = reader.GetString("name");
                            PrcieBox.Text = reader["price"].ToString();
                            VegBox.IsChecked = (bool)reader["vegetarian"];
                            CalBox.Text = reader["calories"].ToString();
                        }
                    }
                    conn.Close();

                }
                catch (Exception ex) { }
            }
        }
        public void ReToLoadData()
        {
            string strConnection = "Server=127.0.0.1;Database=Menu;Uid=root;Pwd=12345";
            using (MySqlConnection conn = new MySqlConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"UPDATE Dish SET name = '{NameBox.Text}', price = {float.Parse(PrcieBox.Text)}, vegetarian = {VegBox.IsChecked}, calories = {int.Parse(CalBox.Text)} WHERE id = {parent.selectedDish()}", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex) { }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Title == "Добавление")
            {
                LoadData();
            }
            else {
                ReToLoadData();
            }
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
