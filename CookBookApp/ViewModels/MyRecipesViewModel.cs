using CookBookApp.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CookBookApp.ViewModels
{
    public class MyRecipesViewModel : ViewModelBase
    {
        #region Constructor
        public MyRecipesViewModel(ObservableCollection<Recipe> myrecipes, Account acc)
        {
            MyRecipes = myrecipes;
            _locator = new ViewModelLocator();
            LoggedAccount = new Account();
            LoggedAccount = acc;
        }
        #endregion

        #region Properties

        public ViewModelLocator _locator;
        public Account LoggedAccount { get; set; }


        public ObservableCollection<Recipe> _myRecipes;
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
        #endregion

        #region Back To Main Page Command
        public ICommand BackToMainPageCommand { get { return new RelayCommand(() => ExecuteBackToMainPage()); } }

        public void ExecuteBackToMainPage()
        {
                _locator.Main.CurrentViewModel = new FirstViewModel(LoggedAccount);
        }
        #endregion
    }
}
