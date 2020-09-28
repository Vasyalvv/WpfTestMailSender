using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

namespace LibMailSender
{
    public class MailSenderService
    {
        public string ServerAddress { get; set; }

        public int ServerPort { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool UseSSL { get; set; }

        public void SendMessage(string SendAddress, string RecipientAddress,string Subject, string Body)
        {
            MailAddress from = new MailAddress(SendAddress);
            MailAddress to = new MailAddress(RecipientAddress);

            using (MailMessage message = new MailMessage(from, to))
            {

                message.Subject = Subject;
                message.Body = Body;

                using (SmtpClient client = new SmtpClient(ServerAddress, ServerPort))
                {
                    client.EnableSsl = UseSSL;

                    client.Credentials = new NetworkCredential
                    {
                        UserName = Login,
                        Password = Password
                    };

                    try
                    {
                        client.Send(message);
                    }
                    catch (SmtpException e)
                    {
                        Trace.TraceError(e.ToString());
                        throw;
                    }
                }
            }
        }


    }
}
