using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CookBookApp.ViewModels
{
    public class EditRecipeViewModel : ViewModelBase
    {

        #region Properties

        private Recipe _editRecipe;
        public Recipe EditRecipe
        {
            get
            {
                return _editRecipe;
            }
            set
            {
                _editRecipe = value;
                OnPropertyChanged();
            }
        }

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

        private ObservableCollection<NewIngredientViewModel> _recipeIngredientsViewModels;
        public ObservableCollection<NewIngredientViewModel> RecipeIngredientsViewModels
        {
            get
            {
                return _recipeIngredientsViewModels;
            }
            set
            {
                _recipeIngredientsViewModels = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RecipeIngredient> _recipeIngredients;
        public ObservableCollection<RecipeIngredient> RecipeIngredients
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
        #endregion

        public ViewModelLocator _locator;

        public EditRecipeViewModel(Account acc, Recipe recipe)
        {
            FillDifficultyLevels();
            EditRecipe = new Recipe();
            EditRecipe = recipe;
            GetDifficultyLevel();

            LoggedAccount = new Account();
            LoggedAccount = acc;

            _locator = new ViewModelLocator();

            FillCategories();

            GetRecipeIngredients();
            GetRecipeIngredientsViewModels();
        }

        public void GetRecipeIngredientsViewModels()
        {
            RecipeIngredientsViewModels = new ObservableCollection<NewIngredientViewModel>();
            foreach (var recIng in RecipeIngredients)
            {
                var NewIngVm = new NewIngredientViewModel(recIng);
                RecipeIngredientsViewModels.Add(NewIngVm);
            }
        }

        public void GetRecipeIngredients()
        {
            using (var context = new RecipeDBEntities1())
            {
                var RecipeIngs = from RecIng in context.RecipeIngredients
                                        where RecIng.RecipeID == EditRecipe.ID
                                        orderby RecIng.Name
                                        select RecIng;
                RecipeIngredients = new ObservableCollection<RecipeIngredient>(RecipeIngs);
            }
        }
        #region Categories and SubCategories

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                EditRecipe.CategoryID = _selectedCategory.ID;
                OnPropertyChanged();
                FillSubCategory();
            }
        }
        private SubCategory _selectedSubCategory;
        public SubCategory SelectedSubCategory
        {
            get
            {
                return _selectedSubCategory;
            }
            set
            {
                _selectedSubCategory = value;
                EditRecipe.SubCategoryID = _selectedSubCategory.ID;
                OnPropertyChanged();
            }
        }
        private void FillCategories()
        {
            using (var context = new RecipeDBEntities1())
            {
                var q = (from c in context.Categories
                         select c).ToList();
                Categories = q;
            }
        }
        private void FillSubCategory()
        {
            Category category = SelectedCategory;
            using (var context = new RecipeDBEntities1())
            {
                var q = (from SubCategory in context.SubCategories
                         orderby SubCategory.ID
                         where SubCategory.CategoryID == category.ID
                         select SubCategory).ToList();
                SubCategories = q;
            }
        }
        private List<Category> _categories;
        public List<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }
        private List<SubCategory> _subCategories;
        public List<SubCategory> SubCategories
        {
            get
            {
                return _subCategories;
            }
            set
            {
                _subCategories = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Difficulty Levels

        public void GetDifficultyLevel()
        {
            using (var context = new RecipeDBEntities1())
            {
                var DiffLvl = context.DifficultyLevels
                    .Where(o => o.DiffLvl_ID == EditRecipe.DifficultyLevel_ID)
                    .SingleOrDefault();
                SelectedDifficultyLvl = DiffLvl.DiffLvl_ID;
            }
        }

        private int _selectedDifficultyLvl;
        public int SelectedDifficultyLvl
        {
            get
            {
                return _selectedDifficultyLvl;
            }
            set
            {
                _selectedDifficultyLvl = value;
                EditRecipe.DifficultyLevel_ID = _selectedDifficultyLvl;
                OnPropertyChanged();
            }
        }

        public List<DifficultyLevel> _difficultyLvls;
        public List<DifficultyLevel> DifficultyLvls
        {
            get
            {
                return _difficultyLvls;
            }
            set
            {
                _difficultyLvls = value;
                OnPropertyChanged();
            }
        }
        private void FillDifficultyLevels()
        {
            using (var context = new RecipeDBEntities1())
            {
                var q = (from d in context.DifficultyLevels
                         select d).ToList();
                DifficultyLvls = q;
            }
        }
        #endregion
    }
}
