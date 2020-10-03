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
using LibMailSender.Models;
using LibMailSender;
using System.Net.Mail;

namespace WpfMailSender
{
    /// <summary>
    /// Логика взаимодействия для WpfMailSenderWindow.xaml
    /// </summary>
    public partial class WpfMailSenderWindow : Window
    {
        public WpfMailSenderWindow()
        {
            InitializeComponent();
        }

    }
}
