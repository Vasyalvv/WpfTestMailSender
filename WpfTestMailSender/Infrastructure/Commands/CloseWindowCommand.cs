using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfTestMailSender.Infrastructure.Commands.Base;

namespace WpfTestMailSender.Infrastructure.Commands
{
    class CloseWindowCommand : Command
    {
        protected override void Execute(object p)
        {
            Window window = p as Window;

            if (window is null)
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(W => W.IsFocused);

            if (window is null)
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(W => W.IsActive);

            window?.Close();
        }

    }
}
