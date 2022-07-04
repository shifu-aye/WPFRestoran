using System.Linq;
using System.Text;
using System.Windows;
using WPFRestoran.Models;

namespace WPFRestoran
{
    public partial class RegistrationWindow : Window
    {
        RestoranEntities db;
        public RegistrationWindow()
        {
            InitializeComponent();
            db = new RestoranEntities();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            AhorizationWindow window = new AhorizationWindow();
            window.Show();
        }
        private void MaleCB_Checked(object sender, RoutedEventArgs e)
        {
            if (MaleCB.IsChecked == true)
            {
                MaleCB.Content = "Женщина";
            }
        }
        private void MaleCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (MaleCB.IsChecked == false)
            {
                MaleCB.Content = "Мужчина";
            }
        }
        public bool Registration(string sName, string name, string pName, string login, string password)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(sName))
                errors.AppendLine("Укажите фамилию");
            if (string.IsNullOrEmpty(name))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrEmpty(pName))
                errors.AppendLine("Укажите отчество");
            if (string.IsNullOrEmpty(login))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrEmpty(password))
                errors.AppendLine("Укажите пароль");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return false;
            }
            int count = 0;
            for (int s = 0; s < RestoranEntities.GetContext().User.Count(); s++)
            {
                if(login == RestoranEntities.GetContext().User.ToList()[s].Email)
                {
                    count++;
                }
            }
            if(count > 0)
            {
                errors.AppendLine("Этот пользователь уже зарегистрирован!");
                return false;
            }    
            return true;
        }
        private void RegistBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LogTB.Text.Length > 0)
            {
                string[] dataLogin = LogTB.Text.Split('@'); // делим строку на две части
                if (dataLogin.Length == 2) // проверяем если у нас две части
                {
                    string[] data2Login = dataLogin[1].Split('.'); // делим вторую часть ещё на две части
                    if (data2Login.Length == 2)
                    {
                        if (PassPB.Password.Length > 0)
                        {
                            if (PassPB.Password.Length >= 6)
                            {
                                bool en = true; // английская раскладка
                                bool symbol = false; // символ
                                bool number = false; // цифра

                                for (int i = 0; i < PassPB.Password.Length; i++) // перебираем символы
                                {
                                    if (PassPB.Password[i] >= 'А' && PassPB.Password[i] <= 'Я') en = false; // если русская раскладка
                                    if (PassPB.Password[i] >= '0' && PassPB.Password[i] <= '9') number = true; // если цифры
                                    if (PassPB.Password[i] == '_' || PassPB.Password[i] == '-' || PassPB.Password[i] == '!') symbol = true; // если символ
                                }
                                if (!en)
                                {
                                    MessageBox.Show("Доступна только английская раскладка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // выводим сообщение
                                }
                                else if (!symbol)
                                {
                                    MessageBox.Show("Добавьте один из следующих символов: _ - !", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // выводим сообщение
                                }
                                else if (!number)
                                {
                                    MessageBox.Show("Добавьте хотя бы одну цифру", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // выводим сообщение
                                }
                                if (en && symbol && number) // проверяем соответствие
                                {
                                    if (SurnameTB.Text.Length > 0 && NameTB.Text.Length > 0 && PatronymicTB.Text.Length > 0)
                                    {
                                        User user = new User();
                                        foreach (var items in db.User)
                                        {
                                            if (LogTB.Text == user.Email)
                                            {
                                                MessageBox.Show("Пользователь с таким логином уже есть!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                LogTB.Text = "";
                                            }
                                            else
                                            {
                                                user.Surname = SurnameTB.Text;
                                                user.Name = NameTB.Text;
                                                user.Patronymic = PatronymicTB.Text;
                                                user.Sex = MaleCB.IsChecked.Value;
                                                user.Password = PassPB.Password;
                                                user.Email = LogTB.Text;
                                                user.Role = false;
                                                RestoranEntities.GetContext().User.Add(user);
                                                RestoranEntities.GetContext().SaveChanges();
                                                MessageBox.Show("Регистрация прошла успешно!");
                                                AhorizationWindow window = new AhorizationWindow();
                                                this.Close();
                                                window.Show();
                                            }
                                        }                                     
                                    }
                                    else
                                    {
                                        MessageBox.Show("Введите данные до конца", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("пароль слишком короткий, минимум 6 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Укажите логин в форме х@x.x", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Укажите логин в форме х@x.x", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Введите логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}