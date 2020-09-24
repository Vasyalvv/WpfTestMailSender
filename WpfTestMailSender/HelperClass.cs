using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestMailSender
{
    public static class HelperClass
    {
        static string yandexSmtpServer = "smtp.yandex.ru";
        static string gmailSmtpServer = "smtp.gmail.com";
        static string mailSmtpServer = "smtp.mail.ru";
        static int yandexSmtpServerPort = 25;
        static int gmailSmtpServerPort = 587;
        static int mailSmtpServerPort = 25;
        static string senderEmail =  "vasyalvv@gmail.com" ;
        static string[] destinationEmails = { "testmail1@mail.ru", "testmail2@yandex.ru", "testmail3@gmail.com"};

        public static string YandexSmtpServer { get => yandexSmtpServer;  }
        public static string GmailSmtpServer { get => gmailSmtpServer;  }
        public static string MailSmtpServer { get => mailSmtpServer; }
        public static int YandexSmtpServerPort { get => yandexSmtpServerPort; }
        public static int GmailSmtpServerPort { get => gmailSmtpServerPort; }
        public static int MailSmtpServerPort { get => mailSmtpServerPort;  }
        public static string SenderEmail { get => senderEmail; }
        public static string[] DestinationEmails { get => destinationEmails;  }
    }
}
