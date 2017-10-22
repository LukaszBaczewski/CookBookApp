using CookBookApp.Commands;
using System.Collections.ObjectModel;
using System.Linq;
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
        #endregion

        #region Constructor
        public SearchResultsViewModel(ObservableCollection<Recipe> sr, Account account)
        {
            SearchedRecipes = new ObservableCollection<Recipe>(sr);
            _locator = new ViewModelLocator();
            LoggedAccount = account;
        }
        #endregion

        #region Back To Main Page Command
        public ICommand BackToMainPageCommand { get { return new RelayCommand(() => ExecuteBackToMainPage()); } }

        public void ExecuteBackToMainPage()
        {
            _locator.Main.CurrentViewModel = new FirstViewModel(LoggedAccount);
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

            //_locator.Main.CurrentViewModel = new SearchResultsViewModel(Results, LoggedAccount);
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
