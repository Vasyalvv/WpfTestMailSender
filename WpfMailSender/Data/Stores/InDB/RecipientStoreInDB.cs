using LibMailSender.Interfaces;
using LibMailSender.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender.Data.Stores.InDB
{
    public class RecipientsStoreInDB : StoreInDB<Recipient>
    {
        public RecipientsStoreInDB(MailSenderDB db) : base(db) { }
    }

    public class ServersStoreInDB : StoreInDB<Server>
    {
        public ServersStoreInDB(MailSenderDB db) : base(db) { }
    }

    public class SendersStoreInDB : StoreInDB<Sender>
    {
        public SendersStoreInDB(MailSenderDB db) : base(db) { }
    }

    public class MessagesStoreInDB : StoreInDB<Message>
    {
        public MessagesStoreInDB(MailSenderDB db) : base(db) { }
    }

    public class SchedulerTasksStoreInDB : StoreInDB<SchedulerTask>
    {
        public SchedulerTasksStoreInDB(MailSenderDB db) : base(db) { }
    }
}
