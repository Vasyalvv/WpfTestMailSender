using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMailSender.ViewModels.Base;

namespace WpfMailSender.ViewModels
{
    class WpfMailSenderWindowViewModel:ViewModel
    {
        private string _Title = "Тестовое окно";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
    }
}
