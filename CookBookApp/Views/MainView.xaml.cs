using CookBookApp.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;

namespace CookBookApp.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void ReplyToMessage(NotificationMessage msg)
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
            else if(msg.Notification == "Open LogInWindow")
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
