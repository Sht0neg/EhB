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
using Microsoft.EntityFrameworkCore;

namespace EhB
{
    /// <summary>
    /// Логика взаимодействия для ClWindow.xaml
    /// </summary>
    public partial class ClWindow : Window
    {
        public Context? context;
        public MainWindow? parent;
        public ClWindow(MainWindow? parent)
        {
            InitializeComponent();
            this.parent = parent;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context = new Context();

            context.Clothes.Load();

            ClDataGridView.ItemsSource = context.Clothes.Local.ToObservableCollection();
            //ClDataGridView.ItemsSource = context.Clothes.Local.ToBindingList();
        }
        private void ReButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClDataGridView.SelectedItems.Count == 0) return;
            Clothe? selectedItem = ClDataGridView.SelectedItem as Clothe;

            AddWindow re = new AddWindow(selectedItem);
            bool? result = re.ShowDialog();

            Clothe good = context.Clothes.Find(selectedItem.Id);

            if (good == null) return;

            Clothe modif = re.CurrentGood;

            context.Clothes.Update(modif);
            context.SaveChanges();
            context = new Context();

            context.Clothes.Load();

            ClDataGridView.ItemsSource = context.Clothes.Local.ToObservableCollection();
            context.SaveChanges();
        }
    }
}
