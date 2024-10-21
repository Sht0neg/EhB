using System;
using System.Collections.Generic;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EhB
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        bool f = false;
        MainWindow? parent;
        ClWindow? sparent;
        public Clothe? CurrentGood;
        public AddWindow()
        {
            InitializeComponent();
        }
        public AddWindow(MainWindow? parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.Title = "Добавление товара";
            this.f = true;
        }

        public AddWindow(ClWindow? parent)
        {
            InitializeComponent();
            this.sparent = parent;
            this.Title = "Изменение товара";
        }
        public AddWindow(Clothe good)
        {
            InitializeComponent();
            this.CurrentGood = good;

            NameBox.Text = good.Titel;
            DataBox.DisplayDate = TimeZoneInfo.ConvertTime(good.DateProd, TimeZoneInfo.Local);
            SizeBox.Text = good.Size;
            CountBox.Text = Convert.ToString(good.Count);
            this.f = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (f)
            {
                parent.addGood(
                NameBox.Text,
                Convert.ToInt32(CountBox.Text),
                DataBox.DisplayDate,
                SizeBox.Text
                );
            }
            else
            {
                CurrentGood.Titel = NameBox.Text;
                CurrentGood.Size = SizeBox.Text;
                CurrentGood.DateProd = TimeZoneInfo.ConvertTime(DataBox.DisplayDate, TimeZoneInfo.Utc);
                CurrentGood.Count = Convert.ToInt32(CountBox.Text);
            }
            Close();
            MessageBox.Show("Товар успешно добавлен!");
            
        }

    }
}
