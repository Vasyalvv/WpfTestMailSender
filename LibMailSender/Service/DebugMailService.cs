using LibMailSender.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibMailSender.Service
{
    public class DebugMailService : IMailService
    {
        public IMailSender GetSender(string Server, int Port, bool SSl, string Login, string Password)
        {
            return new DebugMailSender(Server, Port, SSl, Login, Password);
        }

        private class DebugMailSender : IMailSender
        {
            private readonly string _Address;
            private readonly int _Port;
            private readonly bool _SSL;
            private readonly string _Login;
            private readonly string _Password;

            public DebugMailSender(string Address, int Port, bool SSL, string Login, string Password)
            {
                _Address = Address;
                _Port = Port;
                _SSL = SSL;
                _Login = Login;
                _Password = Password;
            }
            public void Send(string SenderAddress, string RecipientAddress, string Subject, string Body)
            {
                Debug.WriteLine("Отправка почты через сервер {0}:{1} SSL:{2} (Login:{3}; Pass:{4})",_Address,_Port,_SSL,_Login,_Password);
                Debug.WriteLine("Сообщение от {0} к {1}:\r\n{2}\r\n{3}",
                    SenderAddress,RecipientAddress,Subject,Body);
            }

            public void Send(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body)
            {
                foreach (var recipient_address in RecipientsAddresses)
                    Send(SenderAddress, recipient_address, Subject, Body);
            }

            public async Task SendAsync(string SenderAddress, string RecipientAddress, string Subject, string Body, CancellationToken Cancel = default)
            {
                Send(SenderAddress, RecipientAddress, Subject, Body);
            }

            public async Task SendAsync(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body, IProgress<double> Progress = null, CancellationToken Cancel = default)
            {
                var tasks = RecipientsAddresses
               .Select(recipient_address => SendAsync(SenderAddress, recipient_address, Subject, Body, Cancel));
            }

            public void SendParallel(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body)
            {
                foreach (var recipient_address in RecipientsAddresses)
                    ThreadPool.QueueUserWorkItem(o => Send(SenderAddress, recipient_address, Subject, Body));
            }

            public async  Task SendParallelAsync(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body, CancellationToken Cancel = default)
            {
                var tasks = RecipientsAddresses
               .Select(recipient_address => SendAsync(SenderAddress, recipient_address, Subject, Body, Cancel));

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
        }
    }
}
