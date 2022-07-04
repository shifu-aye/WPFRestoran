using System.Linq;
using System.Windows;
using WPFRestoran.Models;

namespace WPFRestoran
{
    public partial class AhorizationWindow : Window
    {
        RestoranEntities db;
        public AhorizationWindow()
        {
            InitializeComponent();
            db = new RestoranEntities();
        }
        public bool Authorization(string login, string password, bool flag = false)
        {
            for(int i = 0; i < RestoranEntities.GetContext().User.Count(); i++)
            {
                if(login == RestoranEntities.GetContext().User.ToList()[i].Email && password == RestoranEntities.GetContext().User.ToList()[i].Password)
                {
                    flag = true;
                    return true;
                }
            }
            if (!flag)
            {
                MessageBox.Show("Такого пользователя не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }
        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (LogTB.Text.Length > 0) {               
                if(PassPB.Password.Length > 0)
                {
                    User user = new User();
                    foreach (var i in db.User)
                    {
                        if (LogTB.Text == i.Email && PassPB.Password == i.Password)
                        {
                            if(i.Role == true)
                            {
                                MessageBox.Show($"Авторизация прошла успешно!\nДобро пожаловать, {i.Email}", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                                MainWindowAdmin adminWindow = new MainWindowAdmin();
                                adminWindow.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show($"Авторизация прошла успешно!\nДобро пожаловать, {i.Email}", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                                MainWindow window = new MainWindow();
                                window.Show();
                                this.Close();
                            }
                        }  
                    }
                    if (count > 0)
                    {
                        MessageBox.Show("Пользователя с такими данными не существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите логин!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            this.Hide();
            registrationWindow.Show();           
        }
    }
}
