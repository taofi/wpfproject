   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfApp1.View.PagesWpfApp1.View.Pages  5      =#clr-namespace:WpfApp1.User_ControlsWpfApp1.User_Controls  66   %    WpfApp1.View.Pages.CommentsPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:WpfApp1.View.Pages     5      6local1#clr-namespace:WpfApp1.User_Controls     66     N� Title$  CommentsPage��5	      .��5      ��    MainBackground#���   6   .+�5   
   �� $��Center=�6   $	��300��6.   $��10q�6:    �� CornerRadius$ 0��6�     InteractionColor#���  6�   y�6F   �� *	IsVisible+  BoolToVisibilityConverter �� 	Converter# [ ._�5      �  � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $��* �6$   �� 5      $
��Auto �6$    � ColumnDefinitions 5      �� 5      $��* �6'   .+�5      ��  �� Padding$ 5q�6   $��0��6%   $ 0��62   #���  6C   ._�5      ]� ��.  .+�5      �� $
��Left=�6=   ��6    �� *
PostText+6=   �� 5      $��Right=�6=   ��6    �� *
PostDate+6=   5      5      �� 5      $��1��6   $ 0��6'   #���  68   ._�5      �� $	��300��6   $��Center=�6(   $��Uniform=�6C   ��6U   �� *file+  PostImageSourceConverter# [ 5      5      5       �� 5!   
     SecondBackground#���  6e   $��10q�6�   $	��500��6�   y�6   �� *IsUser+# [ ._�5"      ��  �� Orientation$ 
Horizontal=�6   .+�5#      �� $��5q�6   $	��390��6&    �� TextWrapping$
 Wrap=�62    �� AcceptsReturn . ��6[   �� *Text+�� 5$      $��10q�6   $	��100��6%     Send#��[ 61   ��6Q   �� *SendCommand+5%      5&      �� 5'   
   $	��510��6   ��6!   �� *
Comments+��5(      �� 5)      .R�5*      �� $ 10q�6     	DarkColor#���  6*   $	��500��6S   ._�5+      �� $ 5��6!   #���  62   ._�5,      �� .+�5-   "   �� $ 
Horizontal=�6.   .+�5.   &   � $��35��6+   $��35��66   .+�5/   *   �� $��UniformToFill=�60   ��6H   �� *Users.UserAvatar+  ImageSourceConverter# [ 	 J� Clip	 50   .   @� 51   2   
 @� Center$
 	17.5,17.5&�6B    @� RadiusX$
 17.5Q�6U    @� RadiusY$
 17.5Q�6d   53   +   54   '   ]� 55   &   ��.  .+�56   *   �� $
��Left=�60   ��6F   �� *
Users.Name+�� 57   *   $��Right=�60   ��6G   �� *Date+&    WpfApp1.User_Controls.IconButton  58   *   $��Right=�6<   $��3q�6S   $��20��6^   $��30��6i      NormalImage$S Mpack://application:,,,/WpfApp1;component/Resources/img/icons/simple/trash.png��59         
HoverImage$R Lpack://application:,,,/WpfApp1;component/Resources/img/icons/hover/trash.png��5:      y�58   u   �� *	IsOwner+# [    Command 5;      �� *DeleteCommand+5<   '   5=   #   �� 5>   "   $ 1��6)   #���  6:   $	��5 0q�6j   $
��Auto��6w   $��2��6�   �� 5?   "   .��5@   &   �� $
��Wrap=�60   ��6D   �� *Text+5A   #   5B      5C      5D      5E      5G      5H      5I      