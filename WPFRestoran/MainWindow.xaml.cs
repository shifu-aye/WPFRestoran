using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPFRestoran.Models;

namespace WPFRestoran
{
    public partial class MainWindow : Window
    {
        RestoranEntities db;
        protected Point SwipeStart;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Source = new Uri(@"\Pages\LeftPage.xaml", UriKind.RelativeOrAbsolute);
            db = new RestoranEntities();
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
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            new AhorizationWindow().Show();
            Close();
        }

        private void NewPassBtn_Click(object sender, RoutedEventArgs e)
        {
            new NewPasswordWindow().Show();
            Close();
        }
    }
}
