   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.PagesWpfApp1.Pages  5          WpfApp1.Pages.CatalogPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WpfApp1.Pages     5        N� Title$  CatalogPage��5	      .��5      �    MainBackground#���   6    � RowDefinitions 5   
   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��* �6   .+�5   
   � $	��Top=�6   
������ $��0��6@   $��5q�6P   $��0��6[   .+�5      �� $��10px 10px 10px 0pxq�6     SecondBackground#���  61   ._�5      �� .+�5      �� $
��20px(�6_   $	��5pxq�6o   $
��40px��6}   $
��10pxq�6�   ��6   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�6�   5      5      5      � 5   
   
������ $��1��6(   $��5q�68   $��0��6C   .+�5       �� -   5        $��AuthorPages��5    �   #���  6g   $
��10pxq�6�   ��6   �� *AuthorPages+ �� SelectedItem 6;   �� *SelectedAuthorPage+��5!      �� 5"       �� 
TargetType" ��6   .�5#      ��  �� Property � �� �� Value$ Stretch=�6G   �� 5$       � ��$ 15(�65   �� 5%       � ��$S Mpack://application:,,,/WpfApp1;component/Resources/Fonts/#PT Sans Narrow Bold)�67   5&      ��5(      �� 5)      .R�5*      ��   InteractionColor#���  6!    �� Padding$ 10q�6Q   $��5q�6^   ._�5+      ]� $
��Left=�6(   .+�5,   "   �� $��50��6)   $��50��64   	 �� CornerRadius$	 50��6@     	DarkColor#���  6R   $��0,0,10,0q�6{   $��Center=�6�   $��Center=�6�   ._�5-   &   �� $��Center=�6-   $��Center=�6J   $��50��6�   $��50��6�   $��UniformToFill=�6�   ��6e   �� *
PageIcon+  ImageSourceConverter
 �� 	Converter#
 [  J� Clip 5.   *   �� 5/   .    �� RadiusX$ 25Q�6@    �� RadiusY$ 25Q�6M    �� Rect$ 	0,0,50,50�6Z   51   '   52   #   �� 53   "   $��Center=�6F   
���   ���6,   �� *
PageName+6a   54      55      56      58      59      5:      5;      