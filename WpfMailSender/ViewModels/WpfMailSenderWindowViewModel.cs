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
using LibMailSender.Models;
using WpfMailSender.ViewModels.Base;
using WpfMailSender.AppWindows;

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
        private readonly IServerStorage _ServerStorage;
        private readonly ISenderStorage _SenderStorage;
        private readonly IStore<Recipient> _RecipientStorage;
        private readonly IMessageStorage _MessageStorage;
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
            if (!ServerEditDialog.Create(
                out var name, out var address, out var port,
                out var ssl, out var description,
                out var login, out var password)) return;
            var server = new Server
            {
                Id = Servers.DefaultIfEmpty().Max(s => s?.Id ?? 0) + 1,
                Name = name,
                Address = address,
                Port = port,
                UseSSL = ssl,
                Description = description,
                Login = login,
                Password = password
            };

            _ServerStorage.Items.Add(server);
            Servers.Add(server);
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

            int serverPosition=Servers.IndexOf(server);

            string name, address, description, login, password;
            int port;
            bool ssl;

            name = server.Name;
            address = server.Address;
            description = server.Description;
            login = server.Login;
            password = server.Password;
            port = server.Port;
            ssl = server.UseSSL;

            if(!ServerEditDialog.Edit(
                ref name, ref address, ref port,
                ref ssl, ref description,
                ref login, ref password))return;

            server.Name = name;
            server.Address = address;
            server.Description = description;
            server.Login = login;
            server.Password = password;
            server.Port = port;
            server.UseSSL = ssl;

            Servers.RemoveAt(serverPosition);
            Servers.Insert(serverPosition, server);
            SelectedServer = server;
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

        #region CreateNewSenderCommand

        private ICommand _CreateNewSenderCommand;
        public ICommand CreateNewSenderCommand => _CreateNewSenderCommand
            ?? new LambdaCommand(OnCreateNewSenderCommandExecute, CanCreateNewSenderCommandExecute);

        private bool CanCreateNewSenderCommandExecute(object arg) => true;

        private void OnCreateNewSenderCommandExecute(object obj)
        {
            if (!SenderEditDialog.Create(
                out var name, out var address, out var description)) return;
            var sender = new Sender
            {
                Id = Senders.DefaultIfEmpty().Max(s => s?.Id ?? 0) + 1,
                Name = name,
                Address = address,
                Description = description,
            };

            _SenderStorage.Items.Add(sender);
            Senders.Add(sender);
        }
        #endregion

        #region EditSenderCommand

        private ICommand _EditSenderCommand;
        public ICommand EditSenderCommand => _EditSenderCommand
            ?? new LambdaCommand(OnEditSenderCommandExecute, CanEditSenderCommandExecute);

        private bool CanEditSenderCommandExecute(object arg) => arg is Sender || SelectedSender != null;

        private void OnEditSenderCommandExecute(object obj)
        {
            Sender sender = obj as Sender ?? SelectedSender;
            if (sender is null) return;

            int senderPosition = Senders.IndexOf(sender);

            string name, address, description;

            name = sender.Name;
            address = sender.Address;
            description = sender.Description;

            if (!SenderEditDialog.Edit(
                ref name, ref address, ref description)) return;

            sender.Name = name;
            sender.Address = address;
            sender.Description = description;

            Senders.RemoveAt(senderPosition);
            Senders.Insert(senderPosition, sender);
            SelectedSender = sender;
        }
        #endregion

        #region DeleteSenderCommand

        private ICommand _DeleteSenderCommand;


        public ICommand DeleteSenderCommand => _DeleteSenderCommand
            ?? new LambdaCommand(OnDeleteSenderCommandExecute, CanDeleteSenderCommandExecute);

        private bool CanDeleteSenderCommandExecute(object arg) => arg is Sender || SelectedSender != null;

        private void OnDeleteSenderCommandExecute(object obj)
        {
            Sender sender = obj as Sender ?? SelectedSender;
            if (sender is null) return;

            Senders.Remove(sender);
            SelectedSender = Senders.FirstOrDefault();
        }
        #endregion

        #region CreateNewRecipientCommand

        private ICommand _CreateNewRecipientCommand;
        public ICommand CreateNewRecipientCommand => _CreateNewRecipientCommand
            ?? new LambdaCommand(OnCreateNewRecipientCommandExecute, CanCreateNewRecipientCommandExecute);

        private bool CanCreateNewRecipientCommandExecute(object arg) => true;

        private void OnCreateNewRecipientCommandExecute(object obj)
        {
            if (!RecipientEditDialog.Create(
                out var name, out var address, out var description)) return;
            var recipient = new Recipient
            {
                Id = Recipients.DefaultIfEmpty().Max(s => s?.Id ?? 0) + 1,
                Name = name,
                Address = address,
                Description = description,
            };

            _RecipientStorage.Add(recipient);
            Recipients.Add(recipient);
        }
        #endregion

        #region EditRecipientCommand

        private ICommand _EditRecipientCommand;
        public ICommand EditRecipientCommand => _EditRecipientCommand
            ?? new LambdaCommand(OnEditRecipientCommandExecute, CanEditRecipientCommandExecute);

        private bool CanEditRecipientCommandExecute(object arg) => arg is Recipient || SelectedRecipient != null;

        private void OnEditRecipientCommandExecute(object obj)
        {
            Recipient recipient = obj as Recipient ?? SelectedRecipient;
            if (recipient is null) return;

            int recipientPosition = Recipients.IndexOf(recipient);

            string name, address, description;

            name = recipient.Name;
            address = recipient.Address;
            description = recipient.Description;

            if (!RecipientEditDialog.Edit(
                ref name, ref address, ref description)) return;

            recipient.Name = name;
            recipient.Address = address;
            recipient.Description = description;

            Recipients.RemoveAt(recipientPosition);
            Recipients.Insert(recipientPosition, recipient);
            SelectedRecipient = recipient;
        }
        #endregion

        #region DeleteRecipientCommand

        private ICommand _DeleteRecipientCommand;


        public ICommand DeleteRecipientCommand => _DeleteRecipientCommand
            ?? new LambdaCommand(OnDeleteRecipientCommandExecute, CanDeleteRecipientCommandExecute);

        private bool CanDeleteRecipientCommandExecute(object arg) => arg is Recipient || SelectedRecipient != null;

        private void OnDeleteRecipientCommandExecute(object obj)
        {
            Recipient recipient = obj as Recipient ?? SelectedRecipient;
            if (recipient is null) return;

            Recipients.Remove(recipient);
            SelectedRecipient = Recipients.FirstOrDefault();
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
            ?? new LambdaCommand(OnLoadDataCommandExecute);

        private void OnLoadDataCommandExecute(object obj)
        {
            //var data = File.Exists(__DataFileName)
            //    ? TestData.LoadFromXML(__DataFileName)
            //    : new TestData();
            //Servers = new ObservableCollection<Server>(data.Servers);
            //Senders = new ObservableCollection<Sender>(data.Senders);
            //Recipients = new ObservableCollection<Recipient>(data.Recipients);
            //Messages = new ObservableCollection<Message>(data.Messages);

            _ServerStorage.Load();
            _SenderStorage.Load();
            //_RecipientStorage.Load();
            _RecipientStorage.GetAll();
            _MessageStorage.Load();

            Servers = new ObservableCollection<Server>(_ServerStorage.Items);
            Senders = new ObservableCollection<Sender>(_SenderStorage.Items);
            Recipients = new ObservableCollection<Recipient>(_RecipientStorage.GetAll());
            Messages = new ObservableCollection<Message>(_MessageStorage.Items);
        }
        #endregion

        #region SaveDataCommand

        private ICommand _SaveDataCommand;

        public ICommand SaveDataCommand => _SaveDataCommand
            ?? new LambdaCommand(OnSaveDataCommandExecuted);

        private void OnSaveDataCommandExecuted(object obj)
        {
            //TestData data = new TestData(Servers,Senders,Recipients,Messages);

            //data.SaveToFile(__DataFileName);

            _ServerStorage.SaveChanges();
            _SenderStorage.SaveChanges();
            //_RecipientStorage.SaveChanges();
            //_RecipientStorage.Update();   <-----Доделать
            foreach (var item in _RecipientStorage.GetAll())
            {
                _RecipientStorage.Update(item);
            }
            _MessageStorage.SaveChanges();
        }
        #endregion

        #endregion

        public WpfMailSenderWindowViewModel(IMailService MailService,
            IStore<Recipient> RecipientStorage)
        {

            _MailService = MailService;

            //_ServerStorage = ServerStorage;
            //_SenderStorage = SenderStorage;
            _RecipientStorage = RecipientStorage;
            //_MessageStorage = MessageStorage;

            if (Servers is null) Servers = new ObservableCollection<Server>();
            if (Senders is null) Senders = new ObservableCollection<Sender>();
            if (Recipients is null) Recipients = new ObservableCollection<Recipient>(RecipientStorage.GetAll());
            if (Messages is null) Messages = new ObservableCollection<Message>();
        }
    }
}
