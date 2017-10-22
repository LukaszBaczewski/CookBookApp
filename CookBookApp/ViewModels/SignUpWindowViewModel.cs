using CookBookApp.Commands;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace CookBookApp.ViewModels
{
    public class SignUpWindowViewModel : ViewModelBase
    {

        public event EventHandler OnRequestClose;

        #region Properties
        private Account _account;
        public Account Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        private User _user;
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        private Login _login;
        public Login Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _password;
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

        public string PasswordCheck = "";
        public string EmailCheck = "";
        public string UserNameCheck = "";
        #endregion

        #region Constructor

        public SignUpWindowViewModel()
        {
            Account = new Account();
            User = new User();
            Login = new Login();
        }
        #endregion

        #region Sign Up Command
        public ICommand SignUpCommand { get { return new RelayCommand(() => executeSignUpCommand()); } }

        public void executeSignUpCommand()
        {

            if (DoesEmailExists())
            {
                MessageBox.Show(EmailCheck);
                return;
            }
            if(DoesUserNameExists())
            {
                MessageBox.Show(UserNameCheck);
                return;
            }

            Login.PasswordHash = PasswordSecurity.PasswordStorage.CreateHash(Password);
            User.UserName = Login.UserName;
            Account.Name = Login.UserName;
            User.FirstName = "NoName";
            User.LastName = "NoName";
            User.Logins.Add(Login);
            Account.Users.Add(User);

            using (var context = new RecipeDBEntities1())
            {
                context.Accounts.Add(Account);
                context.SaveChanges();
            }

            //var account = new Account();
            //account.ID = Account.ID;
            //account.Name = Account.Name;
            Messenger.Default.Send<Account>(Account);
            OnRequestClose(this, new EventArgs());
        }

        public bool DoesEmailExists()
        {
            using (var context = new RecipeDBEntities1())
            {
                bool emailExists = context.Logins.Any(email => email.EmailAdress == Login.EmailAdress);

                if (emailExists)
                {
                    EmailCheck = "User with that email already exists";
                    return true;
                }
                return false;
            }
        }

        public bool DoesUserNameExists()
        {
            using (var context = new RecipeDBEntities1())
            {
                bool userExists = context.Logins.Any(login => login.UserName == Login.UserName);

                if (userExists)
                {
                    UserNameCheck = "User with that name already exists";
                    return true;
                }
                return false;
            }
        }

        #endregion

        #region Log In Command

        public ICommand LogInCommand { get { return new RelayCommand(() => ExecuteLogInCommand()); } }

        public void ExecuteLogInCommand()
        {
            Messenger.Default.Send(new NotificationMessage("Open LogInWindow"));
            OnRequestClose(this, new EventArgs());
        }
        #endregion
    }
}
