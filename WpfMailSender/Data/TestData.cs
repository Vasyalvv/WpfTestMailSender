using System;
using System.Linq;
using WpfMailSender.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibMailSender.Service;
using System.Xml.Serialization;
using System.IO;

namespace WpfMailSender.Data
{
    public class TestData
    {
        public List<Sender> Senders { get; } = Enumerable.Range(1, 10)
            .Select(i => new Sender
            {
                Name = $"Отправитель {i}",
                Address = $"sender_{i}@server.ru"
            }
            ).ToList();

        public List<Recipient> Recipients { get; } = Enumerable.Range(1, 10)
            .Select(i => new Recipient
            {
                Name = $"Получатель {i}",
                Address = $"recipient_{i}@server.ru"
            }
            ).ToList();

        public List<Server> Servers { get; } = Enumerable.Range(1, 10)
            .Select(i => new Server
            {
                Address = $"smtp.server{i}.com",
                Login = $"Login-{i}",
                Password = TextEncoder.Encode($"Password-{i}"),
                UseSSL = i % 2 == 0
            }
            ).ToList();

        public List<Message> Messages { get; } = Enumerable.Range(1, 20)
            .Select(i => new Message
            {
                Subject = $"Сообщение {i}",
                Body = $"Текст сообщения {i}",
                IsHTML = i % 2 == 0
            }).ToList();

        public TestData()
        {

        }

        public TestData(IEnumerable<Server> servers, IEnumerable<Sender> senders,
            IEnumerable<Recipient> recipients, IEnumerable<Message> messages)
        {
            Servers.Clear();
            Servers.AddRange(servers);

            Senders.Clear();
            Senders.AddRange(senders);

            Recipients.Clear();
            Recipients.AddRange(recipients);

            Messages.Clear();
            Messages.AddRange(messages);
        }

        public static TestData LoadFromXML(string FileName)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using (var file = File.OpenText(FileName))
                return (TestData)serializer.Deserialize(file);
        }

        public void SaveToFile(string FileName)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using (var file = File.Create(FileName))
                serializer.Serialize(file, this);
        }
    }
}
