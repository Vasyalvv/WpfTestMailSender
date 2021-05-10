using LibMailSender.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMailSender.Interfaces
{
    public interface IStore<T> where T:Entity
    {
        IEnumerable<T> GetAll();

        T GetById(int Id);
        T Add(T Item);
        void Update(T item);
        void Delete(int Id);
    }
}
