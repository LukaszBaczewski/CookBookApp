   M S B A M L   `   `   `  ����F  BCookBookApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CookBookApp.ViewsCookBookApp.Views  5      ?$clr-namespace:CookBookApp.ViewModelsCookBookApp.ViewModels  5      &     CookBookApp.Views.EditRecipeView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:CookBookApp.Views     5      3vm$clr-namespace:CookBookApp.ViewModels     5      .��5
      � .+�5   
   �� $��Disabled=�6   $
��Auto=�6@   .��5      � $��Disabled=�6   .+�5      �� $��Disabled=�6   .+�5      �� $��10q�6   .��6(   Recipe60   �� 5      �� 5      $
��10 5q�6   .��6*   Title61   �� 5      $
��15 0q�6?   $��25��6M   $��2q�6Y   ��6   �� *EditRecipe.Name+6Y   �� 5        �� Orientation$  
Horizontal=�6!   .+�5      �� $��
10 4 115 4q�6    .��64   
Difficulty6@   �� 5      $��
10 4 110 4q�6    .��64   
Category6>   �� 5      $��
10 4 120 4q�6    .��64   SubCategory6A   5      �� 5      $  
Horizontal=�6!   .+�5      ��  �� 
IsEditable .  �� 
IsReadOnly . $
��Left=�6G   $	��150��6b   $
��15 0q�6n    �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ 
DiffLvl_ID��5       ��5      �� *DifficultyLvls+ �� SelectedValue 5!      �� *SelectedDifficultyLvl+ �� Mode$ TwoWay=��� 5"       .  .  �� Text$ 	Undefined��6G   $
��Left=�6X   $	��150��6s   $
��15 0q�6   $
 Name��5$      $ ID��5%      ��5#      �� *
Categories+ 5&      �� *SelectedCategory+$ TwoWay=��� 5'       .  . $ 	Undefined��6G   $
��Left=�6X   $	��150��6s   $
��15 0q�6   $
 Name��5)      $ ID��5*      ��5(      �� *SubCategories+ 5+      �� *SelectedSubCategory+$ TwoWay=�5,      �� 5.      $��	10 10 0 0q�6   .��6/   
Servings69   �� 5/      $
��15 0q�6   $
��Left=�6,   $��60��6G   $��25��6R   $��2q�6�   ��6^   �� *EditRecipe.Servings+6�   �� 52      $��10q�6   .��6(   Ingredients65   � 53      $��	0 0 100 0q�6    � ColumnDefinitions 54      �� 55      $��2* �6/   �� 56      $��3* �6/   �� 57      $��4* �6/   �� 58      $��6* �6/   �� 59      $��6* �6/   �� 5:      $��* �6/   	 � RowDefinitions	 5<      �� 5=      $��30 �6,   �� 5>      $��30 �6,   �� 5?      $��30 �6,   .+�5C      �� $��0��6    $��0��60   $��heading��6=   $��Center=�6O   �� 5D      $��1��6    $��0��60   $��Quantity��6=   $��Center=�6P   �� 5E      $��2��6    $��0��60   $��Measure��6=   $��Center=�6O   �� 5F      $��3��6    $��0��60   $
��Item��6=   $��Center=�6L   �� 5G      $��4��6    $��0��60   $��
Prep Notes��6=   $��Center=�6R   �� 5H      $��5��6    $��0��60   �� 5K      $��0��6    $��1��60   $	��ex:��6=   $��Center=�6K   �� 5L      $��1��6    $��1��60   $��2 1/2��6=   $��Center=�6M   �� 5M      $��2��6    $��1��60   $
��cups��6=   $��Center=�6L   �� 5N      $��3��6    $��1��60   $��Flour��6=   $��Center=�6M   �� 5O      $��4��6    $��1��60   $��sifted��6=   $��Center=�6N   �� 5P      $��5��6    $��1��60   5R      � 5S      $��	0 0 100 0q�6   .+�5T      �� ��6'   �� *RecipeIngredientsViewModels+$ TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=���5U      �� 5V   "    �� DataType3   -CookBookApp.ViewModels.NewIngredientViewModel"  6/   .R�5W   &   � $
��Auto��6+   $
��Auto��68    5X   *   �� 5Y   .   $��2* �6?   �� 5Z   .   $��3* �6?   �� 5[   .   $��4* �6?   �� 5\   .   $��6* �6?   �� 5]   .   $��6* �6?   �� 5^   .   $��* �6?   	 5`   *   �� 5a   .   $��30 �6<   .+�5c   *   �� $��0��63   $��0��6C   $��Center=�6P   $��Center=�6m   �� 5d   *   $��1��63   $��0��6C   ��6P   �� *NewProduct.Quantity+$ TwoWay=��� 5e   *   $��2��63   $��0��6C   ��6P   �� *NewProduct.Measure+$ TwoWay=��� 5f   *   $��3��63   $��0��6C   ��6P   �� *NewProduct.Name+$ TwoWay=��� 5g   *   $��4��63   $��0��6C   ��6P   �� *NewProduct.PreparationNotes+$ TwoWay=��� 5h   *   $��5��61   $��0��6A   $��+��6N   ��6Z   �� *AddNewIngredientCommand+5i   '   5j   #   5l      5m      �� 5p      $��+Add Ingredient Line��6I   $	��130��6h   $��30��6t   $��
0 10 100 0q�6�   ��6   �� *AddNewIngredientCommand+6�   �� 5s      $��10 0 0 0q�6   .��6.   Preparation Time6@   �� 5t      $��25��6   $��2q�6*   $��60��66   $
��Left=�6A   $
��15 0q�6\   ��6j   �� *EditRecipe.PreparationTime+�� 5w      $��10q�6   $��Preparation��6(   �� 5x      $
��15 0q�6   $	��220��6,    �� AcceptsReturn .  �� TextWrapping$
 Wrap=�6�   ��69   �� *EditRecipe.Instruction+ �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . $ TwoWay=�6�   �� 5{      $��	10 10 0 0q�6   $��Additional Notes��6/   �� 5|      $
��15 0q�6   $��25��6,   ��68   �� *EditRecipe.AdditionalNotes+�� 5      $  
Horizontal=�6!   $��Right=�6:   .+�5�      �� $��Right=�6!   $��15q�6=   $	��110��6I   $��30��6U   $��Back To Main Page��6a   ��6}   �� *BackToMainPageCommand+�� 5�      $��Right=�6!   $��15q�6=   $	��110��6I   $��30��6U   $��Save Changes��6a   ��6x   �� *SaveChangesCommand+5�      5�      5�      5�      5�      5�      