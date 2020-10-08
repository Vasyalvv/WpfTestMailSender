using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMailSender.Models.Base;

namespace LibMailSender.Models
{
    public class Message:Entity
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHTML { get; set; }
    }
}
