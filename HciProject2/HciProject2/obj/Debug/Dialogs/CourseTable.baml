   M S B A M L   `   `   `  ����F  BHciProject2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HciProject2.DialogsHciProject2.Dialogs  5   	   %    HciProject2.Dialogs.CourseTable   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:HciProject2.Dialogs     5   	   -   5          � Title$  CourseTable��5   	   $	��800��6   $	��800��6*    � 
ResizeMode$ NoResize=�5
   	    � 	Resources 5       	  AData��%   &	        S� 5   
   6-   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0��6    � ColumnDefinitions 5      �� 5      $	��100 �6#   61   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/    5       �� 5!      $��* �6    .+�5#      �� $
��Bold"�6   $��0��6&   $��0��66   .��6C   
Filters:6M   �� 5$      -   5        $��1��5$      $��0��6$   .��6R   Name:6Y   �� 5%      -   5        $	��naz��5%      $��2��6!   $��0��61   �� 5&      -   5        $��3��5&      $��0��6$   .��6R   Description:6`   �� 5'      -   5        $
��desc��5'      $��4��6"   $��0��62   5)      � 5*   
   $��1��6   $��0��6    5,      �� 5-      $	��100 �6#   61   �� 5.      $��* �6#   6/   �� 5/      $	��150 �6#   61    51      �� 52      $��* �6    .+�54      �� $
��Bold"�6   $��0��6&   $��0��66   .��6C   	SEARCH:6L   �� 55      -   5         ��Name$ search��55       � ToolTip$X REnter query like :ColumnName ColumnValue. Example :Name name. :All for all courses��6&   $��20��6�   $��1��6�   $��0��6�   �� 56      -   5         ��Name$ 	btnSearch��56      $��3��6(   $��0��68   $��Search��6E   $ Click to search��6V   $��10q�6�   $
��20,5q�6�   57      &   System.Windows.Controls.DataGrid  58   
   -   5          Name$ dgrMain��59   Q   $��2��6   $��0��6   $" Select item to modify/delete��6*   	   
IsReadOnly	 . 
   AutoGenerateColumns
 .  $ �� IsSynchronizedWithCurrentItem$
 TrueY�5;   )   ��5:      �� *MainWindow.courses+   Columns 5<      0  *System.Windows.Controls.DataGridTextColumn  5=      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6J     5>      $
 Name��6%    63   �� *Naziv+6O     5?      $
 Date��6%    63   �� *Datum+6O     5@      $ Description��6%    6:   �� *Opis+6U   5C      � 5D   
   $��3��6   $��0��6   .+�5F      �  5G      �� 5H      $��* �6'   63   �� 5I      $��* �6'   63   �� 5J      $
��Auto �6'   66    5L      �� 5M      $
��Auto �6$   64   �� 5N      $
��Auto �6$   64   .+�5Q      � $��0��6   $��0��6$    5R      �� 5S      $	��150��6+   �� 5T      $��* �6+    5V      �� 5W      $��25 �6(   �� 5X      $��25 �6(   �� 5Y      $��25 �6(   �� 5Z      $��25 �6(   �� 5[      $
��Auto �6(   .+�5]      �� $ Sets id.��6   $��0��6/   $��0��6?   .��6L   Id:6Q   �� 5^      -	   5        $��Id��5^       �� 	MaxLength$	 150��6(   $��1��68   $��0��6H   {�.  ��6U   ��  �� Path$ Id�6n   �� 5_      $ Sets name .��6   $��0��62   $��1��6B   .��6O   Name:6V   �� 5`      -
   5        $��naziv��5`      $	 150��6+   $��1��6;   $��1��6K   {�.  ��6X   �� $ Naziv�6t   �� 5a      $ Sets description.��6   $��0��68   $��2��6H   .��6U   Description:6c   �� 5b      -   5        $
��opis��5b      $	 250��6*   $��1��6:   $��2��6J   {�.  ��6W   �� $
 Opis�6r   5d      � 5f      $��1��6   $��3��6$   .+�5g      �� $��0��6!   $��2��61    �� Orientation$ 
Horizontal=�6>   $��RightToLeft=�6W   
�������.+�5i      �� -   5        $��	btnDelete��5i   !   $��Delete��62   $ Delete selected item��6C   $��10q�6z   $
��20,5q�6�   �� 5j      -   5        $��btnSave��5j   !   $
��Save��60   $ Save changes��6?   $��10q�6l   $
��20,5q�6x   �� 5k      -   5        $��	btnCancel��5k   !   $ Cancel changes��62   $��Cancel��6K    �� IsCancel . $��10q�6�   $
��20,5q�6�   �� 5l      -   5        $��btnAdd��5l   !   $��Add new��6/   $ Add new subject��6A   $��10q�6p   $
��20,5q�6|   5m      5n      5p      5q      5r      5s      