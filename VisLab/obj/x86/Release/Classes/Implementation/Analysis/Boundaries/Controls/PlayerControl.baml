   M S B A M L   `   `   `  ���� T  PVisLab, Version=1.0.4515.16253, Culture=neutral, PublicKeyToken=2f49d7e83098ceed-clr-namespace:VisLab.StylesVisLab.Styles  #    VisLab.Controls.PlayerControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .stylesclr-namespace:VisLab.Styles     
������   � Cursor$  Arrow�� � 	Resources  �� MergedDictionaries �� Source$   VisLab.Styles.BooleanConverter   booleanConverter��  �� $ /Styles/Styles.xaml?�%   &	          .��� -   $��gridMain��
������   �� 
TargetType �� Property �� Value��%�   'A�       'F� s     �� " A�.���  � ��
 �   ���  �@�Cursor$
 Hand����  �@J�OpacityMask �	#CA000000-   �� " F�.���  � ��
 �������  �@�Cursor$
 Hand����  �@J�OpacityMask �	#CA000000-    � ColumnDefinitions �� �� �� �� $��0 � � RowDefinitions �� $��4* ��� $��2* �.+�A� -   $��elPlay��$��3q�$��0��	 � ToolTip	 �� 
 �� ElementName$
 pathPlay�� �� Path$ ToolTip�{��� *IsSimulationRunning+ �� 	Converter# [  A� -   $��elMulti��$��3q�$��1��	 �� $
 	pathMulti��$ ToolTip�{��� *IsSimulationRunning+# [  A� -   $��elStop��$��3q�$��2��	 �� $
 pathStop��$ ToolTip�{��� *IsSimulationRunning+A� -   $��elPause��$��3q�$��3��	 �� $
 	pathPause��$ ToolTip�F� -    F�Name$ pathPlay��$	 Simulation Continuous��$��4q�$��0�� �� Stretch$
 Fill=������,U
��5%�,U
0�h#�ȯ;�lO$��
H��"��'�	H��"@�	�{u#@�	�lO$�@�	�])%�'�	� �%�
� �%�r� �%��"�])%��"�lO$Ӱ�"�{u#rH��"�
H��"{��� *IsSimulationRunning+# [  F� -	   $ 	pathMulti��$	 Simulation Multirun��$��4q�$��1��$
 Fill=������P\�	��5%�P\�	0�h#ш[�
��M$�o�
��M$�P��
��5%�P��
0�h#ш���M$ш���M$��
H��"��'�	H��"@�	�{u#@�	�lO$�@�	�])%�'�	� �%�
� �%�r� �%��"�])%��"�lO$Ӱ�"�{u#rH��"�
H��"{��� *IsSimulationRunning+# [  F� -
   $ pathStop��$	 Stop Simulation��$��4q�$��2��$
 Fill=������#
 c�$�#
��#�H+��#�H+ c�$��
H��"��'�	H��"@�	�{u#@�	�lO$�@�	�])%�'�	� �%�
� �%�r� �%��"�])%��"�lO$Ӱ�"�{u#rH��"�
H��"{��� *IsSimulationRunning+F� -   $ 	pathPause��$	 Simulation Single Step��$��4q�$��3��$
 Fill=�����А��	���$ѐ��	p4�#�Ȕ�
p4�#�Ȕ�
���$А��
���$ѐ��
p4�#�ȯ;p4�#�ȯ;���$��
H��"��'�	H��"@�	�{u#@�	�lO$�@�	�])%�'�	� �%�
� �%�r� �%��"�])%��"�lO$Ӱ�"�{u#rH��"�
H��"�� $��1��$��0��$
��Play��$��Center=�	 �� $
 pathPlay��$ ToolTip��� $��1��$��1��$��Multi��$��Center=�	 �� $
 	pathMulti��$ ToolTip��� $��1��$��2��$
��Stop��$��Center=�	 �� $
 pathStop��$ ToolTip��� $��1��$��3��$��Pause��$��Center=�	 �� $
 	pathPause��$ ToolTip�