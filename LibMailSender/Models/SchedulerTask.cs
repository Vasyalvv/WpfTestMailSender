using LibMailSender.Models.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibMailSender.Models
{
    public class SchedulerTask : Entity
    {
        public DateTime Time { get; set; }
        public Server Server { get; set; }
        public Sender Sender { get; set; }
        public ICollection<Recipient> Recipients { get; set; }
        public Message Message { get; set; }

    }
}