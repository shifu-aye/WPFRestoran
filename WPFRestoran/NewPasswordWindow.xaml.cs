using System;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using WPFRestoran.Models;

namespace WPFRestoran
{
    public partial class NewPasswordWindow : Window
    {
        public int code = 0;
        public static string mail;
        public NewPasswordWindow()
        {
            InitializeComponent();           
        }
        
        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            MailAddress from = new MailAddress("aleshka.karnaukh@mail.ru", "Алексей Карнаух");
            MailAddress to = new MailAddress(emailTB.Text);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Код для восстановления";
            using (RestoranEntities db = new RestoranEntities())
            {
                foreach (User user in db.User)
                {
                    if (emailTB.Text == user.Email)
                    {
                        Random random = new Random();
                        code = random.Next(1000, 9999);
                        message.Body = "Код для восстановления пароля: " + code.ToString();
                    }
                }
            }
            
            
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("", "");
            smtp.EnableSsl = true;
            smtp.Send(message);
            
        }
        private void acceptBtn_Click(object sender, RoutedEventArgs e)
        {
            using (RestoranEntities db = new RestoranEntities())
            {
                foreach (User user in db.User)
                {
                    if (user.Email == emailTB.Text)
                    {
                        user.Password = newPassTB.Text;
                    }
                }
                
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Данные сохранены!");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                if (new User().Role == false)
                {
                    new MainWindow().Show();
                    Close();
                }
                else
                {
                    new MainWindowAdmin().Show();
                    Close();
                }
                
            }
        }

        private void codeTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (codeTB.Text.Length >= 4)
            {
                try
                {
                    if (code == Convert.ToInt32(codeTB.Text))
                    {
                        newPassTB.IsEnabled = true;
                        acceptBtn.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Неверный код!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Неверный код!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            User user = new User();
            foreach (var i in RestoranEntities.GetContext().User)
            {
                if (i.Role == true)
                {
                    new MainWindowAdmin().Show();
                    break;

                }
                else if (i.Role == false)
                {
                    new MainWindow().Show();
                    break;

                }
            }
        }
    }
}
