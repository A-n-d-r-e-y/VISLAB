   M S B A M L   `   `   `  ����T  PVisLab, Version=1.1.4521.27586, Culture=neutral, PublicKeyToken=2f49d7e83098ceed-clr-namespace:VisLab.StylesVisLab.Styles  5      N    HVisLab.Classes.Implementation.Analysis.Boundaries.Controls.ReportControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .stylesclr-namespace:VisLab.Styles     5      $��Segoe UI)�5      
������   � 	Resources  5
       �� MergedDictionaries �� Source$   VisLab.Styles.BooleanConverter   booleanConverter�� 5   
    5      �� 5      $  /Styles/DataGridStyle.xaml?�6%   %   &	          5      6&   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5        $��ccModel��5      $��1��6(   ��68   �� *MasterModel+ �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   $��0��6   $��5��6'   �� 5   
   -   5        $��listbox��5       J� OpacityMask �	#D4000000
������ ��.  $��Visible=�5    8   ��5   !   �� *List+��5!      �� 5"      .R�5#      �� $��1��6    �� CornerRadius$ 10��6-   $��2q�6?   
�������._�5$      � .+�5%      ��  J� RenderTransformOrigin$ 0.5 0.5&�6(   $	��5,0q�6H   ��5&   "   `� 5'   &   	 �� StringFormat$	 {0}#{1}��63   .1�5(   *   �� 
 �� Path$
 	ModelName�62   �� 5)   *   $
 ExperimentNumber�62   5*   '   z�5,      �� 5-   "    �� Angle$	 270Q�62   5/      �� 50      }�. $	��100��6U   ��6-   �� ��6d   ��  �� RelativeSource �� *Self+$
 ActualWidth�51      52      53        55        cvs �� Source �� ElementName �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers ]� 
Conditions �� Property �� Value P� Property P� Value  ListboxItemStyle��%�  &	       '�� <     '�� I    �� 56       6@   �� $ listbox��$
 ItemsSource��� 57      " ��6   .�58      ��  � ��$ 1,2,1,1q�6/   �� 59       � ��$ Stretch=�6<   �� 5:       � ��
 ��ح�63   �� 5;       � �� 5<      �� 5=      " ��6.   .R�5>   "   � .+�5?   &   �� #��z��6-   �� 5@   &   ���	#BEFFFFFF$	��3,1q�6D   ._�5A   *   �  5B   .   �� 5C   2   �� 5D   2   .+�5F   .   �� $��2,1,2,0q�65   $��0��6F   ���	#57FFFFFF6S   5G   +   5H   '   �� 5I   &   $	��8,5q�67   5J   #    5K   "   ]� 5L   &    5M   *   �� 5N   .    �@J�IsMouseOver . 6O   �� 5O   .    �@��
IsSelected .  6N   ./�5Q   *   ��  � ��
 ��ح�6G   5R   '   P� 5S   &    �@��
IsSelected . . �5T   *   ��  � ��
 � ���6G   5U   '   5W      5Y      5Z      �� 5[      " ��6   .�5\      ��  � ��# �  6;   �� 5]       � ��$ 3,3,2,1q�6/   5^      5_      5`      d� 5a   
   -   5        $��btnShowCounters��5a      $��1��6.   $��Right=�6>   $	��Top=�6Z   $��20q�6r   $��2��6~    �	#D4000000$	��120��6�   $��30��5b   f    d� 	IsChecked 6   �� *AreCountersGlobalVisible+��6F   �� *CountersMsg+{�6r   �� *IsCounterButtonEnabled+�� 5c   
   -   5        $��btnCountersReport��5c      $��1��6*   $��Right=�6:   $	��Top=�6V   $��20,54q�6n    �	#D4000000$��30��6�   $	��120��6�   $��Counters Report��6�   {�6�   �� *IsCounterButtonEnabled+d� 5d   
   -   5        $��btnShowTravelTimes��5d      $��1��61   $��Right=�6A   $	��Top=�6]   $��30��6u   $��20,88q�6�   $��2��6�    �	#D4000000$	��120��6�   ��6�   �� *
TrTimesMsg+ 5e      �� *AreTrTimesGlobalVisible+{�6E   �� *IsTrTimesButtonEnabled+�� 5f   
   -   5        $��btnTrTimesReport��5f      $��1��6)   $��Right=�69   $	��Top=�6U   $��20,122q�6m    �	#D4000000$��30��6�   $	��120��6�   $��Tr. Times Report��6�   {�6�   �� *IsTrTimesButtonEnabled+�� 5g   
   -   5        $��ccShadow��5g   :   $��3��6   $��1��6*   ��6J   �� *Shadow.Shadow+5h      5i      