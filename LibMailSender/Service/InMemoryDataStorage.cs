using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMailSender.Interfaces;
using LibMailSender.Models;

namespace LibMailSender.Service
{
    public class InMemoryDataStorage : IServerStorage, ISenderStorage,
        IRecipientStorage, IMessageStorage
    {
        public ICollection<Server> Servers { get; set; } = new List<Server>();
        public ICollection<Sender> Senders { get; set; } = new List<Sender>();
        public ICollection<Recipient> Recipients { get; set; }=new List<Recipient>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        ICollection<Sender> IStorage<Sender>.Items => Senders;

        ICollection<Message> IStorage<Message>.Items => Messages;

        ICollection<Recipient> IStorage<Recipient>.Items => Recipients;

        ICollection<Server> IStorage<Server>.Items => Servers;

        public void Load()
        {
            Debug.WriteLine("Вызвана процедура загрузки данных");
            if (Servers is null || Servers.Count == 0)
                Servers = new List<Server>
                {
                    new Server
                    {
                        Id=1,
                        Name="Яндекс",
                        Address="smtp.yandex.ru",
                        Port=465,
                        UseSSL=true,
                        Login="user@yandex.ru",
                        Password="Pass1"
                    },
                    new Server
                    {
                        Id=2,
                        Name="gmail",
                        Address="smtp.gmail.ru",
                        Port=587,
                        UseSSL=true,
                        Login="user@gmail.com",
                        Password="Pass2"
                    }
                };

            if (Senders is null || Senders.Count == 0)
                Senders = new List<Sender>
                {
                    new Sender
                    {
                        Id=1,
                        Name="Иванов",
                        Address="ivanov@server.ru",
                        Description="Почта от Иванова"
                    },
                    new Sender
                    {
                        Id=2,
                        Name="Петров",
                        Address="petrov@server.ru",
                        Description="Почта от Петрова"
                    },
                    new Sender
                    {
                        Id=3,
                        Name="Сидоров",
                        Address="sidorov@server.ru",
                        Description="Почта от Сидорова"
                    }
                };

            if (Recipients is null || Recipients.Count == 0)
                Recipients = new List<Recipient>
                {
                    new Recipient
                    {
                        Id=1,
                        Name="Иванов",
                        Address="ivanov@server.ru",
                        Description="Почта для Иванова"
                    },
                    new Recipient
                    {
                        Id=2,
                        Name="Петров",
                        Address="petrov@server.ru",
                        Description="Почта для Петрова"
                    },
                    new Recipient
                    {
                        Id=3,
                        Name="Сидоров",
                        Address="sidorov@server.ru",
                        Description="Почта для Сидорова"
                    }
                };

            if (Messages is null || Messages.Count == 0)
                Messages = Enumerable
                    .Range(1, 10).
                    Select(i => new Message
                    {
                        Id = i,
                        Subject = $"Сообщение {i}",
                        Body = $"Текст сообщения {i}"
                    })
                    .ToList();
        }

        public void SaveChanges()
        {
            Debug.WriteLine("Вызвана процедура сохранения данных");
        }
    }
}
