using CookBookApp.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace CookBookApp.ViewModels
{
    public class SearchResultsViewModel : ViewModelBase
    {
        #region Properties

        private ViewModelLocator _locator;

        private Account _loggedAccount;
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

        private ObservableCollection<Recipe> _searchedRecipes;
        public ObservableCollection<Recipe> SearchedRecipes
        {
            get
            {
                return _searchedRecipes;
            }
            set
            {
                _searchedRecipes = value;
                OnPropertyChanged();
            }
        }

        private string _noMatches;
        public string NoMatches
        {
            get
            {
                return _noMatches;
            }
            set
            {
                _noMatches = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public SearchResultsViewModel(ObservableCollection<Recipe> sr, Account account, string searchWord)
        {
            SearchedRecipes = new ObservableCollection<Recipe>(sr);
            NoMatches = (!SearchedRecipes.Any()) ? $"Your search - {searchWord} - did not match any recipes" : "";
            _locator = new ViewModelLocator();
  
             if (account.Name != null)
            {
                LoggedAccount = new Account();
                LoggedAccount = account;
            }

        }
        #endregion

        #region Back To Main Page Command
        public ICommand BackToMainPageCommand { get { return new RelayCommand(() => ExecuteBackToMainPage()); } }

        public void ExecuteBackToMainPage()
        {
            if (LoggedAccount != null)
                _locator.Main.CurrentViewModel = new FirstViewModel(LoggedAccount);
            else
                _locator.Main.CurrentViewModel = new FirstViewModel();
        }
        #endregion

        #region Search Command

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
            ExecuteSearchQuery();

            NoMatches = (!SearchedRecipes.Any()) ? $"Your search - {SearchKeyword} - did not match any recipes" : "";
        }

        public void ExecuteSearchQuery()
        {
            using (var context = new RecipeDBEntities1())
            {
                var results = context.Recipes
                  .Where(r => r.Name.Contains(SearchKeyword))
                  .OrderBy(r => r.Name);
                SearchedRecipes = new ObservableCollection<Recipe>(results);
            }
        }
        #endregion

    }
}
