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
    /// Логика взаимодействия для SenderEditDialog.xaml
    /// </summary>
    public partial class SenderEditDialog : Window
    {
        private SenderEditDialog()
        {
            InitializeComponent();
        }

        private void UniformGrid_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }

        public static bool ShowDialog(
            string Title, ref string Name,
            ref string Address,
            ref string Description)
        {
            var window = new SenderEditDialog
            {
                Title = Title,
                SenderName = { Text = Name },
                SenderAddress = { Text = Address },
                SenderDescription = { Text = Description },
                Owner = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(windowq => windowq.IsActive)
            };

            if (window.ShowDialog() != true) return false;

            Name = window.SenderName.Text;
            Address = window.SenderAddress.Text;
            Description = window.SenderDescription.Text;

            return true;
        }

        public static bool Create(
            out string Name, out string Address,
            out string Description)
        {
            Name = null;
            Address = null;
            Description = null;

            return ShowDialog("Создать отправителя", ref Name, ref Address, ref Description);
        }

        public static bool Edit(
            ref string Name, ref string Address,
            ref string Description)
        {
            return ShowDialog("Редактировать отправителя", ref Name, ref Address, ref Description);
        }

    }
}
