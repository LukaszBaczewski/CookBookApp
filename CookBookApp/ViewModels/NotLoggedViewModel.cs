
using CookBookApp.Commands;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace CookBookApp.ViewModels
{
    public class NotLoggedViewModel : ViewModelBase
    {
        #region Sign Up command
        public ICommand SignUpCommand { get { return new RelayCommand(() => executeSignUpCommand()); } }
        public void executeSignUpCommand()
        {
            Messenger.Default.Send(new NotificationMessage("Open SignUpWindow"));
        }
        #endregion

        #region Log In Command

        public ICommand LogInCommand { get { return new RelayCommand(() => ExecuteLogInCommand()); } }

        public void ExecuteLogInCommand()
        {
            Messenger.Default.Send(new NotificationMessage("Open LogInWindow"));
        }
        #endregion
    }
}
