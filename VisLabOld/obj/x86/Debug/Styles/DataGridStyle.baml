   M S B A M L   `   `   `  ����W  SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ColumnHeaderGripperStyle  �� 
TargetType �� Property �� Value �� 
TargetType �� Padding=   7System.Windows.Controls.Primitives.DataGridColumnHeader  NormalBorderBrush  	TextBrush � 	Resources  Key g� 	BeginTime �� 
TargetName �� TargetProperty	 O� KeyTime
 O� Value � ColumnDefinitions ��Name  NormalBrush �� Stretch  MouseOverBrush J� Opacity J� SnapsToDevicePixels � Cursor F�Name J� RenderTransformOrigin �� ScaleX �� ScaleY n�Name �� Triggers P� Property P� Value �� 
TargetName �� Angle N� EnterActions  HoverOn N� ExitActions  HoverOffG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter   Name J� IsHitTestVisible �� 
ResourceId  �� TypeInTargetAssembly&   System.Windows.Controls.DataGrid! �Name" � Points  ControlContentBrush 	 AlternateBackgroundBrush# �� CornerRadius$ ��Name% � RowDefinitions&   SelectAllCommand7  1System.Windows.Controls.DataGridHeadersVisibility
'   All( �� ConverterParameter!)   HeadersVisibilityConverter* �� 	Converter+ �� RelativeSource, �� AncestorType-  Name.   Column/ ��Name0 �� CanContentScroll1 ��Name2 �� ScrollableHeight3 � Maximum4 �� Orientation*5 �� #ComputedVerticalScrollBarVisibility6 �� ViewportHeight7 �� ViewportSize8 � Value9 �� Mode: �� ScrollableWidth,; �� %ComputedHorizontalScrollBarVisibility< �� ViewportWidth= �� Triggers)  #System.Windows.Controls.DataGridRow> �� Padding? � ToolTip@ ��Name?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid 
 
FocusBrushA �� RadiusXB �� RadiusY  MouseOverHighlightBrush  PressedBrush  PressedBorderBrush?  9System.Windows.Controls.Primitives.DataGridCellsPresenterC   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenterD   DetailsVisibility$E   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientationF   Vertical#G   RowDetailsScrollingConverter:	  4System.Windows.Controls.Primitives.DataGridRowHeader
H   RowI P� 
SourceNameJ ��Name  SelectedOff  
SelectedOn*
  $System.Windows.Controls.DataGridCell  bool2VisibilityConverter  RowHeaderGripperStyle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %�(  &	        '       7  7  7  7   7   7  7  '  �
    (�� �    5q      $" DataGridSelectAllButtonStyle��"   )'  /    7  7  7 	 7  7  '  �    7  7  7  7  '
  �"    7  7  &	 �&    &	 �&    �� 5      "  n�6-   .�5   
   ��  � ��$ 5��6"   �� 5   
    � ��
 ���� 6'   �� 5	   
    �@�Cursor$ SizeWE��6#   �� 5
   
    � �� 5      �� 5      " n�6"   .R�5      �� #��z��6   # z��6G   5      5      5      �� 5      "    6   .�5   
   ��  � ��# �  6'   �� 5   
    � ��# �  6'   �� 5   
    � ��$ Center=�65   �� 5   
    � ��$ 30��6#   �� 5   
    �@  SeparatorBrush �	#FFC9CACA6+   �� 5   
    � ��$ 14(�6%   �� 5   
    � ��$ SemiBold"�6'   �� 5   
    � ��$ 4 0 0 0q�6$   �� 5   
    � �� 5      �� 5      "   6"    5       ���� 5!      HoverOn .��5"      U� $ 00:00:00X�6<   $ Hover��6Q   $ (UIElement.Opacity)�6o   .\�5#   "   �� $	 00:00:00.2000000��67   $
 1Q�6R   5$      5%      �� 5&      HoverOff .��5'      U� $ 00:00:00X�6<   $ Hover��6Q   $ (UIElement.Opacity)�6o   .\�5(   "   �� $	 00:00:00.3000000��67   $
 0Q�6R   5)      5*      5+      .R�5,      � 
������  5-      �� 5.      $
��Auto �6/   �� 5/      $
��Auto �6/   �� 50      $��* �6/   �� 51      $
��Auto �6/   .+�53      �� $ BackgroundRectangle��6$   $��4��6A   $��2��6U   8��  $
 Fill=�54   H   8�� $��1��6�   �� 55      $ Hover��6$   $��4��63   $��2��6G   $
 Fill=�6X   8�� $ 0Q�56   K   �� 57      # z 6+   #��z��58   3   #��z��59   3   #��z��5:   3   # z 5;   3   F� 5<      $ 	SortArrow��6:   $
��Left=�6   $��Center=�6M   $��8��5=   '   $ 0Q�61   $ .5,.5&�6=   $��2��6[   $��2��6k   ���	#FF000000$ Uniform=�6�   +�����l��8] ��O 8] �        ��l��8] z�5>      V� 5?   "   .~�5@   &   �� $ .9Q�65   $ .9Q�6A   SortIconTransform��6M   5A   #   5C      n� 5D      $ PART_LeftHeaderGripper��6    $��0��6@   $
��Left=�6P   8�� 6k   n� 5E      $ PART_RightHeaderGripper��6    $��3��6A   $��Right=�6Q   8�� 6m   5F       5G      P� 5H       �@  SortDirection$ 	AscendingX�6;   . �5I      �� $ 	SortArrow��6%    �@J�Opacity$ 1Q�6O   �� 5J      $ 	SortArrow��6%    � z� 5K   "   �� 5L   &   $	 180Q�66   5N      �� 5O      $ BackgroundRectangle��6%    �@J�Opacity$ 1Q�6Y   5P      P� 5Q       �@  SortDirection$ 
DescendingX�6;   . �5R      �� $ 	SortArrow��6%    �@J�Opacity$ 1Q�6O   �� 5S      $ BackgroundRectangle��6%    �@J�Opacity$ 1Q�6Y   5T      P� 5U       �@J�IsMouseOver .  5V      �� 5W   "   8�� 62    5Y      �� 5Z   "   8�� 62   5\      5^      5`      5a      �� 5d      "   6   .�5e   
   ��  � �� 5f      �� 5g      "  6"   .R�5h      � .+�5i         $ PART_FillerColumnHeader��6/    .  6P   �� 5j      5k      5l      5n      5o      �� 5q      "  ��6{   .�5r   
   ��  � �� 5s      �� 5t      " ��6"   .R�5u      � .+�5v      �� $ Border��6$   #��� ��64    . 6u   � 5w      $! Arrow��6"   
���   �$��Right=�6>   $��8,8,3,3q�6Z   $
 0.15Q�6k   " �   ��� ��� ��� ��� $ Uniform=�6�   $��Bottom=�6�   5x       5y      P� 5z       �@J�IsMouseOver . . �5{      �� $ Border��67   # � ��6K    � ��5|      P� 5}       � �� . . �5~      �� $ Border��65   # � ��6I    � ��5      P� 5�       � {� .  . �5�      �� $ Arrow��6;    � y�$ 	Collapsed=�6N   5�      5�      5�      5�      �� 5�      "   6   .�5�   
   ��  � ��8   6'   �� 5�   
     �@ RowDetailsVisibilityMode$ VisibleWhenSelected=�65   �� 5�   
    � �� . 6:   �� 5�   
    �@��PanningMode$
 Both=�65   �� 5�   
    �@��IsFlicksEnabled .  63   �� 5�   
    � ��$ 5q�6#   �� 5�   
    � ��8  6(   �� 5�   
     �@ AlternatingRowBackground8  65   �� 5�   
     �@ HorizontalGridLinesBrush8  65   �� 5�   
    �@ VerticalGridLinesBrush8  63   �� 5�   
    � �� 5�      �� 5�      "  6"   .R�5�      �� #��z��6   $��2q�6I   $# 5��6]   #��z��6n   # z��6�    . ._�5�      �� $$ DG_ScrollViewer��6'   |�.  ��5�      �� 5�   "   " ��62   .R�5�   &   �  5�   *   �� 5�   .   $
��Auto �6?   �� 5�   .   $��* �6?   �� 5�   .   $
��Auto �6?   % 5�   *   �� 5�   .   $
��Auto �6<   �� 5�   .   $��* �6<   �� 5�   .   $
��Auto �6<   .+�5�   *   �� #��Z& 61   |�.  ��6r   C� *�� $" DataGridSelectAllButtonStyle��"   +y�6�   �� *HeadersVisibility+#( Z' #* Z) + �� ",  ��6�  �� *CellsPanelHorizontalOffset++ �� ",    5�   *   $!- PART_ColumnHeadersPresenter��6I   $��1��6n   y�6~   �� *HeadersVisibility+#( Z. #* Z) + �� ",  �� 5�   *   $!/ PART_ScrollContentPresenter��6A   #0 z��6f   $��2��6�   $��1��6�   �� 5�   *   $1 PART_VerticalScrollBar��64   $��2��6T   #3 z2 6d   $4 Vertical=�6�   $��1��6�   #y�z5 6�   #7 z6 6W  8 6�   �� *VerticalOffset+$9 OneWay=�+ �� *TemplatedParent+6W  � 5�   *   $��1��6/   $��2��6?    5�   .   �� 5�   2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset++ �� ",  �� 5�   2   $��* �6C   .+�5�   .   �� $1 PART_HorizontalScrollBar��68   $��1��6Z   #3 z: 6j   $4 
Horizontal=�6�   #y�z; 6�   #7 z< 6U  8 6�   �� *HorizontalOffset+$9 OneWay=�+ �� *TemplatedParent+6U  5�   +   5�   '   5�   #   .��5�      �� # z 6-   5�      5�      5�      5�      = 5�   
   P� 5�       �@��
IsGrouping . . �5�      ��  � �� .  6B   5�      5�      �� 5�      "   6   .�5�   
   ��  �@J�SnapsToDevicePixels . 60   �� 5�   
    �@=�ErrorTemplate 65   Z� �� 5�   
    �@ ValidationErrorTemplate 5�      �� 5�      .R�5�      � .+�5�      A� $��12��6"   $��12��6-   $��0 2 0 0q�69   
���  ��
���   �$	��Top=�5�   3   $	��0.5��5�      �� 5�      $
��Bold"�6$   $> 4,0,0,0q�66   $	��Top=�5�      
�������$��!��6D   ? 5�      �� *%#(Validation.Errors)[0].ErrorContent++ �� *FindAncestor+",  5�      5�      5�      �� 5�   
    � �� 5�      �� 5�      "  6"    5�      ���� 5�      
SelectedOn .��5�      U� $ 00:00:00X�6<   $ select_gradient��6Q   $ (UIElement.Opacity)�6y   .\�5�   "   �� $	 00:00:00.1000000��67   $
 1Q�6R   5�      5�      �� 5�      SelectedOff .��5�      U� $ 00:00:00X�6<   $ select_gradient��6Q   $ (UIElement.Opacity)�6y   .\�5�   "   �� $	 00:00:00.3000000��67   $
 0Q�6R   5�      5�      �� 5�      HoverOn .��5�      U� $ 00:00:00X�6<   $ hover_gradient��6Q   $ (UIElement.Opacity)�6x   .\�5�   "   �� $	 00:00:00.1000000��67   $

 0.85Q�6R   5�      U� 5�      $ 00:00:00X�6<   $ 	highlight��6Q   $ (UIElement.Opacity)�6s   .\�5�   "   �� $	 00:00:00.1000000��67   $

 0.65Q�6R   5�      5�      �� 5�      HoverOff .��5�      U� $ 00:00:00X�6<   $ hover_gradient��6Q   $ (UIElement.Opacity)�6x   .\�5�   "   �� $	 00:00:00.3000000��67   $
 0Q�6R   5�      U� 5�      $ 00:00:00X�6<   $ 	highlight��6Q   $ (UIElement.Opacity)�6s   .\�5�   "   �� $	 00:00:00.3000000��67   $
 0Q�6R   5�      5�      5�      .R�5�      �� $@ 
DGR_Border��6   #��z��61   #��z��6]   #��z��6�    . ._�5�         5�      �� 5�   "   $
��Auto �63   �� 5�   "   $��* �63   % 5�      �� 5�   "   $��* �60   �� 5�   "   $
��Auto �60   .+�5�      �� $ hover_gradient��6(   #��� 
 6@   $��1��6f   $A 1��6z   $B 1��6�   $ 0Q�6�    .  $��1��6�   #���  6�   �� 5�      $ 	highlight��6(   $��1q�6;   $��1��6F   $	A 0.5��6Z   $	B 0.5��6h   $ 0Q�6v    .  $��1��6�   #���  6�   #���  6�   �� 5�      $ select_gradient��6(   $��0��6A   $��2��6N   $��1��6b   $A 1��6v   $B 1��6�   $ 0Q�6�    .  #���  6�   #���  6�     5     $��1��65   #��zC 6E   # z 6o     5     $��1��67   $��1��6G   #y�zD 6k  E 6T   �� *AreRowDetailsFrozen+#( ZF #* ZG + �� ",  6k  	  5     $��2��60   $E Vertical=�6A   y�6�   �� *HeadersVisibility+#( ZH #* Z) + �� ",  5     5      5     P� 5     $I 
DGR_Border��6F    �@J�IsMouseOver .  5	     �� 5
  "   $J HoverOff_BeginStoryboard��6Y   8��   5     �� 5  "   $J HoverOn_BeginStoryboard��6X   8�� 5     P� 5      �@ 
IsSelected .  5     �� 5  "   $!J SelectedOff_BeginStoryboard��62   8�� 6W    5     �� 5  "   8�� 62   5     5     5     5     �� 5     "  
 6   .�5  
   ��  � �� 5      �� 5!     " 
 6"    5"     ���� 5#     
SelectedOn .��5$     U� $ 00:00:00X�6<   $ select_gradient��6Q   $ (UIElement.Opacity)�6y   .\�5%  "   �� $	 00:00:00.1000000��67   $
 1Q�6R   5&     5'     �� 5(     SelectedOff .��5)     U� $ 00:00:00X�6<   $ select_gradient��6Q   $ (UIElement.Opacity)�6y   .\�5*  "   �� $	 00:00:00.3000000��67   $
 0Q�6R   5+     5,     5-     .R�5.     �� #��z��6   #��z��6I   #��z��6}    . ._�5/     � .+�50     �� $ select_gradient��6(   $��-2 -1 -2 -1q�6A   $��1��6W   $��1��6g   $A 1��6{   $B 1��6�   $ 0Q�6�    .  #���  6�   #���  6�   �� 51     # z 6/   52     53      54     P� 55      �@
 
IsSelected .  56     �� 57  "   $!J SelectedOff_BeginStoryboard��62   8��  6W    59     �� 5:  "   8�� 62   5<     5>     5A     5B     �� 5D     6#   �� 5F     "  n�6*   .�5G  
   ��  � ��$ 8��6#   �� 5H  
    � ��
 ���� 6'   �� 5I  
    �@�Cursor$ SizeNS��6#   �� 5J  
    � �� 5K     �� 5L     " n�6"   .R�5M     �� #��z��6   # z��6G   5N     5P     5Q     5g     