   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:WpfApp1.User_ControlsWpfApp1.User_Controls  5   	       WpfApp1.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   5local#clr-namespace:WpfApp1.User_Controls     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Icon$K  Epack://application:,,,/WpfApp1;component/Resources/img/icons/icon.ico��5   	    � Title$ 
MainWindow��5	   	   $	��650��6   $
��1100��6)   $	��800��66   $	��500��6E   .��5
      �    MainBackground#���   6    � RowDefinitions 5   
   �� 5      $
��45px �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   � $��0��6   $��Stretch=�6    $��0��6<     SecondBackground#���  6I    5      �� 5      $
��80px �6#   �� 5      $
��80px �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6)   $��Center=�6B   .+�5      &    WpfApp1.User_Controls.IconButton  $��10q�6#   $��20��6/   $��30��6:      NormalImage$R Lpack://application:,,,/WpfApp1;component/Resources/img/icons/simple/home.png��5         
HoverImage$Q Kpack://application:,,,/WpfApp1;component/Resources/img/icons/hover/home.png��5      {�.    Command 5       �� *GoHomeCommand+6#   5!      �� 5#      $��1��6   $ 
Horizontal=�6)   .+�5$      �� $��10 0 0 0q�6   $��30��6+   $��30��66    �� CornerRadius$ 50��6B     InteractionColor#���  6T   ._�5%      �� $��25��6   $��25��6(   ��64   �� *StyleChangeCommand+��5&      �� 5'      	 �� 
TargetType"	 ��6.   .R�5(   "   A�   	DarkColor#���  6*   5)      5+      5,      �� 5-      
 �� Padding$
 0 0 0 10q�6   $��10 0 0 0q�6,   $��30��6>   $��30��6I   $ 50��6U   #���  6h   ._�5.      �� $��25��6   $	��NaN��6(   $��2,0,2,-5q�6[   ��65   �� *LangChangeCommand+��5/      �� 50      "	 ��6.   .R�51   "   �� $��Center=�6(   $��12(�6E     
ChosenLang#���  6^   52      54      55      56      �� 58      -   5        $��
AdminPanel��58      $��Center=�6+   $
��Left=�6F   $��2��6a   .+�59      �� $��10 0 0 0q�6     
AdminPanel#���  6+   $��120px��6R   y�6`   �� *	IsAdmin+  BoolToVisibilityConverter �� 	Converter# [ ��6�   �� *AdminPanelCommand+5:      �� 5<      $ 
Horizontal=�6   $��Right=�62   $��3��6N   y�6^   �� *	IsGuest+# [ .+�5=      ��   	SignInStr#���  6   $	��120��6?   $��0,0,10,0q�6m   ��6K   �� *SignInCommand+6m   5>      �� 5A      -   5        $��	UserPanel��5A      $��Center=�6~   $��Right=�6�   $ 
Horizontal=�6�   $��3��6�   y�6*   �� *IsUser+# [ .+�5B      �� $��0,0,10,0q�61   ��6   �� *Name+6E   �� 5C      $ 50��6   #���  6+   $��0,0,10,0q�6T   $��Center=�5D   	   $��Center=�6$   }�. ._�5E      �� $��Center=�6   $��35��6;   $��35��6F    � Cursor$
 Hand��6�    � ToolTip$? 9Кликните для просмотра профиля��6�   $��0q�6�   
������ ��6R   �� *NavigateToUserPageCommand+��5F      �� 5G      "	 ��6.   .R�5H   "   � $��35��6'   $��35��62   .+�5I   &   �� $��UniformToFill=�6,   ��6D   �� *
UserAvatar+  ImageSourceConverter# [  J� Clip 5J   *   @� 5K   .    @� Center$ 	17.5,17.5&�6>    @� RadiusX$
 17.5Q�6Q    @� RadiusY$
 17.5Q�6`   5M   '   5N   #   5O      5Q      5S      5U      5V      �� 5X   
   #���  6   $��1��6H   $��1��6X   $
��Auto=�6e   .��5Y      �� #���   6   $ 0��6D   ._�5Z       � -   5          �Name$ 	MainFrame��5Z        � NavigationUIVisibility$ Hidden=�6+   5[      5\      5^      5_      