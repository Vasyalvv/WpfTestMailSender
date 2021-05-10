using LibMailSender.Interfaces;
using LibMailSender.Models;
using LibMailSender.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfMailSender.Data;
using WpfMailSender.Data.Stores.InDB;
using WpfMailSender.Data.Stores.InMemory;
using WpfMailSender.ViewModels;

namespace WpfMailSender
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _Hosting;

        public static IHost Hosting => _Hosting
            ?? Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
            .ConfigureHostConfiguration(cfg => cfg.AddJsonFile("appsettings.json", true, true))
            .ConfigureAppConfiguration(cfg=>cfg.AddJsonFile("appsettings.json",true,true))
            .ConfigureLogging(log=>
            log.AddConsole()
            .AddDebug())
            .ConfigureServices(ConfigureServices)
            .Build();

        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<WpfMailSenderWindowViewModel>();
            #if DEBUG
                services.AddTransient<IMailService, DebugMailService>();
#else
                services.AddTransient<IMailService, SmtpMailService>();
#endif

            services.AddSingleton<IEncryptorService, Rfc2898Encryptor>();

            services.AddDbContext<MailSenderDB>(opt => opt.UseSqlServer(host.Configuration.GetConnectionString("Default")));
            services.AddTransient<MailSenderDBInitializer>();

            //var memory_store = new InMemoryDataStorage();
            //services.AddSingleton<IServerStorage>(memory_store);
            //services.AddSingleton<ISenderStorage>(memory_store);
            //services.AddSingleton<IRecipientStorage>(memory_store);
            //services.AddSingleton<IMessageStorage>(memory_store);

            //const string dataFileName = "MailSenderStorage.xml";
            //var FileStorage = new DataStorageInXmlFile(dataFileName);
            //services.AddSingleton<IServerStorage>(FileStorage);
            //services.AddSingleton<ISenderStorage>(FileStorage);
            //services.AddSingleton<IRecipientStorage>(FileStorage);
            //services.AddSingleton<IMessageStorage>(FileStorage);

            services.AddSingleton<IStore<Recipient>, RecipientStoreInDB>();
            //services.AddSingleton<IStore<Recipient>, RecipientStoreInMemory>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services.GetRequiredService<MailSenderDBInitializer>().Initialize();
            base.OnStartup(e);
        }
    }
}
