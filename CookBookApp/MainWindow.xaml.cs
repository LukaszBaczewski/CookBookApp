using CookBookApp.ViewModels;
using GalaSoft.MvvmLight.Messaging;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CookBookApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<NotificationMessage>(this, ReplyToMessage);
        }


        public void ReplyToMessage(NotificationMessage msg)
        {
            if (msg.Notification == "Open SignUpWindow")
            {
                Window win = new Window();
                var vm = new SignUpWindowViewModel();
                win.Content = vm;
                win.Width = 700;
                win.Height = 600;
                win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                vm.OnRequestClose += (s, e) => win.Close();
                win.Show();
            }
            else if (msg.Notification == "Open LogInWindow")
            {
                Window win = new Window();
                var vm = new LogInViewModel();
                win.Content = vm;
                win.Width = 700;
                win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                vm.OnRequestClose += (s, e) => win.Close();
                /*vm.OnRequestClose += (s, e) => *//*Messenger.Default.Unregister<NotificationMessage>(this);*/
                win.Show();
            }
        }
    }

}
