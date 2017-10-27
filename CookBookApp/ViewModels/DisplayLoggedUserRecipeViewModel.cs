using CookBookApp.Commands;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace CookBookApp.ViewModels
{
    public class DisplayLoggedUserRecipeViewModel : ViewModelBase
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
        public DisplayLoggedUserRecipeViewModel(Recipe recipe, object p)
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

        #region Recipe Ingredients and Difficulty Level Get methods
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
        #endregion

        #region Back To Main Page Commmand
        public ICommand BackToMainPageCommand { get { return new RelayCommand(() => ExecuteBackToMainPageCommand()); } }

        public void ExecuteBackToMainPageCommand()
        {
            MessageBox.Show(LoggedAccount.Name);
            _locator.Main.CurrentViewModel = new FirstViewModel(LoggedAccount);
        }
        #endregion

        #region Edit Recipe Command

        public ICommand EditRecipeCommand { get { return new RelayCommand(() => ExecuteEditRecipeCommand()); } }

        public void ExecuteEditRecipeCommand()
        {
            _locator.Main.CurrentViewModel = new EditRecipeViewModel(LoggedAccount, Recipe);
        }
        #endregion

        #region Delete Recipe Command

        public ICommand DeleteRecipeCommand { get { return new RelayCommand(() => ExecuteDeleteRecipeCommand()); } }

        public void ExecuteDeleteRecipeCommand()
        {
            DeleteRecipeIngredients();

            DeleteRecipe();

            ExecuteBackToMainPageCommand();
        }
        public void DeleteRecipeIngredients()
        {
            using (var context = new RecipeDBEntities1())
            {
                foreach (var recIng in RecipeIngredientsCollection)
                    context.RecipeIngredients.Attach(recIng);
                context.RecipeIngredients.RemoveRange(RecipeIngredientsCollection);
            }
        }

        public void DeleteRecipe()
        {
            using (var context = new RecipeDBEntities1())
            {
                context.Recipes.Attach(Recipe);
                context.Recipes.Remove(Recipe);
                context.SaveChanges();
            }
        }
        #endregion
    }
}

