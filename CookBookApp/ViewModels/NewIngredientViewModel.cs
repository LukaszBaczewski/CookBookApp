

using System;

namespace CookBookApp.ViewModels
{
    public class NewIngredientViewModel : ViewModelBase
    {
        private RecipeIngredient _newProduct;
        public RecipeIngredient NewProduct
        {
            get
            {
                return _newProduct;
            }
            set
            {
                _newProduct = value;
                OnPropertyChanged();
            }
        }
        public NewIngredientViewModel(RecipeIngredient Ingredient)
        {
            NewProduct = new RecipeIngredient();
            NewProduct = Ingredient;            
        }
    }
}
