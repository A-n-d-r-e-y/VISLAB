   M S B A M L   `   `   `  ���� 
  VisLabV    PVisLab.Classes.Implementation.Analysis.Boundaries.Controls.CountersReportControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     $��Segoe UI)�$��16(�
������ $	��350��  � 	Resources   �� MergedDictionaries �� Source��  �� $  /Styles/DataGridStyle.xaml?�%    .��� .+�� -   $��
flyingGrid��$
��40,6q�$��Stretch=�$��Stretch=�   NormalBorderBrush#��[   J� OpacityMask �	#D4000000 � RowDefinitions �� $
��Auto ��� $��* �.+��  �� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $	��120��$	��200���� .+��� -   $��	tbkHeader��$��1��$��Center=�$��Right=�$��0,5,15,0q�$
��Bold"�$��18(��� $��Columns��$��3��$
��10,5q�.���� .+�	� -   $��rbSelectAll��$��
Select all�� d� 	IsChecked$
 TrueY�	� -   $��rbOnlyWithData��$��Only with data��	� -   $��rbCustom��.����  �� Orientation$ 
Horizontal=�.+��� $��Custom��$��Center=��� -   $��Change��$��5,0,0,0q�$��50��$��10(�{���  �� ElementName$ rbCustom��	 �� Path$	 	IsChecked�� $��1��$��1��$��Center=�$	��6,0q� �� $
��Auto ��� $
��Auto ��� $��* � �� $
��Auto �.+��� $��Filter: ��$��Center=��� -   $��cbxColumnsList��$��1��$��1q����� .R��� $��Center=����� *Header+�� -   $��	tbxFilter��$��2��$��1q�$	��100��� $��2��$��1��$��Bottom=�$	��6,0q�$��Right=� �� �� .+��� -	   $��btnExportToExcel��$��Export to Excel��$��2q�$	��120���� -
   $��btnClose��$��1��$��Close��$��2q�$	��120��&   System.Windows.Controls.DataGrid  -   $��datagrid��$��1��$��10q�
   AutoGenerateColumns
 .    GridLinesVisibility$ 
Horizontal=�   HeadersVisibility$ Column=�   SelectionMode$ Single=�   
IsReadOnly .    CanUserResizeRows .     ClipboardCopyMode$ IncludeHeader=�}�.  ���� �� -   $��ccTopLayerHost������ *Shadow+