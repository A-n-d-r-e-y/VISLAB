   M S B A M L   `   `   `  ����T  PVisLab, Version=1.1.4521.27586, Culture=neutral, PublicKeyToken=2f49d7e83098ceed-clr-namespace:VisLab.StylesVisLab.Styles  5      #    VisLab.Controls.PlayerControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .stylesclr-namespace:VisLab.Styles     5      
������   � Cursor$  Arrow��5   O    � 	Resources 5
       �� MergedDictionaries �� Source$   VisLab.Styles.BooleanConverter   booleanConverter�� 5   
    5      �� 5      $ /Styles/Styles.xaml?�6%   %   &	          5      6&   5      .��5      � -   5        $��gridMain��5      
������  5   
    �� 
TargetType �� Property �� Value��%�  'A�       'F� �     �� 5      " A�6   .�5      ��  � ��
 �   �6)   �� 5       �@�Cursor$
 Hand��6+   �� 5       �@J�OpacityMask �	#CA00000060   -   5        5      �� 5      " F�6   .�5       ��  � ��
 �����6)   �� 5!       �@�Cursor$
 Hand��6+   �� 5"       �@J�OpacityMask �	#CA00000060   -   5        5'      5(       � ColumnDefinitions 5)   
   �� 5*      �� 5+      �� 5,      �� 5-      $��0 �6    � RowDefinitions 5/   
   �� 50      $��4* �6   �� 51      $��2* �6   .+�53   
   A� -   5        $��elPlay��53      $��3q�6   $��0��6+   	 � ToolTip	 6;   �� 
 �� ElementName$
 pathPlay�� �� Path$ ToolTip�{�6r   �� *IsSimulationRunning+ �� 	Converter# [  A� 54   
   -   5        $��elMulti��54      $��3q�6   $��1��6,   	 6<   �� $
 	pathMulti��$ ToolTip�{�6t   �� *IsSimulationRunning+# [  A� 55   
   -   5        $��elStop��55      $��3q�6   $��2��6+   	 6;   �� $
 pathStop��$ ToolTip�{�6r   �� *IsSimulationRunning+A� 56   
   -   5        $��elPause��56      $��3q�6   $��3��6,   	 6<   �� $
 	pathPause��$ ToolTip�F� 57   
   -   5         F�Name$ pathPlay��57      $	 Simulation Continuous��6!   $��4q�6A   $��0��6L    �� Stretch$
 Fill=�6\   �����,U
��5%�,U
0�h#�ȯ;�lO$��
H��"��'�	H��"@�	�{u#@�	�lO$�@�	�])%�'�	� �%�
� �%�r� �%��"�])%��"�lO$Ӱ�"�{u#rH��"�
H��"{�6k   �� *IsSimulationRunning+# [  6�   F� 58   
   -	   5        $ 	pathMulti��58      $	 Simulation Multirun��6"   $��4q�6@   $��1��6K   $
 Fill=�6[   �����P\�	��5%�P\�	0�h#ш[�
��M$�o�
��M$�P��
��5%�P��
0�h#ш���M$ш���M$��
H��"��'�	H��"@�	�{u#@�	�lO$�@�	�])%�'�	� �%�
� �%�r� �%��"�])%��"�lO$Ӱ�"�{u#rH��"�
H��"{�6j   �� *IsSimulationRunning+# [  6�   F� 59   
   -
   5        $ pathStop��59      $	 Stop Simulation��6!   $��4q�6;   $��2��6F   $
 Fill=�6V   �����#
 c�$�#
��#�H+��#�H+ c�$��
H��"��'�	H��"@�	�{u#@�	�lO$�@�	�])%�'�	� �%�
� �%�r� �%��"�])%��"�lO$Ӱ�"�{u#rH��"�
H��"{�6e   �� *IsSimulationRunning+6�   F� 5:   
   -   5        $ 	pathPause��5:      $	 Simulation Single Step��6"   $��4q�6C   $��3��6N   $
 Fill=�6^   ����А��	���$ѐ��	p4�#�Ȕ�
p4�#�Ȕ�
���$А��
���$ѐ��
p4�#�ȯ;p4�#�ȯ;���$��
H��"��'�	H��"@�	�{u#@�	�lO$�@�	�])%�'�	� �%�
� �%�r� �%��"�])%��"�lO$Ӱ�"�{u#rH��"�
H��"6m   �� 5;   
   $��1��6   $��0��6!   $
��Play��61   $��Center=�6t   	 6=   �� $
 pathPlay��$ ToolTip�6t   �� 5<   
   $��1��6   $��1��6!   $��Multi��61   $��Center=�6v   	 6>   �� $
 	pathMulti��$ ToolTip�6v   �� 5=   
   $��1��6   $��2��6!   $
��Stop��61   $��Center=�6t   	 6=   �� $
 pathStop��$ ToolTip�6t   �� 5>   
   $��1��6   $��3��6!   $��Pause��61   $��Center=�6v   	 6>   �� $
 	pathPause��$ ToolTip�6v   5?      5@      