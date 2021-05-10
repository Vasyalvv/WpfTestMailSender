using LibMailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMailSender.Interfaces
{
    public interface IMailSchedulerService
    {
        void Start();

        void Stop();

        void AddTask(DateTime time, Sender sender, IEnumerable<Recipient> recipients, Server server, Message message);
    }
}
