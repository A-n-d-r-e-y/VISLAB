   M S B A M L   `   `   `  ���� T  PVisLab, Version=1.0.4515.16253, Culture=neutral, PublicKeyToken=2f49d7e83098ceed-clr-namespace:VisLab.StylesVisLab.Styles  N    HVisLab.Classes.Implementation.Analysis.Boundaries.Controls.ReportControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .stylesclr-namespace:VisLab.Styles     $��Segoe UI)�
������   � 	Resources   �� MergedDictionaries �� Source$   VisLab.Styles.BooleanConverter   booleanConverter��  �� $  /Styles/DataGridStyle.xaml?�%   &	          .���  � RowDefinitions �� $��* ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* �.+��� -   $��ccModel��$��1������ *MasterModel+ �� UpdateSourceTrigger$ PropertyChanged=��� $��0��$��5���� -   $��listbox�� J� OpacityMask �	#D4000000
������ ��.  $��Visible=����� *List+���� .R��� $��1�� �� CornerRadius$ 10��$��2q�
�������._�� .+���  J� RenderTransformOrigin$ 0.5 0.5&�$	��5,0q���`� 	 �� StringFormat$	 {0}#{1}��.1��� 
 �� Path$
 	ModelName��� $
 ExperimentNumber�z���  �� Angle$	 270Q��� }�. $	��100������ ����  �� RelativeSource �� *Self+$
 ActualWidth�    cvs �� Source �� ElementName �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers ]� 
Conditions �� Property �� Value P� Property P� Value  ListboxItemStyle��%*  &	       '�� .     '�� �    ��  �� $ listbox��$
 ItemsSource��� " ��.���  � ��$ 1,2,1,1q���  � ��$ Stretch=���  � ��
 ��ح���  � �� �� " ��.R�� .+��� #��z���� ���	#BEFFFFFF$	��3,1q�._��  �� �� .+��� $��2,1,2,0q�$��0�����	#57FFFFFF�� $	��8,5q� ]�  ��  �@J�IsMouseOver . ��  �@��
IsSelected .  ./���  � ��
 ��ح�P�  �@��
IsSelected . . ���  � ��
 � ����� " ��.���  � ��# �  ��  � ��$ 3,3,2,1q�d� -   $��btnShowCounters��$��1��$��Right=�$	��Top=�$��20q�$��2�� �	#D4000000$	��120��$��30�� d� 	IsChecked �� *AreCountersGlobalVisible+���� *CountersMsg+{��� *IsCounterButtonEnabled+�� -   $��btnCountersReport��$��1��$��Right=�$	��Top=�$��20,54q� �	#D4000000$��30��$	��120��$��Counters Report��{��� *IsCounterButtonEnabled+d� -   $��btnShowTravelTimes��$��1��$��Right=�$	��Top=�$��30��$��20,88q�$��2�� �	#D4000000$	��120������ *
TrTimesMsg+ �� *AreTrTimesGlobalVisible+{��� *IsTrTimesButtonEnabled+�� -   $��btnTrTimesReport��$��1��$��Right=�$	��Top=�$��20,122q� �	#D4000000$��30��$	��120��$��Tr. Times Report��{��� *IsTrTimesButtonEnabled+�� -   $��ccShadow��$��3��$��1������ *Shadow.Shadow+