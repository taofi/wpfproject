   M S B A M L   `   `   `  ����   WpfApp1    WpfApp1.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  
MainWindow��$	��650��$
��1100��.���    MainBackground#���    � RowDefinitions �� $
��45px ��� $��* � � ColumnDefinitions �� $��* �.+�� $��0��$��Stretch=�$��0��  SecondBackground#���   �� $
��80px ��� $��* ��� $��* �.+���  �� Orientation$ 
Horizontal=�.+��� $��10 0 0 0q�$��30��$��30�� �� CornerRadius$ 50��  InteractionColor#���  ._��� $��25��$��25������ *StyleChangeCommand+����  �� 
TargetType" ��.R�A�   	DarkColor#���  ��  �� Padding$ 0 0 0 10q�$��10 0 0 0q�$��30��$��30��$ 50��#���  ._��� $��25��$	��NaN��$��2,0,2,-5q����� *LangChangeCommand+���� " ��.R��� $��Center=�$��12(�  
ChosenLang#���  �� -   $��
AdminPanel��$��Center=�$
��Left=�$��1��.+��� $��10 0 0 0q�  
AdminPanel#���  $��120px������ *AdminPanelCommand+�� $ 
Horizontal=�$��Right=�$��2��y��� *	IsGuest+  BoolToVisibilityConverter �� 	Converter# [ .+���   	SignInStr#���  $	��120��$��0,0,10,0q����� *SignInCommand+�� $��1��$ 
Horizontal=�$��Center=�.+��� $��
Назад������ *GoBackCommand+{��� *IsCanGoBack+�� $��Вперёд������ *GoForwardCommand+{��� *IsCanGoForward+�� -   $��	UserPanel��$��Center=�$��Right=�$ 
Horizontal=�$��2��y��� *IsUser+# [ .+��� $��0,0,10,0q����� *Name+�� $ 50��#���  $��0,0,10,0q�$��Center=�$��Center=�}�. ._��� $��Center=�$��35��$��35�� � Cursor$
 Hand��	 � ToolTip$?	 9Кликните для просмотра профиля��$��0q�
������ ���� *NavigateToUserPageCommand+���� " ��.R��� $��UniformToFill=����� *
UserAvatar+  ImageSourceConverter# [ 
 J� Clip
 ��  �� RadiusX$ 50Q� �� RadiusY$ 50Q� �� Rect$ 	0,0,35,35��� #���  $��1��$��1��$��Visible=�.���� #���   $ 0��._� � -     �Name$ 	MainFrame��  � NavigationUIVisibility$ Hidden=�