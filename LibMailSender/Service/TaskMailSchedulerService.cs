using LibMailSender.Interfaces;
using LibMailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibMailSender.Service
{
    public class TaskMailSchedulerService : IMailSchedulerService
    {
        private readonly IStore<SchedulerTask> _TaskStore;
        private Task _SchedulerTask;
        private readonly CancellationTokenSource _TaskCancellation = new CancellationTokenSource();
        private IMailService _MailService;

        public TaskMailSchedulerService(IStore<SchedulerTask> TaskStore, IMailService MailService)
        {
            _TaskStore = TaskStore;
            _MailService = MailService;
        }

        public void Start()
        {
            _SchedulerTask = Task.Factory.StartNew(SchedulerTaskMethodAsync, TaskCreationOptions.LongRunning);
        }

        public void Stop() => _TaskCancellation.Cancel();

        private async Task SchedulerTaskMethodAsync()
        {
            var cancel = _TaskCancellation.Token;
            while (true)
            {
                cancel.ThrowIfCancellationRequested();
                var next_task = _TaskStore.GetAll()
                    .OrderBy(t => t.Time)
                    .FirstOrDefault(t => t.Time > DateTime.Now);

                if (next_task is null) break;

                var sleep_time = next_task.Time - DateTime.Now;

                if (sleep_time.TotalMilliseconds > 0)
                    await Task.Delay(sleep_time, cancel);

                cancel.ThrowIfCancellationRequested();
                await Execute(next_task);
            }
        }

        private async Task Execute(SchedulerTask task)
        {
            var server = task.Server;
            var sender = _MailService.GetSender(server.Address, server.Port,
                server.UseSSL, server.Login, server.Password);
            await sender.SendAsync(
                task.Sender.Address, task.Recipients.Select(r => r.Address),
                task.Message.Subject, task.Message.Body);
        }

        public void AddTask(DateTime time ,Sender sender, IEnumerable<Recipient> recipients, Server server, Message message)
        {
            Stop();

            _TaskStore.Add(new SchedulerTask
            {
                Message=message,
                Recipients=recipients.ToArray(),
                Server=server,
                Sender=sender,
                Time= time
            });

            Start();
        }
    }
}
