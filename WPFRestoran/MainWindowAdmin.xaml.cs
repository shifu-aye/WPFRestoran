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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFRestoran.Models;
using System.Windows.Media.Animation;

namespace WPFRestoran
{
    public partial class MainWindowAdmin : Window
    {
        RestoranEntities db;
        protected Point SwipeStart;
        public MainWindowAdmin()
        {
            InitializeComponent();
            MainFrame.Source = new Uri(@"\Pages\LeftPage.xaml", UriKind.RelativeOrAbsolute);
            db = new RestoranEntities();
        }

        private void ToBookTableMI_Click(object sender, RoutedEventArgs e)
        {
            new TablesWindow().Show();
            Close();
        }

        private void DishesMI_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Hide();
        }


        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var Swipe = e.GetPosition(this);

                //Swipe Left
                if (SwipeStart != null && Swipe.X > (SwipeStart.X + 200))
                {
                    // OR Use Your Logic to switch between pages.
                    MainFrame.Source = new Uri(@"\Pages\LeftPage.xaml", UriKind.RelativeOrAbsolute);
                }

                //Swipe Right
                if (SwipeStart != null && Swipe.X < (SwipeStart.X - 200))
                {
                    // OR Use Your Logic to switch between pages.
                    MainFrame.Source = new Uri(@"\Pages\RightPage.xaml", UriKind.RelativeOrAbsolute);
                }
            }
            e.Handled = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SwipeStart = e.GetPosition(this);
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                db.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                showMenuDG.ItemsSource = db.Menu.ToList();
            }
        }

        private void toBasketBtn_Click(object sender, RoutedEventArgs e)
        {
            Models.Menu menu = showMenuDG.SelectedItem as Models.Menu;
            string dish = menu.Dish;
            double price = menu.Price;
            MessageBox.Show($"Вы купили {dish} по цене {price}");
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            new AhorizationWindow().Show();
            Close();
        }

        private void newPassBtn_Click(object sender, RoutedEventArgs e)
        {
            new NewPasswordWindow().Show();
            Close();
        }
    }
}
