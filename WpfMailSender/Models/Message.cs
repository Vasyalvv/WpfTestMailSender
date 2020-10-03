using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender.Models
{
    public class Message
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHTML { get; set; }
    }
}
