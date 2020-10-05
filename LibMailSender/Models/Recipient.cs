using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMailSender.Models.Base;

namespace LibMailSender.Models
{
    public class Recipient:Person
    {
        public string Description {
            get=>base.Name;
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                if (value == " ")
                    throw new ArgumentException("Имя не может быть пустой строкой", nameof(value));

                if (value == "QWE")
                    throw new ArgumentException("Запрещено вводить QWE", nameof(value));
                base.Name = value;
            }
        }
    }
}
