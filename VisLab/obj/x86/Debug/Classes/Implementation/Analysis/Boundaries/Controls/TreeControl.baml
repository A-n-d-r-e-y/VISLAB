   M S B A M L   `   `   `  ����T  PVisLab, Version=1.1.4521.27586, Culture=neutral, PublicKeyToken=2f49d7e83098ceed-clr-namespace:VisLab.StylesVisLab.Styles  5      1clr-namespace:VisLab.ControlsVisLab.Controls  5      1clr-namespace:VisLab.ControlsVisLab.Controls  5      !    VisLab.Controls.TreeControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .stylesclr-namespace:VisLab.Styles     5      2controlsclr-namespace:VisLab.Controls     5      .thisclr-namespace:VisLab.Controls     5      -   5        $��Segoe UI)�5
   ]   
������   � 	Resources  5       �� MergedDictionaries �� Source!   VisLab.Controls.NodeControl �� 
TargetType!   VisLab.Controls.NodeControl   	AnalyzeOn   
AnalyzeOff   Select   Load   Delete	   Open$   VisLab.Styles.BooleanConverter   booleanConverter�� 5   
    5      �� 5      $ /Styles/Styles.xaml?�6%   %1  '        &	  
    �� 5      "  6   .�5      ;� 60    NodeControl_AnalyzeOn;� 5   1    NodeControl_AnalyzeOff;� 5   -    NodeControl_Select;� 5   +    NodeControl_Load;� 5   -    NodeControl_Delete;� 5   +   	 NodeControl_Open5        5      6&   5      .��5      � 
������ .+�5    
   � -   5        
������ .+�5!      �� -   5        $��cnvTree��5!      
 J� RenderTransformOrigin$
 0.5 0.5&�6$   $��Stretch=�6D   $��Stretch=�6b   z�5"      V� 5#      .~�5$      T� -   5        ttModel��5$   -   �� 5%      -   5        stModel�� �� ScaleX 5&   )   ��  �� ElementName$ slider�� �� Path$ Value� �� Mode$ TwoWay=� �� ScaleY 5'   )   �� $ slider��$ Value�$ TwoWay=�5(      5*      � 5+      $	��Top=�6   $
��Left=�6+   $��30q�6F    � RowDefinitions 5,      �� 5-      �� 5.      .+�50      �� $��22(�6   ��6*   �� *SelectedExperimentPath+�� 51      $��1��6   $��22(�6)   ��67   �� *LoadedExperimentPath+52      53      �� 54   
   -   5        $��slider��54       � Cursor$
 Hand��6   $��Bottom=�6-   $
��Left=�6H   $	��200��6c   $
��20,5q�55       � Minimum$	 0.5Q�6    � Maximum$ 10Q�6-    �� TickPlacement$ BottomRight=�6:    � Value$ 1Q�6V    �� TickFrequency$	 0.5Q�6`   �� 56   
   -   5        $��ccShadowHost��56      $ Arrow��6H   ��6-   �� *Shadow+6H   � 57   
   -   5        $��modelControlGrid��57      $	��Top=�6'   $��Right=�6?   $��30q�6[    58      �� 59      $
��Auto �6    �� 5:      $��* �6     � ColumnDefinitions 5<      �� 5=      �� 5>      .+�5A      �� $��36(�6.   $	��Top=�6<   $
��15,0q�6U   $
��Wrap=�6c    �� TextAlignment$ Center=�6w   $��WordEllipsis=�6�   ��6   �� *Name+6�   �� 5B      -	   5        $��button��5B   6   $��1��6   $��2��6%    � ToolTip$ Load VISSIM��6D   $��70��6Z   $��70��6e     CircleButton#��[ 6q   $
��Load��6�   $��16(�6�   
���   ����	#FF7C7C7C$
 Hand��5C   @   {�6N   �� *IsSimulationRunning+ �� 	Converter# [  #   VisLab.Controls.PlayerControl  5D      -
   5          Name$ player��5D   %   #   VisLab.Controls.PlayerControl   Click$ palyer_Click��65   $��1��6J   $��60��6W   $	��120��6c   $��Center=�6o   $	��Top=�6�   {�6�   �� *
IsLoaded+5E      5F      5G      