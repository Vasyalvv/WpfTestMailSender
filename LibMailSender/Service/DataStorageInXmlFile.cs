using LibMailSender.Interfaces;
using LibMailSender.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibMailSender.Service
{
    public class DataStorageInXmlFile : IServerStorage, ISenderStorage,
        IRecipientStorage, IMessageStorage
    {
        public class DataStructure
        {
            public List<Server> Servers { get; set; } = new List<Server>();

            public List<Sender> Senders { get; set; } = new List<Sender>();

            public List<Recipient> Recipipients { get; set; } = new List<Recipient>();

            public List<Message> Messages { get; set; } = new List<Message>();

        }

        private readonly string _FileName;

        public DataStorageInXmlFile(string FileName) => _FileName = FileName;

        private DataStructure Data { get; set; } = new DataStructure();


        ICollection<Sender> IStorage<Sender>.Items => Data.Senders;

        ICollection<Recipient> IStorage<Recipient>.Items => Data.Recipipients;

        ICollection<Message> IStorage<Message>.Items => Data.Messages;

        ICollection <Server> IStorage<Server>.Items => Data.Servers;

        public void Load()
        {
            if (!File.Exists(_FileName))
            {
                Data = new DataStructure();
                return;
            }

            using (var file = File.OpenText(_FileName))
            {
                if (file.BaseStream.Length == 0)
                {
                    Data = new DataStructure();
                    return;
                }


                var serializer = new XmlSerializer(typeof(DataStructure));
                Data = (DataStructure)serializer.Deserialize(file);
            }
        }

        public void SaveChanges()
        {
            using (var file = File.CreateText(_FileName))
            {
                var serializer = new XmlSerializer(typeof(DataStructure));
                serializer.Serialize(file, Data);
            }
        }
    }
}
