using System;
using System.Text;
using System.Windows;
using WPFRestoran.Models;

namespace WPFRestoran
{
    public partial class UpdateMenuWindow : Window
    {
        private Models.Menu _currentMenu = new Models.Menu();
        public UpdateMenuWindow(Models.Menu selectedMenu)
        {
            InitializeComponent();
            if (selectedMenu != null)
            {
                _currentMenu = selectedMenu;
            }
            DataContext = _currentMenu;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();
        }

        private void AcceptBtn1_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(_currentMenu.Dish))
                errors.AppendLine("Укажите название отеля");
            if (_currentMenu.Price < 0)
                errors.AppendLine("Укажите адекватную цену");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentMenu.Id == 0)
            {
                RestoranEntities.GetContext().Menu.Add(_currentMenu);
            }          
            try
            {
                RestoranEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                new MenuWindow().Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
