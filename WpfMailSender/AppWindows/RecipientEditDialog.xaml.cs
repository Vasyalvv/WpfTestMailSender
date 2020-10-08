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
    /// Логика взаимодействия для RecipientEditDialog.xaml
    /// </summary>
    public partial class RecipientEditDialog : Window
    {
        private RecipientEditDialog()
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
            var window = new RecipientEditDialog
            {
                Title = Title,
                RecipientName = { Text = Name },
                RecipientAddress = { Text = Address },
                RecipientDescription = { Text = Description },
                Owner = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(windowq => windowq.IsActive)
            };

            if (window.ShowDialog() != true) return false;

            Name = window.RecipientName.Text;
            Address = window.RecipientAddress.Text;
            Description = window.RecipientDescription.Text;

            return true;
        }

        public static bool Create(
            out string Name, out string Address,
            out string Description)
        {
            Name = null;
            Address = null;
            Description = null;

            return ShowDialog("Создать Получателя", ref Name, ref Address, ref Description);
        }

        public static bool Edit(
            ref string Name, ref string Address,
            ref string Description)
        {
            return ShowDialog("Редактировать получателя", ref Name, ref Address, ref Description);
        }

    }
}
