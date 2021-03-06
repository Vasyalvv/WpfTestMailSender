﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
