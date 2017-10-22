using CookBookApp.Commands;
using GalaSoft.MvvmLight.Messaging;
using PasswordSecurity;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CookBookApp.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {

        public Login MyLogin = new Login();
        public Account MyAccount = new Account();

        public event EventHandler OnRequestClose;

        public string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string _emailOrUserName;
        public string EmailOrUserName
        {
            get
            {
                return _emailOrUserName;
            }
            set
            {
                _emailOrUserName = value;
                OnPropertyChanged();
            }
        }

        #region Constructor
        public LogInViewModel()
        {

        }
        #endregion

        #region Log In Command


        public ICommand LogInCommand { get { return new RelayCommand(() => ExecuteLogInCommand()); } }
        
        public void ExecuteLogInCommand()
        {
            using (var context = new RecipeDBEntities1())
            {
                MyLogin = context.Logins
               .Where(o => o.EmailAdress == EmailOrUserName || o.UserName == EmailOrUserName)
               .FirstOrDefault();

                if (MyLogin == null)
                {
                    MessageBox.Show("wrong username or password");
                    return;
                }

                bool correctPassword = PasswordStorage.VerifyPassword(Password, MyLogin.PasswordHash);

                if (correctPassword)
                {
                    MyAccount = MyLogin.User.Account;
                    Messenger.Default.Send(MyAccount, "loggedacc");
                    OnRequestClose(this, new EventArgs());
                }
            }
        }

        #endregion

        #region Sign Up command
        public ICommand SignUpCommand { get { return new RelayCommand(() => ExecuteSignUpCommand()); } }
        public void ExecuteSignUpCommand()
        {
            Messenger.Default.Send(new NotificationMessage("Open SignUpWindow"));
            OnRequestClose(this, new EventArgs());
        }
        #endregion
    }
}
