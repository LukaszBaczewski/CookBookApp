using CookBookApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace CookBookApp.ViewModels
{
    public class CreateNewRecipeViewModel : ViewModelBase
    {
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
        private Recipe _newRecipe;
        public Recipe NewRecipe
        {
            get
            {
                return _newRecipe;
            }
            set
            {
                _newRecipe = value;
                OnPropertyChanged();
            }
        }
       
        #region Recipe Properties
        public string Name
        {
            get
            {
                return NewRecipe.Name;
            }
            set
            {
                NewRecipe.Name = value;
                OnPropertyChanged();
            }
        }
        public int PreparationTime
        {
            get
            {
                return NewRecipe.PreparationTime;
            }
            set
            {
                NewRecipe.PreparationTime = value;
                OnPropertyChanged();
            }
        }
        public int Servings
        {
            get
            {
                return NewRecipe.Servings;
            }
            set
            {
                NewRecipe.Servings = value;
                OnPropertyChanged();
            }
        }
        
        public string Instruction
        {
            get
            {
                return NewRecipe.Instruction;
            }
            set
            {
                NewRecipe.Instruction = value;
                OnPropertyChanged();
            }
        }
        public string AdditionalNotes
        {
            get
            {
                return NewRecipe.AdditionalNotes;
            }
            set
            {
                NewRecipe.AdditionalNotes = value;
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<NewIngredientViewModel> _recipeIngredients;
        public ObservableCollection<NewIngredientViewModel> RecipeIngredientsViewModels
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
                NewRecipe.CategoryID = _selectedCategory.ID;
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
                NewRecipe.SubCategoryID = _selectedSubCategory.ID;
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

        #region Difficulty Level
        private DifficultyLevel _selectedDifficultyLvl;
        public DifficultyLevel SelectedDifficultyLvl
        {
            get
            {
                return _selectedDifficultyLvl;
            }
            set
            {
                _selectedDifficultyLvl = value;
                NewRecipe.DifficultyLevel_ID = _selectedDifficultyLvl.DiffLvl_ID;
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

        #region Constructor
        public CreateNewRecipeViewModel(Account LoggedAcc)
        {
            _locator = new ViewModelLocator();
            LoggedAccount = new Account();
            LoggedAccount = LoggedAcc;
            RecipeIngredientsViewModels = new ObservableCollection<NewIngredientViewModel>();   
            NewRecipe = new Recipe();

            FillCategories();
            FillDifficultyLevels();

            RecipeIngredient NewRecipeIngredient = new RecipeIngredient();
            NewRecipe.RecipeIngredients.Add(NewRecipeIngredient);
            var NewIngredientVM = new NewIngredientViewModel(NewRecipeIngredient);
            RecipeIngredientsViewModels.Add(NewIngredientVM);
        }
        #endregion

        #region Back To Main Page Command
        public ICommand BackToMainPageCommand { get { return new RelayCommand(() => ExecuteBackToMainPage()); } }

        public void ExecuteBackToMainPage()
        {
            _locator.Main.CurrentViewModel = new FirstViewModel(LoggedAccount);
        }
        #endregion

        #region Save Recipe Command
        public ICommand SaveRecipeCommand { get { return new RelayCommand(() => ExecuteSaveRecipeCommand(), CanExecuteSaveRecipeCommand); } }

        public void ExecuteSaveRecipeCommand()
        {
            if (RecipeAlreadyExists())
            {
                MessageBox.Show("Recipe with that name already exists in the database");
                return;
            }

            DisposeEmptyRecipeIngredients();

            FilterAndAddNewIngredients();         

            AddRecipe();
                   
            ExecuteBackToMainPage();
        }


        public void AddRecipe()
        {
            if (LoggedAccount != null)
            {
                using (var context = new RecipeDBEntities1())
                {
                    var loggedAccount = context.Accounts
                        .Where(o => o.ID == LoggedAccount.ID)
                        .SingleOrDefault();

                    if (loggedAccount != null)
                    {
                        loggedAccount.Recipes.Add(NewRecipe);

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    MessageBox.Show("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }                      
                    }
                }              
            }
        }
        public void FilterAndAddNewIngredients()
        {
            using (var context = new RecipeDBEntities1())
            {

                foreach (RecipeIngredient recipeIng in NewRecipe.RecipeIngredients)
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
            var FilledIngredients = NewRecipe.RecipeIngredients
                 .Where(ingredient => (!string.IsNullOrEmpty(ingredient.Name) && !string.IsNullOrEmpty(ingredient.Measure) && !string.IsNullOrEmpty(ingredient.Quantity)));
            ObservableCollection<RecipeIngredient> FilledRecipeIngredients = new ObservableCollection<RecipeIngredient>(FilledIngredients);
            NewRecipe.RecipeIngredients = FilledRecipeIngredients;
                
        }
        public bool CanExecuteSaveRecipeCommand()
        {
            return (PropertiesFilled());
        }

        /// <summary>
        /// Checks if Recipe already exists
        /// </summary>
        /// <returns></returns>
        public bool RecipeAlreadyExists()
        {
            using (var context = new RecipeDBEntities1())
            {
                return (context.Recipes.Any(o => o.Name == NewRecipe.Name));
            }
        }

        /// <summary>
        /// Checks if Recipe Properties are filled
        /// </summary>
        /// <returns></returns>
        public bool PropertiesFilled()
        {
            if (NewRecipe != null)
            {
                if (NewRecipe.DifficultyLevel_ID == null || NewRecipe.PreparationTime == 0 || NewRecipe.Servings == 0 /*|| !NewRecipe.RecipeIngredients.Any()*/)
                    return false;
                else
                    return !NewRecipe.GetType().GetProperties()
                        .Where(pi => pi.GetValue(NewRecipe) is string)
                        .Select(pi => (string)pi.GetValue(NewRecipe))
                        .Any(value => string.IsNullOrEmpty(value));
            }
            return false;
        }
        #endregion

        #region Add New Ingredient Command
        public ICommand AddNewIngredientCommand { get { return new RelayCommand(() => ExecuteAddIngredient(), CanExecuteAddIngredient); } }

        private void ExecuteAddIngredient()
        {
            RecipeIngredient NewRecipeIngredient = new RecipeIngredient();
            
            NewRecipe.RecipeIngredients.Add(NewRecipeIngredient);
            var NewIngredientVM = new NewIngredientViewModel(NewRecipeIngredient);
            RecipeIngredientsViewModels.Add(NewIngredientVM);            
        }

       
        private bool CanExecuteAddIngredient()
        {
            if(NewRecipe != null)   
                return AreIngredientsFilled();
            return false;
        }
       
        private bool AreIngredientsFilled()
        {
            if (NewRecipe.RecipeIngredients != null)
            {
                IEnumerable<RecipeIngredient> EmptyIngredients = NewRecipe.RecipeIngredients
            .Where(ingredient => (string.IsNullOrEmpty(ingredient.Name) || ((ingredient.Quantity == null)) || (string.IsNullOrEmpty(ingredient.Measure))));
                if (EmptyIngredients.Count() > 10)
                    return false;                
            }
            return true;
        }
        #endregion
    }
}