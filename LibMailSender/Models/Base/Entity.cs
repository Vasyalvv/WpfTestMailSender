using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMailSender.Models.Base
{
    public abstract class Entity
    {
        public int Id { get; set; }
    }

    public abstract class NamedEntity:Entity
    {
        public virtual string Name { get; set; }
    }

    public abstract class Person : NamedEntity
    {
        public virtual string Address { get; set; }
    }
}
