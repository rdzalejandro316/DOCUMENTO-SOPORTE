   M S B A M L   `   `   `  ����K  GDocumentoSoporte, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DocumentoSoporteDocumentoSoporte  5   	        DocumentoSoporte.BtnEditar   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:DocumentoSoporte     5   	   a ]Syncfusion.SfGrid.WPF, Version=16.1460.0.32, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89j fSyncfusion.SfGridConverter.WPF, Version=16.1460.0.32, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.Wpf, Version=16.1460.0.32, Culture=neutral, PublicKeyToken=3d67ed1f87d44c896
syncfusion!http://schemas.syncfusion.com/wpf    5   	   -   5          � Title$  
Documentos��5   	   $	��400��6   $	��550��6)    � 	Resources 5
      3  -Syncfusion.UI.Xaml.Grid.GridHeaderCellControl �� 
TargetType   headerStyle �� Property �� Value��%�   &	        �� 5   
   "  6   .�5      ��  � ��$
 Bold"�6+   �� 5       � ��$ 10(�6)   �� 5       � ��
 �����6+   �� 5       � �� �#3737376+   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      �� 5      $��40 �6   .+�5   
   � 
���   � � ColumnDefinitions 5      �� 5      $��80 �6#   �� 5      $	��100 �6#   �� 5       $��70 �6#   �� 5!      $	��100 �6#   �� 5"      .+�5&      �� $��
Fecha Ini:��6   $��Center=�6*   $��Right=�6E   
�������$
��Bold"�6t   (  "System.Windows.Controls.DatePicker  5'      -   5        $��TxFecIni��5'      $��1��6)   $��Center=�69   �� 5)      $��2��6   $��
Fecha Fin:��6(   $��Center=�6:   $��Right=�6U   
�������$
��Bold"�6�     5*      -   5        $��TxFecFin��5*      $��3��6)   $��Center=�69   �� 5,      -   5        $��BtnConsultar��5,      $��	Consultar��6)   $	��100��6=   $��4��6I   $��30��6Y   $��5q�6e   $
��Left=�6p   ���#3498db���#3498db
�������6�   5.      � 50   
   $��1��6   .+�51      (  "Syncfusion.UI.Xaml.Grid.SfDataGrid  -   5        $��dataGrid��51   $   $��1��64   $��2q�6A      EnableDataVirtualization . (  "Syncfusion.UI.Xaml.Grid.SfGridBase   AutoGenerateColumns .  	   ShowGroupDropArea	 .  $��10(�6�   
   AllowFiltering
 .    SelectionMode$ Single=�6�      HeaderStyle# [  6�      Columns 53      ,  &Syncfusion.UI.Xaml.Grid.GridTextColumn  ,  &Syncfusion.UI.Xaml.Grid.GridColumnBase   
HeaderText$ 	Documento��60      MappingName$ idreg��6G      IsHidden . 6[   0  *Syncfusion.UI.Xaml.Grid.GridDateTimeColumn  54      $ Fecha��64   $ fecha��6I      Width$	 100Q�6]     55      $ 	Documento��60   $ num_trn��6G   (  "Syncfusion.UI.Xaml.Grid.GridColumn   ColumnSizer$ AutoLastColumnFill=�6]     56      $ Nit/CC��60   $ cod_cli��6D   $	 120Q�6Z     57      $ Vendedor��60   $ cod_ven��6F   $	 100Q�6\   59      5:      � 5<   
   $��2��6   
���   �.+�5=      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $��5q�6M   .+�5>      �� $��Registros :��6   
�������$
��Bold"�6B   �� 5?      -   5        $��TxTotal��5?      
�������$��-��6>   $��2,0,0,0q�6G   5@      �� 5B      -   5        $��	BtnSelect��5B      $��Seleccionar��6&   $	��100��6<   $��4��6H   $��30��6X   $��5q�6d   $��Right=�6o   ���#3498db���#3498db
�������6�   5C      5E      5F      