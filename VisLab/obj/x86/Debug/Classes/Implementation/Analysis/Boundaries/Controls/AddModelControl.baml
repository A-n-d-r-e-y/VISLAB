   M S B A M L   `   `   `  ����
  VisLab%    VisLab.Controls.AddModelControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        $	��240��5   7   $	��550��6D   $��16(�6P   $��Segoe UI)�6^   
������   � LayoutTransform  5
      �� 5   
   -   5        stBorder�� �� ScaleX$ 1Q�5   +    �� ScaleY$ 1Q�66   .��5      ��  �� CornerRadius$ 10��6   $��3q�6   
�������
�������._�5   
   � $��Center=�6   $��30q�6*    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      	� -   5        $��rbCreateNewModel��5      $��Center=�62    	� 	GroupName$ group��6M    d� 	IsChecked 6_   �� *IsNewModelNameActive+.��5      �� $��Center=�6   $��Create New: ��67   5      �� 5      -   5        $��tbxModelName��5      $��1��6*   $��1q�6:   {�6E   ��  �� ElementName$ rbCreateNewModel��	 �� Path$	 	IsChecked���6�   �� *NewModelName+
 �� Mode$
 TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5      $��1��6   $��2��6%   $��Stretch=�69   $��20,11q�6W   $��2��6f   	� 5       -   5        $��rbLoadFromFile��5    '   $��2��6   $��Center=�6=   $ group��6X    6j   �� *IsLoadModelFileActive+.��5!      �� $��Center=�6   $��Load From File: ��67   5"      � 5#      $��2��6   $��1��6     5$      �� 5%      $��* �6'   �� 5&      $
��Auto �6'   .+�5(      �� -   5        $��tbxFileName��5(      $��1q�6-    �� 
IsReadOnly . {�68   �� $ rbLoadFromFile��$	 	IsChecked���6y   �� *LoadModelFromFile+$
 TwoWay=�$ PropertyChanged=�6�   �� 5)      -   5        $��	btnBrowse��5)      $��1��6+   $	��100��6;   $��5,1,1,1q�6G   $��
Browse ...��6X   6m   5*      � 5+      $��3��6   $��2��6    $
��20,6q�64   $��Right=�6B   {�6^   �� $ rbLoadFromFile��$	 	IsChecked� 5,      �� 5-      $
��Auto �6'   �� 5.      $
��Auto �6'   �� 5/      $��* �6'   $	��200��61   .+�51      �� $��with new Model name: ��6   $��Center=�69   �� 52      -   5        $��cbAltModelName��52   +   $��1��6   $��Right=�6A   $��Center=�6]   $	��5,1q�6x    6�   �� *IsAltModelNameActive+�� 53      -	   5        $��tbxAltModelName��53      $��2��61   $��1q�6A   {�6L   �� $ cbAltModelName��$	 	IsChecked���6�   �� *AltModelName+$
 TwoWay=�$ PropertyChanged=�54      �� 55      $��4��6   $��2��6&    �� Orientation$ 
Horizontal=�6:   $��Right=�6S   $��
10,17,10,0q�6o   .+�56      �� -
   5        $��btnAddModel��56      $	��Add��6,   $	��140��6:   $��1q�6F   {�6Q   �� *IsValidated+�� 57      -   5        $��	btnCancel��57      $��Cancel��6*   $	��140��6;   $��1q�6G   58      59      5:      5;      