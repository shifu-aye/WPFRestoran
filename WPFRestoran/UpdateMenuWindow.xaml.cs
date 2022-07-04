using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool ChangeMenu(string dish, string description, float price)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(_currentMenu.Dish))
                errors.AppendLine("Укажите название блюда");
            if (_currentMenu.Price < 0)
                errors.AppendLine("Укажите адекватную цену");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return false;
            }
            int count = 0;
            for (int s = 0; s < RestoranEntities.GetContext().Menu.Count(); s++)
            {
                if (DishTB.Text == RestoranEntities.GetContext().Menu.ToList()[s].Dish)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                MessageBox.Show("Такое блюдо уже есть!");
            }
            return true;
        }
        public bool AddMenu(string dish, string description, float price)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(_currentMenu.Dish))
                errors.AppendLine("Укажите название блюда");
            if (_currentMenu.Price < 0)
                errors.AppendLine("Укажите адекватную цену");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return false;
            }
            int count = 0;
            for (int s = 0; s < RestoranEntities.GetContext().Menu.Count(); s++)
            {
                if (DishTB.Text == RestoranEntities.GetContext().Menu.ToList()[s].Dish)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                MessageBox.Show("Такое блюдо уже есть!");
            }
            return true;
        }
        private void AcceptBtn1_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(_currentMenu.Dish))
                errors.AppendLine("Укажите название блюда");
            if (_currentMenu.Price < 0)
                errors.AppendLine("Укажите адекватную цену");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            int count = 0;
            for (int s = 0; s < RestoranEntities.GetContext().Menu.Count(); s++)
            {
                if (DishTB.Text == RestoranEntities.GetContext().Menu.ToList()[s].Dish)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                MessageBox.Show("Такое блюдо уже есть!");
                return;
            }
            else
            {
                try
                {
                    RestoranEntities.GetContext().Menu.Add(_currentMenu);
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
}
