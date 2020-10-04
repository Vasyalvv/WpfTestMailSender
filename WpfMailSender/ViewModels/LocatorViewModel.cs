using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender.ViewModels
{
    class LocatorViewModel
    {
        public WpfMailSenderWindowViewModel WpfMailSenderWindowModel => App.Services.GetRequiredService<WpfMailSenderWindowViewModel>();
    }
}
