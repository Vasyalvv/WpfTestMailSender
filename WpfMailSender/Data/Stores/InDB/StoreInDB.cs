using LibMailSender.Interfaces;
using LibMailSender.Models.Base;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender.Data.Stores.InDB
{
    public class StoreInDB<T>:IStore<T> where T:Entity
    {
        private readonly MailSenderDB _db;
        private readonly DbSet<T> _Set;

        public StoreInDB(MailSenderDB db)
        {
            _db = db;
            _Set = _db.Set<T>();
        }

        public T Add(T Item)
        {
            _db.Entry(Item).State = EntityState.Added;
            _db.SaveChanges();
            return Item;
        }

        public void Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return;
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public IEnumerable<T> GetAll() => _Set.ToArray();


        public T GetById(int Id) => _Set.FirstOrDefault(r => r.Id == Id);

        public void Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

    }
}
