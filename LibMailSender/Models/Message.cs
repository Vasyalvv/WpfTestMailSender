using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMailSender.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHTML { get; set; }
    }
}
