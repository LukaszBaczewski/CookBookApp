
using CookBookApp.Commands;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CookBookApp.ViewModels
{
    public class LoggedUserViewModel : ViewModelBase
    {
        #region Properties
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

        public ViewModelLocator _locator;
        #endregion

        #region Constructor
        public LoggedUserViewModel(Account loggedAccount)
        {
            LoggedAccount = new Account();
            LoggedAccount = loggedAccount;
            _locator = new ViewModelLocator();
        }
        #endregion

        #region Log Out Command

        public ICommand LogOutCommand { get { return new RelayCommand(() => ExecuteLogOutCommand()); } }

        public void ExecuteLogOutCommand()
        {
            //_locator.FirstVM.CurrentViewModel = new NotLoggedViewModel();
            Messenger.Default.Send(new NotificationMessage("LogOut"), "FirstViewModel");
        }
        #endregion

        #region My Recipes Command

        private ObservableCollection<Recipe> _myRecipes;

        public ObservableCollection<Recipe> MyRecipes
        {
            get
            {
                return _myRecipes;
            }
            set
            {
                _myRecipes = value;
                OnPropertyChanged();
            }
        }
        public ICommand MyRecipesCommand { get { return new RelayCommand(() => ExecuteMyRecipesCommand()); } }

        public void ExecuteMyRecipesCommand()
        {
            GetRecipes();
            _locator.Main.CurrentViewModel = new MyRecipesViewModel(MyRecipes, LoggedAccount);
        }

        public void GetRecipes()
        {
            using (var context = new RecipeDBEntities1())
            {
                var results = context.Recipes
                  .Where(r => r.RelatedAccountID == LoggedAccount.ID)
                  .OrderBy(r => r.Name);
                MyRecipes = new ObservableCollection<Recipe>(results);
            }
        }
        #endregion
    }
}
