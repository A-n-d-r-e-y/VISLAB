   M S B A M L   `   `   `  ����T  PVisLab, Version=1.1.4521.27586, Culture=neutral, PublicKeyToken=2f49d7e83098ceed�Hclr-namespace:VisLab.Classes.Implementation.Analysis.Boundaries.Controls:VisLab.Classes.Implementation.Analysis.Boundaries.Controls  5      | xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting 5      M    GVisLab.Classes.Implementation.Analysis.Boundaries.Controls.ChartControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      YthisHclr-namespace:VisLab.Classes.Implementation.Analysis.Boundaries.Controls     5      �toolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5      $��Segoe UI)�5      .��5
      �� $��2q�6     �� CornerRadius$  10��6   $��3q�6*   
�������
�������._�5   
   � -   5        $
��grid��5      $	��400��6   $	��700��6+    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      � $��Stretch=�6   $��
10,10,10,0q�61    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   .+�5      �� $��Selected Counter: ��6   $��Center=�66   �� 5      -   5        $��cbxMeasures��5      $��1��6.    �� SelectedIndex$ 0��6>   $��5q�6P   $��70��6[   6i   �� 5      $��2��6   $��Expected value: ��6,   $��Right=�6D   $��Center=�6`   $
��Wrap=�6{   �� 5      -   5        $��tbxExpectedValue��5      $	��200��62   $��Right=�6A   $��3��6]   $	��200��6m   $��5q�6|   �� 5      -   5        $��btnAddExpectedValue��5      $��4��64   $	��Add��6D   $��60��6R   $��5,5,20,5q�6]   5       �� 5!      $��1��6   $��10q�6(   .��5"      >  8System.Windows.Controls.DataVisualization.Charting.Chart  -   5        $��chart��5"          LegendTitle$ Counters��6-   $	��300��6D   5#      �� 5%      $��2��6   $��	15,0,0,-5q�6&   $��16(�69   $
��Bold"�6G   ��6Y   ��  �� ElementName$ cbxMeasures�� �� Path$
 Text� �� StringFormat$ Counter N{0}��&   System.Windows.Controls.DataGrid  5&      -   5        $��datagrid��5&   $   $��3��6   $��10q�6L      GridLinesVisibility$ 
Horizontal=�6X   	   HeadersVisibility$	 Column=�6y   
   SelectionMode$
 Single=�5'         
IsReadOnly .    CanUserResizeRows .     AutoGenerateColumns .  }�.  $	��200��6�   ��5&   4   ��    Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Experiment��6)      MinWidth$	 100Q�6Y      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6h   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *	RowName+6h     5*      $
 Runs��6)    67   �� *Value.NumberOfRuns+  5+      $
 Mean��6)    67   �� *Value.Value+  5,      $ Std.Dev.��6)    6;   �� *Value.StandardDeviation+  5-      $ Conf.lim.90%��6)    6?   �� *Value.Confidence90+  5.      $ Conf.lim.95%��6)    6?   �� *Value.Confidence95+  5/      $ Conf.lim.99%��6)    6?   �� *Value.Confidence99+51      �� 52      -   5        $��btnClose��52   "   $��4��6   $��Center=�62   $��Right=�6M   $��	0,0,30,15q�6i   $	��120��6|   $��Close��6�   53      54      55      