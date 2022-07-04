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
using WPFRestoran.Models;

namespace WPFRestoran
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        RestoranEntities db;
        public MenuWindow()
        {
            InitializeComponent();
            db = new RestoranEntities();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            new UpdateMenuWindow((sender as Button).DataContext as Models.Menu).Show(); 
            Close();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                RestoranEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                MenuDG.ItemsSource = RestoranEntities.GetContext().Menu.ToList();
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateMenuWindow updateMenuWindow = new UpdateMenuWindow(null);
            updateMenuWindow.Show();
            Close();
        }
        public bool RemoveMenu(string dish) {
            var menuForRemoving = MenuDG.SelectedItems.Cast<Models.Menu>().ToList();
            dish = "";
            if (MessageBox.Show($"Вы точно хотите удалить следующие {menuForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    RestoranEntities.GetContext().Menu.RemoveRange(menuForRemoving);
                    RestoranEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    MenuDG.ItemsSource = RestoranEntities.GetContext().Menu.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return false;
        }
        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var menuForRemoving = MenuDG.SelectedItems.Cast<Models.Menu>().ToList();

            if(MessageBox.Show($"Вы точно хотите удалить следующие {menuForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    RestoranEntities.GetContext().Menu.RemoveRange(menuForRemoving);
                    RestoranEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    MenuDG.ItemsSource = RestoranEntities.GetContext().Menu.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindowAdmin mainWindow = new MainWindowAdmin();
            mainWindow.Show();
            Close();
        }
    }
}
