   M S B A M L   `   `   `  ����T  PVisLab, Version=1.1.4521.27586, Culture=neutral, PublicKeyToken=2f49d7e83098ceed5clr-namespace:VisLab.PropertiesVisLab.Properties  5      V    PVisLab.Classes.Implementation.Analysis.Boundaries.Controls.CountersReportControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4settingsclr-namespace:VisLab.Properties     5      $��Segoe UI)�5   7   $��16(�6M   
������ $	��350��6t     � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $  /Styles/DataGridStyle.xaml?�6%   %    5      .��5      � .+�5   
   � -   5        $��
flyingGrid��5      $
��40,6q�6   $��Stretch=�6/   $��Stretch=�6K      NormalBorderBrush#��[  5       J� OpacityMask �	#D4000000 � RowDefinitions 5      �� 5      $
��Auto �6   �� 5      $��* �6   .+�5      �  5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5       $	��120��6#   $	��200��62   �� 5!      .+�5#      �� -   5        $��	tbkHeader��5#   (   $��1��6   $��Center=�69   $��Right=�6T   $��0,5,15,0q�6p   $
��Bold"�6�   $��18(�6�   �� 5$      $��Columns��6   $��3��6(   $
��10,5q�69   .��5%      �� .+�5&      	� -   5        $��rbSelectAll��5&   "   $��
Select all��65    d� 	IsChecked$
 TrueY�6h   	� 5'      -   5        $��rbOnlyWithData��5'   "   $��Only with data��68   6Q   	� 5(      -   5        $��rbCustom��5(   &   .��5)      ��  �� Orientation$ 
Horizontal=�6)   .+�5*   "   �� $��Custom��6,   $��Center=�6:   �� 5+   "   -   5        $��Change��5+   )   $��5,0,0,0q�6:   $��50��6K   $��10(�6V   {�6d   ��  �� ElementName$ rbCustom��	 �� Path$	 	IsChecked�6�   5,      5-      5.      5/      � 50      $��1��6   $��1��6#   $��Center=�60   $	��6,0q�6K    51      �� 52      $
��Auto �6'   �� 53      $
��Auto �6'   �� 54      $��* �6'    56      �� 57      $
��Auto �6$   .+�59      �� $��Filter: ��6    $��Center=�60   �� 5:      -   5        $��cbxColumnsList��5:   /   $��1��6   $��1q�6E   ��5;      �� 5<      .R�5=   "   �� $��Center=�6,   ��6G   �� *Header+5>      5@      �� 5A      -   5        $��	tbxFilter��5A      $��2��6/   $��1q�6?   $	��100��6J   6Y   5B      � 5C      $��2��6   $��1��6    $��Bottom=�60   $	��6,0q�6K   $��Right=�6X    5D      �� 5E      �� 5F      .+�5H      �� -	   5        $��btnExportToExcel��5H      $��Export to Excel��61   $��2q�6K   $	��120��6V   6b   �� 5I      -
   5        $��btnClose��5I   )   $��1��6   $��Close��69   $��2q�6I   $	��120��6T   5J      5K      &   System.Windows.Controls.DataGrid  5L      -   5        $��datagrid��5L   $   $��1��6   $��10q�64   
   AutoGenerateColumns
 .    GridLinesVisibility$ 
Horizontal=�6[      HeadersVisibility$ Column=�6|      SelectionMode$ Single=�5M         
IsReadOnly .    CanUserResizeRows .     ClipboardCopyMode$ IncludeHeader=�6V   }�.  5W      5X      �� 5Y   
   -   5        $��ccTopLayerHost��5Y      ��6/   �� *Shadow+5Z      5[      