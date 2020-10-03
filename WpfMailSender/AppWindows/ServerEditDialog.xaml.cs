using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfMailSender.AppWindows
{
    /// <summary>
    /// Логика взаимодействия для ServerEditDialog.xaml
    /// </summary>
    public partial class ServerEditDialog : Window
    {
        private ServerEditDialog()
        {
            InitializeComponent();
        }

        private void ServerPort_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(sender is TextBox text_box) || text_box.Text == "") return;
            e.Handled = !int.TryParse(text_box.Text, out _);
        }

        private void UniformGrid_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }

        public static bool ShowDialog(
            string Title, ref string Name,
            ref string Address, ref int Port, ref bool UseSSL,
            ref string Description, ref string Login,
            ref string Password)
        {
            var window = new ServerEditDialog
            {
                Title = Title,
                ServerName = { Text = Name },
                ServerAddress = { Text = Address },
                ServerPort = { Text = Port.ToString() },
                ServerSSL = { IsChecked = UseSSL },
                Login = { Text = Login },
                Password = { Password = Password },
                ServerDescription = { Text = Description },
                Owner = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(windowq => windowq.IsActive)
            };

            if (window.ShowDialog() != true) return false;

            Name = window.ServerName.Text;
            Address = window.ServerAddress.Text;
            Port = int.Parse(window.ServerPort.Text);
            Login = window.Login.Text;
            Password = window.Password.Password;

            return true;
        }

        public static bool Create(
            out string Name, out string Address,
            out int Port, out bool UseSSL,
            out string Description, out string Login,
            out string Password)
        {
            Name = null;
            Address = null;
            Port = 25;
            UseSSL = false;
            Description = null;
            Login = null;
            Password = null;

            return ShowDialog("Создать сервер", ref Name, ref Address,ref Port,
                ref UseSSL, ref Description, ref Login, ref Password);
        }
    }
}
