   M S B A M L   `   `   `  ���� T  PVisLab, Version=1.0.4515.16253, Culture=neutral, PublicKeyToken=2f49d7e83098ceed-clr-namespace:VisLab.StylesVisLab.Styles  �Hclr-namespace:VisLab.Classes.Implementation.Analysis.Boundaries.Controls:VisLab.Classes.Implementation.Analysis.Boundaries.Controls  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\Bclr-namespace:System.Windows.Shapes;assembly=PresentationFrameworkSystem.Windows.Shapes "    VisLab.Controls.ModelControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .stylesclr-namespace:VisLab.Styles     YthisHclr-namespace:VisLab.Classes.Implementation.Analysis.Boundaries.Controls     UshapesBclr-namespace:System.Windows.Shapes;assembly=PresentationFramework     -   $��modelControl��$��Segoe UI)�
������   � 	Resources   �� MergedDictionaries �� Source-   'VisLab.Styles.StringFormattingConverter   stringFormattingConverter!   VisLab.Styles.MathConverter  mathConverterV   PVisLab.Classes.Implementation.Analysis.Boundaries.Controls.DataCollectionControl �� 
TargetTypeV   PVisLab.Classes.Implementation.Analysis.Boundaries.Controls.DataCollectionControl   	MouseDown   	MouseMove   
MouseEnter   
MouseLeave   MouseUp��  �� $  /Styles/DataGridStyle.xaml?�%:  &	        &	      '  
     'A� �     '� �         �� "  .�;� # DataCollectionControl_MouseDown;� # DataCollectionControl_MouseMove;� $  DataCollectionControl_MouseEnter;� $  DataCollectionControl_MouseLeave;� ! DataCollectionControl_MouseUp�� " A�.�-   �� " �.�-   .��� .+�� -   
������ .+�A� -   $��ellipse��$��40��$��40��
�������$��1���� -   $��cnvModel��
������ 	 J� RenderTransformOrigin$	 0.5 0.5&�$��Stretch=�$��Stretch=�z�V� .~�T� -   ttModel��
 T� X$
 0Q� T� Y$ 0Q��� -   stModel�� �� ScaleY ��  �� ElementName$ slider�� �� Path$ Value� �� Mode$ TwoWay=� �� ScaleX �� $ slider��$ Value�$ TwoWay=��� $ -1Q�.+�A�  �� Left$ 0��
 �� Top$ 0��
���  ��$��20��$��20���� -	   $��panel��$��Bottom=�$��Right=�$��5q�.+�d� -
   $��btnFixedPoints��$��Fixed boxes���� -   $��tbkXCoordinate���� -   $��tbkYCoordinate���� ���� $ stModel��$ ScaleX��� -   $��spModelDescription��$
��Left=�$��20q�.+��� $��32(���`�  �� StringFormat$ {0}#{1}��.1��� $ modelControl��$ 	ModelName��� $ modelControl��$ ExperimentNumber��� ���� *Data.NumberOfRuns+$ Number of runs: {0}���� ���� *Data.Description+$ Description: {0}��� -   $��countersGrid��$	��Top=�$��Right=�$��20,20,145,0q�  NormalBorderBrush#��[  J� OpacityMask �	#D4000000 � RowDefinitions �� $
��Auto ��� $��* �z�T� -   
ttDataGrid��$
 0Q�$ 0Q�.+��� -   $��checkGrouping��$��Grouping��$	��Top=�$��Right=�$��4q��� -   $��	checkMini��$
��Mini��$	��Top=�$
��Left=�$	��8,4q�&   System.Windows.Controls.DataGrid  -   $��
dgCounters��$��1��$��5,20,5,5q�$	��360��$��16(�
������    AutoGenerateColumns .     RowDetailsVisibilityMode$ 	Collapsed=�$	��500��   SelectionMode$ Single=�   
IsReadOnly .    CanUserResizeRows .     ClipboardCopyMode$ IncludeHeader=�$��76��   GridLinesVisibility$ 
Horizontal=�   HeadersVisibility$ Column=�$ �� IsSynchronizedWithCurrentItem$
 TrueY�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn    Header$  MeasurId��!   
Visibility$! Hidden=�1  +System.Windows.Controls.DataGridBoundColumn"   Binding" �� *
MeasurId+  $  Point Id��" �� *	PointId+  $  
Point Name��#   Width5	  /System.Windows.Controls.DataGridLengthConverter$# *	 " �� *Name+  $
  Link��" �� *Link+  $  Coord��$! Hidden=�" �� *Coord+$ �� 
GroupStyle$ (
  "System.Windows.Controls.GroupStyle
    GroupHeaderStyle% 
  ContainerStyle#% [ & 
  Panel& �� .R�>  8System.Windows.Controls.Primitives.DataGridRowsPresenter  ' � LayoutTransform' �� -   stBorder��$ 1Q�$ 1Q�� -   $��trTimesGrid��$	��Top=�$��Right=�$��20,90,145,0q�#��[  �	#D4000000z�T� -   ttTrTimesGrid��$
 0Q�$ 0Q�.+�  -   $��	dgTrTimes��$��1��$��5,10,5,5q�$	��360��$��16(�
������  .  $ 	Collapsed=�$	��500��$ Single=� .  .  $ IncludeHeader=�$ 
Horizontal=�$ Column=�$
 TrueY�   $  No��" �� *No+  $  Section Name��$# *	 " �� *Name+  $  FromLink��" �� *
FromLink+  $  ToLink��" �� *ToLink+  $  Coord��$! Hidden=�" �� *Coord+' �� -   stTrTimesGrid��$ 1Q�$ 1Q��� -   $��slider��( � Cursor$
( Hand��$��Bottom=�$
��Left=�$	��400��$
��20,5q�) � Minimum$
) 0.05Q�* � Maximum$* 40Q�+ �� TickPlacement$+ BottomRight=�, � Value$, 1Q�- �� TickFrequency$	- 0.5Q�