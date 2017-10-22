
using CookBookApp.Commands;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace CookBookApp.ViewModels
{
    public class DisplayRecipeViewModel : ViewModelBase
    {
        #region Properties
        public ViewModelLocator _locator;
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

        public Recipe _recipe;
        public Recipe Recipe
        {
            get
            {
                return _recipe;
            }
            set
            {
                _recipe = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RecipeIngredient> _recipeIngredients;
        public ObservableCollection<RecipeIngredient> RecipeIngredientsCollection
        {
            get
            {
                return _recipeIngredients;
            }
            set
            {
                _recipeIngredients = value;
                OnPropertyChanged();
            }
        }
        private string _difficultyLvl;
        public string DifficultyLvl
        {
            get
            {
                return _difficultyLvl;
            }
            set
            {
                _difficultyLvl = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public DisplayRecipeViewModel(Recipe recipe, object p)
        {
            LoggedAccount = new Account();
            LoggedAccount = (Account)p;
            _locator = new ViewModelLocator();
            Recipe = new Recipe();
            Recipe = recipe;

            GetDifficultyLevel();
            GetRecipeIngredients();
        }
        #endregion

        public void GetRecipeIngredients()
        {
            using (var context = new RecipeDBEntities1())
            {
                var Ingredients = context.RecipeIngredients
                    .Where(o => o.RecipeID == Recipe.ID);
                RecipeIngredientsCollection = new ObservableCollection<RecipeIngredient>(Ingredients);
            }
        }

        public void GetDifficultyLevel()
        {
            using (var context = new RecipeDBEntities1())
            {
                var DiffLvl = context.DifficultyLevels
                    .Where(o => o.DiffLvl_ID == Recipe.DifficultyLevel_ID)
                    .SingleOrDefault();
                DifficultyLvl = $"Difficulty: {DiffLvl.Name}";
            }
        }
        #region Back To Main Page Commmand
        public ICommand BackToMainPageCommand { get { return new RelayCommand(() => ExecuteBackToMainPageCommand()); } }

        public void ExecuteBackToMainPageCommand()
        {
            MessageBox.Show(LoggedAccount.Name);
            _locator.Main.CurrentViewModel = new FirstViewModel(LoggedAccount);
        }
        #endregion
    }
}
