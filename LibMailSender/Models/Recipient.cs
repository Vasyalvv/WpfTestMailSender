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
        public string Description { get; set; }
    }
}
