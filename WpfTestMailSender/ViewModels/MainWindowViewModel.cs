using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTestMailSender.ViewModels.Base;

namespace WpfTestMailSender.ViewModels
{
    class MainWindowViewModel:ViewModel
    {
        private string _Title = "Тестовое окно";

        public string Title {
            get => _Title;
            set
            {
                if (_Title == value) return;
                _Title = value;
                OnPropertyChanged("Title");
            }
        }
    }
}
