using LibMailSender.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfMailSender.Data;
using WpfMailSender.Infrastructure.Commands;
using WpfMailSender.Models;
using WpfMailSender.ViewModels.Base;

namespace WpfMailSender.ViewModels
{
    class WpfMailSenderWindowViewModel : ViewModel
    {
        #region Свойства/Поля


        public StatisticViewModel Statistic { get; } = new StatisticViewModel();

        private string _Title = "Тестовое окно";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private ObservableCollection<Server> _Servers;
        private ObservableCollection<Sender> _Senders;
        private ObservableCollection<Recipient> _Recipients;
        private ObservableCollection<Message> _Messages;

        public ObservableCollection<Server> Servers
        {
            get => _Servers;
            set => Set(ref _Servers, value);
        }
        public ObservableCollection<Sender> Senders
        {
            get => _Senders;
            set => Set(ref _Senders, value);
        }
        public ObservableCollection<Recipient> Recipients
        {
            get => _Recipients;
            set => Set(ref _Recipients, value);
        }
        public ObservableCollection<Message> Messages
        {
            get => _Messages;
            set => Set(ref _Messages, value);
        }


        private Server _SelectedServer;
        public Server SelectedServer { get => _SelectedServer; set => Set(ref _SelectedServer, value); }

        private Sender _SelectedSender;
        public Sender SelectedSender { get => _SelectedSender; set => Set(ref _SelectedSender, value); }

        private Message _SelectedMessage;
        public Message SelectedMessage { get => _SelectedMessage; set => Set(ref _SelectedMessage, value); }

        private Recipient _SelectedRecipient;
        public Recipient SelectedRecipient { get => _SelectedRecipient; set => Set(ref _SelectedRecipient, value); }

        private readonly IMailService _MailService;
        private readonly string __DataFileName = "DataLists.xml";
        #endregion


        #region Команды

        #region CreateNewServerCommand

        private ICommand _CreateNewServerCommand;
        public ICommand CreateNewServerCommand => _CreateNewServerCommand
            ?? new LambdaCommand(OnCreateNewServerCommandExecute, CanCreateNewServerCommandExecute);

        private bool CanCreateNewServerCommandExecute(object arg) => true;

        private void OnCreateNewServerCommandExecute(object obj)
        {
            MessageBox.Show("Создание нового сервера");
        }
        #endregion

        #region EditServerCommand

        private ICommand _EditServerCommand;
        public ICommand EditServerCommand => _EditServerCommand
            ?? new LambdaCommand(OnEditServerCommandExecute, CanEditServerCommandExecute);

        private bool CanEditServerCommandExecute(object arg) => arg is Server || SelectedServer != null;

        private void OnEditServerCommandExecute(object obj)
        {
            Server server = obj as Server ?? SelectedServer;
            if (server is null) return;
            MessageBox.Show("Редактирование сервера");
        }
        #endregion

        #region DeleteServerCommand

        private ICommand _DeleteServerCommand;


        public ICommand DeleteServerCommand => _DeleteServerCommand
            ?? new LambdaCommand(OnDeleteServerCommandExecute, CanDeleteServerCommandExecute);

        private bool CanDeleteServerCommandExecute(object arg) => arg is Server || SelectedServer != null;

        private void OnDeleteServerCommandExecute(object obj)
        {
            Server server = obj as Server ?? SelectedServer;
            if (server is null) return;

            Servers.Remove(server);
            SelectedServer = Servers.FirstOrDefault();
        }
        #endregion

        #region SendMailCommand

        private ICommand _SendMailCommand;
        public ICommand SendMailCommand => _SendMailCommand
            ?? new LambdaCommand(OnSendMailCommandExecute, CanSendMailCommandExecute);

        private bool CanSendMailCommandExecute(object arg)
        {
            if (SelectedServer is null) return false;
            if (SelectedSender is null) return false;
            if (SelectedRecipient is null) return false;
            if (SelectedMessage is null) return false;
            return true;
        }

        private void OnSendMailCommandExecute(object obj)
        {
            Server server = SelectedServer;
            Sender sender = SelectedSender;
            Recipient recipient = SelectedRecipient;
            Message message = SelectedMessage;

            if (SelectedServer is null) return;
            if (SelectedSender is null) return;
            if (SelectedRecipient is null) return;
            if (SelectedMessage is null) return;

            IMailSender mailSender = _MailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);
            mailSender.Send(sender.Address, recipient.Address, message.Subject, message.Body);

            Statistic.MessageSend();
        }
        #endregion

        #region LoadDataCommand

        private ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand
            ?? new LambdaCommand(OnLoadDataCommand);

        private void OnLoadDataCommand(object obj)
        {
            var data = File.Exists(__DataFileName)
                ? TestData.LoadFromXML(__DataFileName)
                : new TestData();
            Servers = new ObservableCollection<Server>(data.Servers);
            Senders = new ObservableCollection<Sender>(data.Senders);
            Recipients = new ObservableCollection<Recipient>(data.Recipients);
            Messages = new ObservableCollection<Message>(data.Messages);
        }
        #endregion

        #region SaveDataCommand

        private ICommand _SaveDataCommand;

        public ICommand SaveDataCommand => _SaveDataCommand
            ?? new LambdaCommand(OnSaveDataCommandExecuted);

        private void OnSaveDataCommandExecuted(object obj)
        {
            TestData data = new TestData(Servers,Senders,Recipients,Messages);

            data.SaveToFile(__DataFileName);
        }
        #endregion

        #endregion

        public WpfMailSenderWindowViewModel(IMailService MailService)
        {

            _MailService = MailService;
        }
    }
}
