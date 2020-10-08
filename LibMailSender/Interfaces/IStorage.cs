using LibMailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMailSender.Interfaces
{
    public interface IStorage<T>
    {
        ICollection<T> Items { get; }

        void Load();

        void SaveChanges();
    }

    public interface IServerStorage : IStorage<Server> { }
    public interface ISenderStorage : IStorage<Sender> { }
    public interface IRecipientStorage : IStorage<Recipient> { }
    public interface IMessageStorage : IStorage<Message> { }
}
