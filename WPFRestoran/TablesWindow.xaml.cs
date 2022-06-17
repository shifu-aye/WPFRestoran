using System;
using System.Linq;
using System.Windows;
using WPFRestoran.Models;

namespace WPFRestoran
{

    public partial class TablesWindow : Window
    {
        RestoranEntities db;

        public TablesWindow()
        {
            InitializeComponent();
            db = new RestoranEntities();
            DataContext = RestoranEntities.GetContext().User;
            TableDG.ItemsSource = db.Table.ToList();
            UserDG.ItemsSource = RestoranEntities.GetContext().User.ToList();
            BookingDG.ItemsSource = RestoranEntities.GetContext().Booking.ToList();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                db.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                TableDG.ItemsSource = db.Table.ToList();
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindowAdmin window = new MainWindowAdmin();
            window.Show();
            this.Close();
        }
        private void bookBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Table table = TableDG.SelectedItem as Models.Table;
                User user = UserDG.SelectedItem as User;
                string userSNP = Convert.ToString(user.Surname) + " " + Convert.ToString(user.Name) + " " + Convert.ToString(user.Patronymic) + " имеет столик под номером №" + Convert.ToString(table.Id);
                int tableID = table.Id;
                int userID = user.Id;
                Booking booking = new Booking();
                booking.FK_Table = tableID;
                booking.FK_User = userID;

                if (table.Status != false)
                {
                    MessageBox.Show("Этот столик уже занят");
                }
                else
                {
                    RestoranEntities.GetContext().Booking.Add(booking);
                    table.Status = true;
                    MessageBox.Show($"Столик забронирован:\n{userSNP}");
                    
                }
                try
                {
                    RestoranEntities.GetContext().SaveChanges();
                    new TablesWindow().Show();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void BookingDG_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                RestoranEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                BookingDG.ItemsSource = RestoranEntities.GetContext().Booking.ToList();
            }
        }

        private void TableDG_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                RestoranEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                TableDG.ItemsSource = RestoranEntities.GetContext().Table.ToList();
            }
        }

        private void delBookBtn_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = new Booking();
            var bookForRemoving = BookingDG.SelectedItems.Cast<Booking>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {bookForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {  
                try
                {
                    RestoranEntities.GetContext().Booking.RemoveRange(bookForRemoving);
                    foreach (var i in RestoranEntities.GetContext().Table)
                    {
                        if (i.Id != booking.Id)
                        {
                            i.Status = false;
                        }
                    }  
                    RestoranEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    BookingDG.ItemsSource = RestoranEntities.GetContext().Booking.ToList();
                    new TablesWindow().Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
