using System.Collections.ObjectModel;

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
    }
}
