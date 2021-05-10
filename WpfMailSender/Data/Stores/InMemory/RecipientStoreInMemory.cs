using LibMailSender.Interfaces;
using LibMailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender.Data.Stores.InMemory
{
    class RecipientStoreInMemory : IStore<Recipient>
    {
        public Recipient Add(Recipient Item)
        {
            if (TestData.Recipients.Contains(Item)) return Item;
            Item.Id = TestData.Recipients.DefaultIfEmpty().Max(r => r.Id) + 1;            
            TestData.Recipients.Add(Item);
            return Item;
        }

        public void Delete(int Id)
        {
            var Item = GetById(Id);
            if (Item is null) return;
            TestData.Recipients.Remove(Item);
        }

        public IEnumerable<Recipient> GetAll() => TestData.Recipients;

        public Recipient GetById(int Id) => GetAll().FirstOrDefault(r => r.Id == Id);

        public void Update(Recipient item)
        {
            
        }
    }
}
