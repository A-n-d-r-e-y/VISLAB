   M S B A M L   `   `   `  ���� A  =VisLab, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:VisLab.StylesVisLab.Styles  "    VisLab.Controls.ModelControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .stylesclr-namespace:VisLab.Styles     -     � Cursor$
  Hand��
������ $��Segoe UI)� � 	Resources -   'VisLab.Styles.StringFormattingConverter   stringFormattingConverter��%   &	          .���  � Triggers :�  :� RoutedEvent$ Expander.Expanded�� :� 
SourceName$ expander��[��� .���� .��W�  W� From$ 0X�	 W� To$ 1X� g� Duration$ 0:0:0.25D� �� 
TargetName$ border��	 �� TargetProperty$@	 :(FrameworkElement.LayoutTransform).(ScaleTransform.ScaleX)�
 � RowDefinitions
 �� $��1* ��� $��1* ��� $��1* � � ColumnDefinitions �� $��* ��� $��* ��� $
��Auto �.+��� -   $��cnvModel��
������  J� RenderTransformOrigin$ 0.5 0.5&�$��Stretch=�$��Stretch=�$��3��$��3��z�V� .~�T� -   ttModel���� -   stModel�� �� ScaleY$ -1Q��� $��Bottom=�$��Right=�$��5q�$��3��$��3��.+��� -   $��tbkXCoordinate���� -   $��tbkYCoordinate���� ����  �� ElementName$ stModel�� �� Path$ ScaleX�A� $��40��$��40��
�������$��1��$��3��$��3��9� -   $��expander��$��2��$��2�� 9� ExpandDirection$
 Left=�
������ ���� $��Reports��$
��0,10q�$��Italic$�
������� � LayoutTransform ��  �� Angle$ 90Q�.���� -   $��border��$��5q� �� CornerRadius$ 10��$��2q�
������� ��  �� ScaleX$ 0Q�$ 1Q�._��� -	   $��stack��$��5q� �� Orientation$ Vertical=�$	��200��
������ .+��� -
   $��Avg. Speed Report��$��22(�$��2q��� $��Other Report��$��22(�$��2q�����  �� EndPoint$ 1,0.5&� �� 
StartPoint$ 0,0.5&�.���  � Color$ 	#75EFF2FA�� � Offset$ 0Q�� $ 	#7BFDFDFE��$ 1Q�