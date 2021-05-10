﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibMailSender.Interfaces
{
    public interface IMailService
    {
        IMailSender GetSender(string Server, int Port, bool SSl, string Login, string Password);
    }


    public interface IMailSender
    {
        void Send(string SenderAddress, string RecipientAddress, string Subject, string Body);

        void Send(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body);

        void SendParallel(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body);

        Task SendAsync(string SenderAddress, string RecipientAddress, string Subject, string Body, CancellationToken Cancel = default);

        Task SendAsync(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body,
            IProgress< double> Progress = null, CancellationToken Cancel = default);

        Task SendParallelAsync(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body,
            CancellationToken Cancel = default);
    }
}
