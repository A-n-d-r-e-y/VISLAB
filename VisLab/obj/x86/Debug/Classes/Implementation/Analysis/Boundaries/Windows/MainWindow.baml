   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35`Dclr-namespace:System.Windows.Controls;assembly=PresentationFrameworkSystem.Windows.Controls  5   	   T PVisLab, Version=1.1.4521.27586, Culture=neutral, PublicKeyToken=2f49d7e83098ceed1clr-namespace:VisLab.ControlsVisLab.Controls 5   	   -clr-namespace:VisLab.StylesVisLab.Styles 5   	   /clr-namespace:VisLab.WindowsVisLab.Windows 5   	      VisLab.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5   	   \winControlsDclr-namespace:System.Windows.Controls;assembly=PresentationFramework      5   	   2controlsclr-namespace:VisLab.Controls      5   	   .stylesclr-namespace:VisLab.Styles      5   	   -thisclr-namespace:VisLab.Windows      5   	   -   5        $	��350��5   M   $	��525��6Z     � WindowState$  	Maximized=�5	   	    � Icon$) #/VisLab;component/Styles/VISSIM.ICO��6!    � Title 5   	   �� *Project.Name+ �� StringFormat$ Vissim Laboratory: {0}�� � 	Resources 5       �� MergedDictionaries �� Source    booleanToVisibilityConverter!  VisLab.Styles.MathConverter  mathConverter�� 5   
    5      �� 5      $ /Styles/Styles.xaml?�6%   %:   &	        &	      �� 5      67     5      6#   5      .��5      � -   5        $��
LayoutRoot��5        lgbDefaultBackground#��[ 6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��ccBottomLayerHost��5      ��62   �� *FocusedNode.Control+�� 5   
   -   5        $��tbkLeftArrow��5      $��17,0,0,0q�6(   $��35��6:   $��Center=�6p   $
��Left=�6�   
�������$��12(�6�    J� RenderTransformOrigin$ 0.5 0.5&�6�   ��6H   �� *LeftDirectionModelName+y�6�   �� *CanNavigateLeft+	 �� 	Converter#	 [  z�5      V� 5      .~�5       �� 
 �� Angle$	
 270Q�6&   T� 5!       T� X 6)   ��  �� ElementName$ tbkLeftArrow�� �� Path$ ActualWidth�#	 [  �� ConverterParameter$ @VALUE/2-@VALUE��5"      5$      &   VisLab.Controls.LeftArrowControl  5%   
   -   5          Name$ leftArrowControl��5%   $   &   VisLab.Controls.LeftArrowControl   MouseUp$ LeftArrowControl_MouseUp��6�   $��30,0,0,0q�6>   $
��Left=�6P   $��Center=�6k   $��60��6�   $��30��6�   y�6�   �� *CanNavigateLeft+#	 [  6�   '  !VisLab.Controls.RightArrowControl  5&   
   -   5          Name$ rightArrowControl��5&   %   '  !VisLab.Controls.RightArrowControl   MouseUp$ RightArrowControl_MouseUp��6i   $��0,0,30,0q�6@   $��60��6R   $��30��6^   $��Right=�6�   $��Center=�6�   y�6�   �� *CanNavigateRight+#	 [  �� 5'   
   -   5        $��tbkRightArrow��5'      $��0,0,17,0q�6)   $��35��6;   
�������$��12(�6�   $ 0.5 0.5&�6�   $��Right=�6�   $��Center=�6�   ��6I   �� *RightDirectionModelName+y�6�   �� *CanNavigateRight+#	 [  z�5(      V� 5)      .~�5*      �� $
 90Q�6&   T� 5+       6)   �� $ tbkRightArrow��$ ActualWidth�#	 [ $ @VALUE/2��5,      5.      �� 5/   
   -   5        $��topArrowPanel��5/      $	��Top=�6*   $��Center=�6B   y�6_   �� *CanNavigateTop+#	 [  .+�50      �� $��30��6   $��	0,10,0,-6q�6'   $��Center=�6a   $��Center=�6|   
�������$��12(�6�   ��6:   �� *TopDirectionModelName+6�   $  VisLab.Controls.UpArrowControl  51      $  VisLab.Controls.UpArrowControl   MouseUp$ topArrowButton_MouseUp��6=   $��45��6&   $��60��62   6=   52      �� 53   
   -	   5        $��bottomArrowPanel��53   (   $��	0,0,0,-30q�6   $��Bottom=�6@   $��Center=�6[   y�6x   �� *CanNavigateBottom+#	 [  .+�54      &   VisLab.Controls.DownArrowControl  &	   VisLab.Controls.DownArrowControl 	  MouseUp$ bottomArrowButton_MouseUp��6?   $��45��6(   $��60��64   6?   �� 55      $��30��6   $��Center=�6Q   $��Center=�6l   
�������$��12(�56   *   ��55   '   �� *BottomDirectionModelName+56   *   57      �� 58   
   -
   5        $��ccTopLayerHost��58      ��6/   �� *Shadow+�� 5:   
   $��1��6   $��5q�6    K� ZIndex$ 20000��6)   
������ ._�5;      �� -   5        $��	statusBar��5;      $��23��6)   $��1q�65   
������ .@�5<      �� ��5=      �� 5>      -   5         �� 
TargetName$ _blank��5>   $   $��http://www.morozov.lv?�68   .�6t   .�5?      �� ��6(   �� 
  VisLab.Windows.App 
  AssemblyCopyright �� Source# Z 5@      5B      �� 5C      6   �� 5D      ��5E      �� 5F      -   5        $ _blank��5F   $    �� TextDecorations$
 None{�68   $*��$http://vislab.morozov.lv/support.htm?�5G   ,   .�6_   .�5H      ��  �� Orientation$ 
Horizontal=�6)   .+�5I   "   �� $'��!/VisLab;component/Styles/help.png��6(   �� 5J   "   $
��Help��6,   $	��4,0q�68   5K      5L      5N      �� 5O      $��Right=�6_   ��6    ��  
  AssemblyVersion# Z 6_   5P      5Q      5R      5S      