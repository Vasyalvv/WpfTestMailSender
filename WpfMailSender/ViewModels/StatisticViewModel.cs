using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMailSender.ViewModels.Base;

namespace WpfMailSender.ViewModels
{
    class StatisticViewModel:ViewModel
    {
        private int _SendMessagesCount;

        public int SendMessagesCount { get => _SendMessagesCount; set => Set(ref _SendMessagesCount,value); }

        public void MessageSend() => SendMessagesCount++;
    }
}
