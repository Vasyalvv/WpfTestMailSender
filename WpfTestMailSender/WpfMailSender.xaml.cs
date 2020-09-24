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

            EmailSendServiceClass emailSendServiceClass = new EmailSendServiceClass(new NetworkCredential { UserName = HelperClass.SenderEmail, SecurePassword = passwordBox.SecurePassword });

            //Добавляем адресатов
            emailSendServiceClass.AddDestinationEmail(HelperClass.DestinationEmails[0]);
            emailSendServiceClass.AddDestinationEmail(HelperClass.DestinationEmails[1]);
            emailSendServiceClass.AddDestinationEmail(HelperClass.DestinationEmails[2]);

            //Указываем отправителя
            emailSendServiceClass.Sender = HelperClass.SenderEmail;

            //Текст и тема письма
            emailSendServiceClass.Subject = txtSubject.Text;
            emailSendServiceClass.Body = txtBody.Text;

            //Данные SMTP сервера
            emailSendServiceClass.SmtpServer = HelperClass.GmailSmtpServer;
            emailSendServiceClass.SmtpServerPort = HelperClass.GmailSmtpServerPort;

            if (emailSendServiceClass.Send() != 0)
            {
                MessageWindow mw = new MessageWindow();
                mw.SetMessage (emailSendServiceClass.LastError);
                mw.SetDescription(emailSendServiceClass.ErrorDescrition);
                mw.Show();
            }
            else
            {
                SendEndWindow sew = new SendEndWindow();
                sew.ShowDialog();
            }
        }
    }
}
