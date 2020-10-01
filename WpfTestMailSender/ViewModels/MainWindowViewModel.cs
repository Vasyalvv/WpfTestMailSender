using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using WpfTestMailSender.Infrastructure.Commands;
using WpfTestMailSender.ViewModels.Base;

namespace WpfTestMailSender.ViewModels
{
    class MainWindowViewModel:ViewModel
    {
        private string _Title = "Тестовое окно";

        public string Title {
            get => _Title;
            set => Set(ref _Title, value);
            //set
            //{
            //    if (_Title == value) return;
            //    _Title = value;
            //    OnPropertyChanged("Title");
            //}
        }

        public DateTime CurrentTime => DateTime.Now;
private bool _TimerEnabled = true;

        public bool TimerEnabled
        {
            get => _TimerEnabled;
            set
            {
                if (!Set(ref _TimerEnabled, value)) return;
                _Timer.Enabled = value;
            }
        }


        private readonly Timer _Timer;

        private ICommand _ShowDialogCommand;

        public ICommand ShowDialogCommand => _ShowDialogCommand
            ??  new LambdaCommand(OnShowDialogCommandExecute);

        private void OnShowDialogCommandExecute(object obj)
        {
            MessageBox.Show("Hello World!");
        }

        public MainWindowViewModel()
        {
            _Timer = new Timer(100);
            _Timer.Elapsed += _Timer_Elapsed;
            _Timer.AutoReset = true;
            _Timer.Enabled = true;
        }

        private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }
    }
}
