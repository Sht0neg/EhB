using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EhB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Context? context;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow add = new AddWindow(this);
            bool? result = add.ShowDialog();
        }
        public void addGood(string title, int? count, DateTime date, string size)
        {
            context = new Context();
            context.Database.EnsureCreated();
            context.Clothes.Load();
            context.Clothes.Add(new Clothe
            {
                Titel = title,
                DateProd = date,
                Count = count,
                Size = size
            }); ;
            context.SaveChanges(true);
        }
        private void InfButton_Click(object sender, RoutedEventArgs e)
        {
            ClWindow inf = new ClWindow(this);
            bool? result = inf.ShowDialog();
        }
    }
}