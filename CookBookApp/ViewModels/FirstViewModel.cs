using CookBookApp;
using CookBookApp.Commands;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CookBookApp.ViewModels
{
    public class FirstViewModel : ViewModelBase
    {
        #region Properties

        private ViewModelLocator _locator;
        public LoggedUserViewModel LoggedUserVM;
        bool isLogged = false;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get
            {
                return LoggedAccount.Name;
            }
            set
            {
                LoggedAccount.Name = value;
                OnPropertyChanged();
            }
        }


        public Account _loggedAccount;
        public Account LoggedAccount
        {
            get
            {
                return _loggedAccount;
            }
            set
            {
                _loggedAccount = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        [PreferredConstructor]
        public FirstViewModel(Account LA)
        {
            _locator = new ViewModelLocator();
            LoggedAccount = new Account();
            if (LA != null)
            {
                LoggedAccount = LA;
                CurrentViewModel = new LoggedUserViewModel(LoggedAccount);
                isLogged = true; 
            }
            else
            {
                CurrentViewModel = new NotLoggedViewModel();
            }

            Messenger.Default.Register<Account>(this, "loggedacc", (account) =>
            {
                LoggedAccount = account;
                Name = LoggedAccount.Name;
                isLogged = true;
                CurrentViewModel = new LoggedUserViewModel(account);           
            });

            Messenger.Default.Register<NotificationMessage>(this, "FirstViewModel", (msg) =>
            {
                CurrentViewModel = new NotLoggedViewModel();
                isLogged = false;
            });
        }

        
        public FirstViewModel()
        {
            _locator = new ViewModelLocator();
            LoggedAccount = new Account();
           
            CurrentViewModel = new NotLoggedViewModel();

            Messenger.Default.Register<Account>(this, "loggedacc", (account) =>
            {
                LoggedAccount = account;
                Name = LoggedAccount.Name;
                isLogged = true;
                CurrentViewModel = new LoggedUserViewModel(account);
            });

            Messenger.Default.Register<NotificationMessage>(this, "FirstViewModel", (msg) =>
            {
                CurrentViewModel = new NotLoggedViewModel();
                isLogged = false;
            });
        }
        #endregion

        #region Create new recipe command
        public void executeCreateNewRecipeCommand()
        {
            if (isLogged)
                _locator.Main.CurrentViewModel = new CreateNewRecipeViewModel(LoggedAccount);
            else
                Messenger.Default.Send(new NotificationMessage("Open LogInWindow"));
        }

        public ICommand CreateNewRecipeCommand { get { return new RelayCommand(() => executeCreateNewRecipeCommand()); } }
        #endregion

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

        #region Search Recipe Command

        public ObservableCollection<Recipe> _results;
        public ObservableCollection<Recipe> Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
                OnPropertyChanged();
            }
        }

        private string _searchKeyword;
        public string SearchKeyword
        {
            get
            {
                return _searchKeyword;
            }
            set
            {
                _searchKeyword = value;
                OnPropertyChanged();
            }
        }
        public ICommand SearchRecipeCommand { get { return new RelayCommand(() => ExecuteSearchRecipeCommand()); } }

        public void ExecuteSearchRecipeCommand()
        {
            if (SearchKeyword == "")
                return;
            SearchRecipes();
            _locator.Main.CurrentViewModel = new SearchResultsViewModel(Results, LoggedAccount);
        }

        public void SearchRecipes()
        {
            using (var context = new RecipeDBEntities1())
            {
                var results = context.Recipes
                  .Where(r => r.Name.Contains(SearchKeyword))
                  .OrderBy(r => r.Name);
                Results = new ObservableCollection<Recipe>(results);
            }
        }
        #endregion

    }
}