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
using WpfMailSender.Models;
using LibMailSender;
using System.Net.Mail;

namespace WpfMailSender
{
    /// <summary>
    /// Логика взаимодействия для WpfMailSenderWindow.xaml
    /// </summary>
    public partial class WpfMailSenderWindow : Window
    {
        public WpfMailSenderWindow()
        {
            InitializeComponent();
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            Sender sender_ = SendersList.SelectedItem as Sender;
            if (sender is null) return;
            if (!(RecipientsList.SelectedItem is Recipient recipient)) return;
            Server server = ServersList.SelectedItem as Server;
            if (server is null) return;
            Message message = MessagesList.SelectedItem as Message;
            if (message is null) return;

            MailSenderService senderService = new MailSenderService
            {
                ServerAddress = server.Address,
                ServerPort = server.Port,
                UseSSL = server.UseSSL,
                Login = server.Login,
                Password = server.Password
            };

            try
            {
                senderService.SendMessage(sender_.Address, recipient.Address, message.Subject, message.Body);
            }
            catch (SmtpException error)
            {
                MessageBox.Show("Ошибка при отправке почты " + error.Message, "Ошибка",
                    MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
