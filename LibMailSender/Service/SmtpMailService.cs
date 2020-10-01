using LibMailSender.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibMailSender.Service
{
    public class SmtpMailService : IMailService
    {
        public IMailSender GetSender(string Server, int Port, bool SSl, string Login, string Password)
        {
            return new SmtpMailSender(Server, Port, SSl, Login, Password);
        }
    }

    internal class SmtpMailSender: IMailSender
    {
        private readonly string _Address;
        private readonly int _Port;
        private readonly bool _SSL;
        private readonly string _Login;
        private readonly string _Password;

        public SmtpMailSender(string Address, int Port, bool SSl, string Login, string Password)
        {
            _Address = Address;
            _Port = Port;
            _SSL = SSl;
            _Login = Login;
            _Password = Password;
        }

        public void Send(string SenderAddress, string RecipientAddress, string Subject, string Body)
        {
            MailAddress from = new MailAddress(SenderAddress);
            MailAddress to = new MailAddress(RecipientAddress);

            using (MailMessage message = new MailMessage(from, to))
            {

                message.Subject = Subject;
                message.Body = Body;

                using (SmtpClient client = new SmtpClient(_Address, _Port))
                {
                    client.EnableSsl = _SSL;

                    client.Credentials = new NetworkCredential
                    {
                        UserName = _Login,
                        Password = _Password
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
