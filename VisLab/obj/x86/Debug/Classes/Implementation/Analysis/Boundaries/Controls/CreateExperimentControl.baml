   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      
 VisLabX   RVisLab.Classes.Implementation.Analysis.Boundaries.Controls.CreateExperimentControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      -   5        $	��700��5   7   $��Segoe UI)�6F   $��16(�6\   
������   � LayoutTransform  5
      �� 5   
   -   5        stBorder�� �� ScaleX$ 1Q�5   +    �� ScaleY$ 1Q�66   .��5      �  � ColumnDefinitions 5   
   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��2q�6    �� CornerRadius$ 10��6   $��3q�6.   
�������
�������._�5      �� $��10q�6   $
��Left=�6%   $	��Top=�6@   .+�5      �� $��Simulation complete!��6   �� 5      $��Saving experiment results��6   5      5      �� 5   
   $��2q�6   $��1��6   $ 10��6)   $��3q�6;   
�������
�������._�5      � $��10q�6    5      �� 5      $
��Auto �6$   �� 5       $
��Auto �6$   �� 5!      $
��Auto �6$   �� 5"      $
��Auto �6$   �� 5#      $
��Auto �6$   �� 5$      $
��Auto �6$   �� 5%      $
��Auto �6$    5'      �� 5(      $
��Auto �6'   $	��140��64   �� 5)      .+�5+      �� $��Created on:��6   $��1q�6/   �� 5,      $��1��6   $��1q�6,   ��67   �� *	CreatedOn+ �� StringFormat$ {0:D}���� 5-      -   5        $��tbkDuration��5-      $��1��6/   $��	Duration:��6<    � ToolTip$&  Duration in 'real world' seconds��6M   $��1q�6x   �� 5.      $��1��66   $��1��6}   $��1q�6�   ��6   �� *
Duration+ 6C   ��  �� ElementName$ tbkDuration��	 �� Path$	 ToolTip�6�   �� 51      $��Number of Runs:��6   $��1q�63   $��3��6>   �� 52      $��1��6   $��3��6,   $��1q�69   ��6D   �� *NumberOfRuns+�� 53      $��4��6   $��Description:��6)   $��1q�6>   �� 54      $��5��6   $��2��6'   $��1q�6;   
 �� TextWrapping$

 Wrap=�6F   ��6Z   �� *Description+� 55      $��6��6   $��2��6$   $��Stretch=�68   $��10q�6V    56      �� 57      $��* �6+   �� 58      $
��Auto �6+   �� 59      $
��Auto �6+   .+�5;      �� $��Create Experiment?��6    $��Center=�6:   $��0,0,20,0q�6U   �� 5<      -   5        $��btnCreateExperimenet��5<   -   $��1��6   $	��Yes��6I   $	��120��6W   $��3q�6c   �� 5=      -   5        $��	btnCancel��5=   -   $��2��6   $��No��6>   $	��120��6K   $��3q�6W   5>      5?      5@      5A      5B      