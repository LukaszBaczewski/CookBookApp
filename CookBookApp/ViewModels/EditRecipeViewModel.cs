using CookBookApp.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

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

        #region Fields

        public ViewModelLocator _locator;
        public ObservableCollection<RecipeIngredient> NewRecipeIngredients;

        #endregion

        #region Constructor

        public EditRecipeViewModel(Account acc, Recipe recipe)
        {
            EditRecipe = new Recipe();
            EditRecipe = recipe;

            NewRecipeIngredients = new ObservableCollection<RecipeIngredient>();

            LoggedAccount = new Account();
            LoggedAccount = acc;

            _locator = new ViewModelLocator();

            FillCategories();
            GetCategory();
            FillSubCategory();
            GetSubCategory();
            
            FillDifficultyLevels();
            GetDifficultyLevel();
            GetRecipeIngredients();
            GetRecipeIngredientsViewModels();
        }

        #endregion

        #region RecipeIngredients Get methods

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

        #endregion

        #region Categories and SubCategories properties, fill and get methods

        public void GetCategory()
        {
            using (var context = new RecipeDBEntities1())
            {
                var Cat = (from c in context.Categories
                           where c.ID == EditRecipe.CategoryID
                           select c).FirstOrDefault();
                SelectedCategory = Cat.ID;
            }
        }

        public void GetSubCategory()
        {
            using ( var context = new RecipeDBEntities1())
            {
                var subCat = (from s in context.SubCategories
                              where s.ID == EditRecipe.SubCategoryID
                              select s).FirstOrDefault();
                SelectedSubCategory = subCat.ID;
            }
        }

        private int _selectedCategory;
        public int SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                EditRecipe.CategoryID = _selectedCategory;
                OnPropertyChanged();
                FillSubCategory();
            }
        }
        private int _selectedSubCategory;
        public int SelectedSubCategory
        {
            get
            {
                return _selectedSubCategory;
            }
            set
            {
                _selectedSubCategory = value;
                EditRecipe.SubCategoryID = _selectedSubCategory;
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
            using (var context = new RecipeDBEntities1())
            {
                var q = (from SubCategory in context.SubCategories
                         orderby SubCategory.ID
                         where SubCategory.CategoryID == SelectedCategory
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

        #region Add New Ingredient Command
        public ICommand AddNewIngredientCommand { get { return new RelayCommand(() => ExecuteAddIngredient(), CanExecuteAddIngredient); } }

        private void ExecuteAddIngredient()
        {
            AddIngredient();
        }

        private void AddIngredient()
        {
            RecipeIngredient NewRecipeIngredient = new RecipeIngredient();

            NewRecipeIngredients.Add(NewRecipeIngredient);
            var NewIngredientVM = new NewIngredientViewModel(NewRecipeIngredient);
            RecipeIngredientsViewModels.Add(NewIngredientVM);
        }
        
        private bool CanExecuteAddIngredient()
        {
            if (EditRecipe != null)
                return AreIngredientsFilled();
            return false;
        }

        private bool AreIngredientsFilled()
        {
            if (RecipeIngredients != null)
            {
                IEnumerable<RecipeIngredient> EmptyIngredients = RecipeIngredients
            .Where(ingredient => (string.IsNullOrEmpty(ingredient.Name) || ((ingredient.Quantity == null)) || (string.IsNullOrEmpty(ingredient.Measure))));
                if (EmptyIngredients.Count() > 10)
                    return false;
            }
            return true;
        }
        #endregion

        #region Save Changes Command

        public ICommand SaveChangesCommand { get { return new RelayCommand(() => ExecuteSaveChangesCommand()); } }

        public void ExecuteSaveChangesCommand()
        {
            DisposeEmptyRecipeIngredients();

            FilterAndAddNewIngredients();

            SaveChanges();

            ExecuteBackToMainPage();
        }

        public void FilterAndAddNewIngredients()
        {
            using (var context = new RecipeDBEntities1())
            {

                foreach (RecipeIngredient recipeIng in RecipeIngredients)
                {

                    var existingIng = context.Ingredients
                            .Where(ingredient => ingredient.Name == recipeIng.Name)
                            .OrderByDescending(o => o.ID)
                            .FirstOrDefault();
                    if (existingIng != null)
                    {
                        recipeIng.IngredientID = existingIng.ID;
                    }
                    else
                    {
                        Ingredient newIngredient = new Ingredient();
                        newIngredient.Name = recipeIng.Name;
                        context.Ingredients.Add(newIngredient);
                        context.SaveChanges();
                        recipeIng.IngredientID = newIngredient.ID;
                    }

                    context.SaveChanges();
                }

                foreach (RecipeIngredient recipeIng in NewRecipeIngredients)
                {

                    var existingIng = context.Ingredients
                            .Where(ingredient => ingredient.Name == recipeIng.Name)
                            .OrderByDescending(o => o.ID)
                            .FirstOrDefault();
                    if (existingIng != null)
                    {
                        recipeIng.IngredientID = existingIng.ID;
                    }
                    else
                    {
                        Ingredient newIngredient = new Ingredient();
                        newIngredient.Name = recipeIng.Name;
                        context.Ingredients.Add(newIngredient);
                        context.SaveChanges();
                        recipeIng.IngredientID = newIngredient.ID;
                    }

                    context.SaveChanges();
                }
            }
        }

        public void DisposeEmptyRecipeIngredients()
        {
            var FilledIngredients = RecipeIngredients
                 .Where(ingredient => (!string.IsNullOrEmpty(ingredient.Name) && !string.IsNullOrEmpty(ingredient.Measure) && !string.IsNullOrEmpty(ingredient.Quantity)));
            ObservableCollection<RecipeIngredient> FilledRecipeIngredients = new ObservableCollection<RecipeIngredient>(FilledIngredients);
            RecipeIngredients = FilledRecipeIngredients;
        }

        public void SaveChanges()
        {
            using (var context = new RecipeDBEntities1())
            {             
                foreach (var ing in RecipeIngredients)
                    context.Entry(ing).State = System.Data.Entity.EntityState.Modified;
                context.Entry(EditRecipe).State = System.Data.Entity.EntityState.Modified;

                foreach (var recIng in NewRecipeIngredients)
                    EditRecipe.RecipeIngredients.Add(recIng);
                context.SaveChanges();
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
