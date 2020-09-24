using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace WpfTestMailSender
{
    public class EmailSendServiceClass
    {
        //Список рассылки
        List<string> destinationEmails = new List<string>();
        NetworkCredential nc = new NetworkCredential();
        string subject;
        string body;
        string sender;
        string smtpServer;
        int smtpServerPort;
        string lastError;
        string errorDescrition;

        //Тема письма
        public string Subject { get => subject; set => subject = value; }
        //Содержание письма
        public string Body { get => body; set => body = value; }
        //Адрес отправителя
        public string Sender { get => sender; set => sender = value; }
        //Адрес SMTP сервера
        public string SmtpServer { get => smtpServer; set => smtpServer = value; }
        //Порт SMTP сервера
        public int SmtpServerPort { get => smtpServerPort; set => smtpServerPort = value; }
        public string LastError { get => lastError; }
        public string ErrorDescrition { get => errorDescrition; }

        public EmailSendServiceClass(NetworkCredential senderNC)
        {
            nc.UserName = senderNC.UserName;
            nc.SecurePassword = senderNC.SecurePassword;
        }



        //Добавление нового адресата в список рассылки
        public void AddDestinationEmail(string destEmail)
        {
            destinationEmails.Add(destEmail);
        }

        //Очистка списка рассылки
        public void ClearDestinationEmailList()
        {
            destinationEmails.Clear();
        }

        //Отправка письма
        public int Send()
        {
            if (destinationEmails.Count == 0)
            {
                lastError = "Пустой список адресатов";
                errorDescrition = "";
                return -1;
            }

            if (string.IsNullOrEmpty(sender))
            {
                lastError = "Не задан адрес отправителя";
                errorDescrition = "";
                return -1;
            }
            if (string.IsNullOrEmpty(smtpServer))
            {
                lastError = "Не задан SMTP сервер";
                errorDescrition = "";
                return -1;
            }
            if (smtpServerPort == 0)
            {
                lastError = "Не задан порт SMTP сервера";
                errorDescrition = "";
                return -1;
            }

            foreach (string mail in destinationEmails)
            {
                using (MailMessage mm = new MailMessage(sender, mail))
                {
                    mm.Subject = subject;
                    mm.Body = body;
                    mm.IsBodyHtml = false;

                    using (SmtpClient sc = new SmtpClient(smtpServer, smtpServerPort))
                    {
                        sc.EnableSsl = true;
                        sc.UseDefaultCredentials = false;
                        sc.Credentials = nc;

                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            lastError = "Вызвано исключение. Невозможно отправить письмо";
                            errorDescrition = ex.ToString();
                            return -1;
                        }
                    }
                }
            }
            return 0;
        }
    }
}
