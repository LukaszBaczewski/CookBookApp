   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      F BCookBookApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:CookBookApp.ViewModelsCookBookApp.ViewModels 5      5clr-namespace:CookBookApp.ViewsCookBookApp.Views 5      +   %CookBookApp.Views.CreateNewRecipeView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      3vm$clr-namespace:CookBookApp.ViewModels     5      1localclr-namespace:CookBookApp.Views     5      .��5      �� $
��Auto=�6   $
��Auto=�68   .��5   
   � .+�5   
   �� .+�5      �� $��10q�6   .��6    Recipe6(   �� 5      �� 5      $
��10 5q�6   .��6"   Title6)   �� 5      $
��15 0q�6c   $��25��6q   $��2q�6}   ��6   �� *NewRecipe.Name+  �� NotifyOnSourceUpdated  .  �� Mode$ TwoWay=�6}   �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� $��
10 4 115 4q�6   .��60   
Difficulty6<   �� 5      $��
10 4 110 4q�6   .��60   
Category6:   �� 5      $��
10 4 120 4q�6   .��60   SubCategory6=   5      �� 5      $ 
Horizontal=�6   .+�5      ��  �� 
IsEditable .  �� 
IsReadOnly .  �� Text$ 	Undefined��6C   $
��Left=�6T   $	��150��6o   $
��15 0q�6{    �� DisplayMemberPath$
 Name��5       ��5      �� *DifficultyLvls+ �� SelectedItem 5!      �� *SelectedDifficultyLvl+�� 5"       .  . $ 	Undefined��6C   $
��Left=�6T   $	��150��6o   $
��15 0q�6{   $
 Name��5$      ��5#      �� *
Categories+ 5%      �� *SelectedCategory+�� 5&       .  . $ 	Undefined��6C   $
��Left=�6T   $	��150��6o   $
��15 0q�6{   $
 Name��5(      ��5'      �� *SubCategories+ 5)      �� *SelectedSubCategory+5*      �� 5,      $��	10 10 0 0q�6   .��6'   
Servings61   �� 5-      $
��15 0q�6   $
��Left=�6$   $��60��6?   $��25��6J   $��2q�6z   ��6V   �� *NewRecipe.Servings+6z   �� 50      $��10q�6   .��6    Ingredients6-   � 51      $��	0 0 100 0q�6    � ColumnDefinitions 52      �� 53      $��2* �6'   �� 54      $��3* �6'   �� 55      $��4* �6'   �� 56      $��6* �6'   �� 57      $��6* �6'   �� 58      $��* �6'   	 � RowDefinitions	 5:      �� 5;      $��30 �6$   �� 5<      $��30 �6$   �� 5=      $��30 �6$   .+�5A      �� $��0��6   $��0��6(   $��heading��65   $��Center=�6G   �� 5B      $��1��6   $��0��6(   $��Quantity��65   $��Center=�6H   �� 5C      $��2��6   $��0��6(   $��Measure��65   $��Center=�6G   �� 5D      $��3��6   $��0��6(   $
��Item��65   $��Center=�6D   �� 5E      $��4��6   $��0��6(   $��
Prep Notes��65   $��Center=�6J   �� 5F      $��5��6   $��0��6(   �� 5I      $��0��6   $��1��6(   $	��ex:��65   $��Center=�6C   �� 5J      $��1��6   $��1��6(   $��2 1/2��65   $��Center=�6E   �� 5K      $��2��6   $��1��6(   $
��cups��65   $��Center=�6D   �� 5L      $��3��6   $��1��6(   $��Flour��65   $��Center=�6E   �� 5M      $��4��6   $��1��6(   $��sifted��65   $��Center=�6F   �� 5N      $��5��6   $��1��6(   5P      � 5Q      $��	0 0 100 0q�6   .+�5R      �� ��6#   �� *RecipeIngredientsViewModels+$ TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=���5S      �� 5T       �� DataType3  -CookBookApp.ViewModels.NewIngredientViewModel"  6+   .R�5U   "   � $
��Auto��6'   $
��Auto��64    5V   &   �� 5W   *   $��2* �6;   �� 5X   *   $��3* �6;   �� 5Y   *   $��4* �6;   �� 5Z   *   $��6* �6;   �� 5[   *   $��6* �6;   �� 5\   *   $��* �6;   	 5^   &   �� 5_   *   $��30 �68   .+�5a   &   �� $��0��6/   $��0��6?   $��Center=�6L   $��Center=�6i   �� 5b   &   $��1��6/   $��0��6?   ��6L   �� *NewProduct.Quantity+�� 5c   &   $��2��6/   $��0��6?   ��6L   �� *NewProduct.Measure+�� 5d   &   $��3��6/   $��0��6?   ��6L   �� *NewProduct.Name+�� 5e   &   $��4��6/   $��0��6?   ��6L   �� *NewProduct.PreparationNotes+�� 5f   &   $��5��6-   $��0��6=   $��+��6J   ��6V   �� *AddNewIngredientCommand+5g   #   5h      5j      5k      �� 5n      $��+Add Ingredient Line��6a   $	��130��6�   $��30��6�   $��
0 10 100 0q�6�   ��6   �� *AddNewIngredientCommand+  . 6�   �� 5q      $��10 0 0 0q�6   .��6&   Preparation Time68   �� 5r      $��25��6   $��2q�6"   $��60��6.   $
��Left=�69   $
��15 0q�6T   ��6b   �� *NewRecipe.PreparationTime+�� 5u      $��10q�6   $��Preparation��6    �� 5v      $
��15 0q�6   $	��220��6$    �� AcceptsReturn . ��61   �� *NewRecipe.Instruction+6X   �� 5y      $��	10 10 0 0q�6   $��Additional Notes��6'   �� 5z      $
��15 0q�6   $��25��6$   ��60   �� *AdditionalNotes+�� 5}      $ 
Horizontal=�6   $��Right=�62   .+�5~      �� $��Right=�6   $��15q�69   $	��110��6E   $��30��6Q   $��Back To Main Page��6]   ��6y   �� *BackToMainPageCommand+�� 5      $��Right=�6   $��15q�69   $	��110��6E   $��30��6Q   $��Save Recipe��6]   ��6s   �� *SaveRecipeCommand+  . 5�      5�      5�      5�      5�      