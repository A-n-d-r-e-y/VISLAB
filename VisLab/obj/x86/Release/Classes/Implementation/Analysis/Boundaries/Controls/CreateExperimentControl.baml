   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  
 VisLabX   RVisLab.Classes.Implementation.Analysis.Boundaries.Controls.CreateExperimentControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6sys&clr-namespace:System;assembly=mscorlib     -   $	��700��$��Segoe UI)�$��16(�
������   � LayoutTransform  �� -   stBorder�� �� ScaleX$ 1Q� �� ScaleY$ 1Q�.���  � ColumnDefinitions ��  � RowDefinitions �� $
��Auto ��� $
��Auto �.+��� $��2q� �� CornerRadius$ 10��$��3q�
�������
�������._��� $��10q�$
��Left=�$	��Top=�.+��� $��Simulation complete!���� $��Saving experiment results���� $��2q�$��1��$ 10��$��3q�
�������
�������._�� $��10q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
��Auto �$	��140���� .+��� $��Created on:��$��1q��� $��1��$��1q����� *	CreatedOn+ �� StringFormat$ {0:D}���� -   $��tbkDuration��$��1��$��	Duration:�� � ToolTip$&  Duration in 'real world' seconds��$��1q��� $��1��$��1��$��1q����� *
Duration+ ��  �� ElementName$ tbkDuration��	 �� Path$	 ToolTip��� $��Number of Runs:��$��1q�$��3���� $��1��$��3��$��1q����� *NumberOfRuns+�� $��4��$��Description:��$��1q��� $��5��$��2��$��1q�
 �� TextWrapping$

 Wrap=����� *Description+� $��6��$��2��$��Stretch=�$��10q� �� $��* ��� $
��Auto ��� $
��Auto �.+��� $��Create Experiment?��$��Center=�$��0,0,20,0q��� -   $��btnCreateExperimenet��$��1��$	��Yes��$	��120��$��3q��� -   $��	btnCancel��$��2��$��No��$	��120��$��3q�