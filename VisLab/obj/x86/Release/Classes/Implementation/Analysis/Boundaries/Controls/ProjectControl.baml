   M S B A M L   `   `   `  ���� 
  VisLab$    VisLab.Controls.ProjectControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -   $��Segoe UI)�
������   � 	Resources   �� 
TargetType �� Property �� Value��%o   '��       �� " ��.���  � ��$ 50����  � ��$	 300����  � ��$ 3q���  � ��$ 30(�.��� .+�� -   
������ .+��� -   $��canvas�� J� RenderTransformOrigin$ 0.5 0.5&�z�V� .~�T� -   ttModel���� -   stModel�� �� ScaleX ��  �� ElementName$ slider�� �� Path$ Value� �� Mode$ TwoWay=�	 �� ScaleY	 �� $ slider��$ Value�$ TwoWay=�.+�� -   $
��grid��$��Center=�$��Center=�
 � ColumnDefinitions
 �� �� ��  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��	0,0,0,-20q�$��2��$��Bottom=�$��Center=�$��40(����� *Project.Name+� $��0��$��1��$��Center=�$��Center=� �� �� .+��� -   $��btnCreateProject��$��Create Project���� -   $��btnOpenProject��$��1��$��Open Project���� $��1��$��3��$	��400��$��20q�#��["�� $��2��$��1��$��Center=�$��Center=� �� �� .+��� -	   $��listbox��$	��200��$	��384��$	��400��
������ ��.  ���� *Project.Models.Model+���� .R��� $��22(����� *Name+   �� 
TargetType �� Triggers ]� 
Conditions �� Property �� Value P� Property P� Value   ListboxItemStyle��%�  '��       '�� �    �� " ��.���  � ��$ 1,2,1,1q���  � ��$ Stretch=���  � ��
 ��ح���  � �� �� " ��.R�� .+��� #��z���� ���	#BEFFFFFF$	��3,1q�._��  �� �� .+��� $��2,1,2,0q�$��0�����	#57FFFFFF�� $	��8,5q� ]�  ��  �@J�IsMouseOver . ��  �@��
IsSelected .  ./���  � ��
 ��ح�P�  �@��
IsSelected . . ���  � ��
 � ����� " ��.���  � ��# �   ��  � ��$ 3,3,2,1q��� $	��6,2q� �� Orientation$ 
Horizontal=�$��1��$��Right=�{��� *
HasProject+.+��� -
   $��btnAddModel��$��30��$	��140��$��3q�$��16(�$��	Add Model���� -   $��btnDeleteModel��$��30��$	��140��$��3q�$��16(�$��Delete Model���� -   $��slider�� � Cursor$
 Hand��$��Bottom=�$
��Left=�$	��200��$
��20,5q� � Minimum$	 0.5Q� � Maximum$ 10Q� �� TickPlacement$ BottomRight=� � Value$ 1Q� �� TickFrequency$	 0.5Q�