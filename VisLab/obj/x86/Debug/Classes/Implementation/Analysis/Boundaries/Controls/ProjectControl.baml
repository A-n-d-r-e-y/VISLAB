   M S B A M L   `   `   `  ����
  VisLab$    VisLab.Controls.ProjectControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        $��Segoe UI)�5      
������   � 	Resources  5	       �� 
TargetType �� Property �� Value��%�   '��       �� 5
   
   " ��6   .�5      ��  � ��$ 50��6'   �� 5       � ��$	 300��6&   �� 5       � ��$ 3q�6'   �� 5       � ��$ 30(�6)   5      5      .��5      � .+�5   
   � -   5        
������ .+�5      �� -   5        $��canvas��5       J� RenderTransformOrigin$ 0.5 0.5&�6#   z�5      V� 5      .~�5      T� -   5        ttModel��5   -   �� 5      -   5        stModel�� �� ScaleX 5   :   ��  �� ElementName$ slider�� �� Path$ Value� �� Mode$ TwoWay=�	 �� ScaleY	 6y   �� $ slider��$ Value�$ TwoWay=�5      .+�5      � -   5        $
��grid��5      $��Center=�6#   $��Center=�6>   
 � ColumnDefinitions
 5      �� 5      �� 5      �� 5       � RowDefinitions 5!      �� 5"      $
��Auto �6(   �� 5#      $
��Auto �6(   �� 5$      $
��Auto �6(   .+�5'      �� $��	0,0,0,-20q�6>   $��2��6Q   $��Bottom=�6a   $��Center=�6|   $��40(�6�   ��6    �� *Project.Name+6�   � 5(      $��0��6   $��1��6+   $��Center=�68   $��Center=�6S    5)      �� 5*      �� 5+      .+�5-      �� -   5        $��btnCreateProject��5-   !   $��Create Project��69   �� 5.      -   5        $��btnOpenProject��5.   .   $��1��6!   $��Open Project��6D   5/      �� 50      $��1��6    $��3��60   $	��400��6A   $��20q�6N   #��["�6Z   � 51      $��2��6   $��1��6+   $��Center=�68   $��Center=�6S    52      �� 53      �� 54      .+�56      �� -	   5        $��listbox��56   "   $	��200��61   $	��384��6A   $	��400��6Q   
������ ��.  ��6y   �� *Project.Models.Model+��58      �� 59   "   .R�5:   &   �� $��22(�6F   ��60   �� *Name+6F   5;   #     5=       �� 
TargetType �� Triggers ]� 
Conditions �� Property �� Value P� Property P� Value   ListboxItemStyle��%�  '��       '��     �� 5>   "   " ��6(   .�5?   &   ��  � ��$ 1,2,1,1q�6?   �� 5@   &    � ��$ Stretch=�6L   �� 5A   &    � ��
 ��ح�6C   �� 5B   &    � �� 5C   *   �� 5D   .   " ��6>   .R�5E   2   � .+�5F   6   �� #��z��6=   �� 5G   6   ���	#BEFFFFFF$	��3,1q�6T   ._�5H   :   �  5I   >   �� 5J   B   �� 5K   B   .+�5M   >   �� $��2,1,2,0q�6E   $��0��6V   ���	#57FFFFFF6c   5N   ;   5O   7   �� 5P   6   $	��8,5q�6G   5Q   3    5R   2   ]� 5S   6    5T   :   �� 5U   >    �@J�IsMouseOver . 6_   �� 5V   >    �@��
IsSelected .  6^   ./�5X   :   ��  � ��
 ��ح�6W   5Y   7   P� 5Z   6    �@��
IsSelected . . �5[   :   ��  � ��
 � ���6W   5\   7   5^   /   5`   '   5a   #   �� 5b   "   " ��6(   .�5c   &   ��  � ��# �   6K   �� 5d   &    � ��$ 3,3,2,1q�6?   5e   #   5f      5g      �� 5h      $	��6,2q�6%    �� Orientation$ 
Horizontal=�62   $��1��6K   $��Right=�6X   {�6t   �� *
HasProject+.+�5i      �� -
   5        $��btnAddModel��5i   %   $��30��68   $	��140��6D   $��3q�6P   $��16(�6[   $��	Add Model��6i   �� 5j      -   5        $��btnDeleteModel��5j   %   $��30��6;   $	��140��6G   $��3q�6S   $��16(�6^   $��Delete Model��6l   6�   5k      5m      5n      5o      5p      �� 5q   
   -   5        $��slider��5q       � Cursor$
 Hand��6   $��Bottom=�6-   $
��Left=�6H   $	��200��6c   $
��20,5q�5r       � Minimum$	 0.5Q�6    � Maximum$ 10Q�6-    �� TickPlacement$ BottomRight=�6:    � Value$ 1Q�6V    �� TickFrequency$	 0.5Q�6`   5s      5v      