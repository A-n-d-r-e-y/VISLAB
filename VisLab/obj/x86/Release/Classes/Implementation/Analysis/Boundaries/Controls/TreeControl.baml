   M S B A M L   `   `   `  ���� T  PVisLab, Version=1.0.4515.16253, Culture=neutral, PublicKeyToken=2f49d7e83098ceed-clr-namespace:VisLab.StylesVisLab.Styles  1clr-namespace:VisLab.ControlsVisLab.Controls  1clr-namespace:VisLab.ControlsVisLab.Controls  !    VisLab.Controls.TreeControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .stylesclr-namespace:VisLab.Styles     2controlsclr-namespace:VisLab.Controls     .thisclr-namespace:VisLab.Controls     -   $��Segoe UI)�
������   � 	Resources   �� MergedDictionaries �� Source!   VisLab.Controls.NodeControl �� 
TargetType!   VisLab.Controls.NodeControl   	AnalyzeOn   
AnalyzeOff   Select   Load   Delete	   Open$   VisLab.Styles.BooleanConverter   booleanConverter��  �� $ /Styles/Styles.xaml?�%�   '        &	  �     �� "  .�;�  NodeControl_AnalyzeOn;�  NodeControl_AnalyzeOff;�  NodeControl_Select;�  NodeControl_Load;�  NodeControl_Delete;� 	 NodeControl_Open  .��� 
������ .+�� -   
������ .+��� -   $��cnvTree��
 J� RenderTransformOrigin$
 0.5 0.5&�$��Stretch=�$��Stretch=�z�V� .~�T� -   ttModel���� -   stModel�� �� ScaleX ��  �� ElementName$ slider�� �� Path$ Value� �� Mode$ TwoWay=� �� ScaleY �� $ slider��$ Value�$ TwoWay=�� $	��Top=�$
��Left=�$��30q� � RowDefinitions �� �� .+��� $��22(����� *SelectedExperimentPath+�� $��1��$��22(����� *LoadedExperimentPath+�� -   $��slider�� � Cursor$
 Hand��$��Bottom=�$
��Left=�$	��200��$
��20,5q� � Minimum$	 0.5Q� � Maximum$ 10Q� �� TickPlacement$ BottomRight=� � Value$ 1Q� �� TickFrequency$	 0.5Q��� -   $��ccShadowHost��$ Arrow������ *Shadow+� -   $��modelControlGrid��$	��Top=�$��Right=�$��30q� �� $
��Auto ��� $��* � � ColumnDefinitions �� �� .+��� $��36(�$	��Top=�$
��15,0q�$
��Wrap=� �� TextAlignment$ Center=�$��WordEllipsis=����� *Name+�� -	   $��button��$��1��$��2�� � ToolTip$ Load VISSIM��$��70��$��70��  CircleButton#��[ $
��Load��$��16(�
���   ����	#FF7C7C7C$
 Hand��{��� *IsSimulationRunning+ �� 	Converter# [  #   VisLab.Controls.PlayerControl  -
     Name$ player��#   VisLab.Controls.PlayerControl   Click$ palyer_Click��$��1��$��60��$	��120��$��Center=�$	��Top=�{��� *
IsLoaded+