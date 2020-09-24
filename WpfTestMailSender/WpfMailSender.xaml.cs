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
using System.Net;
using System.Net.Mail;

namespace WpfTestMailSender
{
    /// <summary>
    /// Логика взаимодействия для WpfMailSender.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();
        }

        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            List<string> listStrMails = new List<string> { "lihobabin@polymetal.kz" };
            string strPassword = passwordBox.Password;
            foreach (string mail in listStrMails)
            {
                using (MailMessage mm = new MailMessage("vasyalvv@gmail.com", mail))
                {
                    mm.Subject = "Привет от C#";
                    mm.Body = "Hello? world!";
                    mm.IsBodyHtml = false;

                    using (SmtpClient sc = new SmtpClient("smtp.gmail.com", 587))
                    {
                        sc.EnableSsl = true;
                        sc.UseDefaultCredentials = false;
                        sc.Credentials = new NetworkCredential { UserName = "vasyalvv@gmail.com" , SecurePassword=passwordBox.SecurePassword };
                        
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Невозможно отправить письмо " + ex.ToString());
                        }
                    }
                }
            }
            MessageBox.Show("Работа завершена");
        }
    }
}
