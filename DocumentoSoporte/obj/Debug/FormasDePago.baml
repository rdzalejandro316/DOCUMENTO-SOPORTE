   M S B A M L   `   `   `  ����K  GDocumentoSoporte, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DocumentoSoporteDocumentoSoporte  5   	   #    DocumentoSoporte.FormasDePago   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:DocumentoSoporte     5   	   a ]Syncfusion.SfGrid.WPF, Version=16.1460.0.32, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89j fSyncfusion.SfGridConverter.WPF, Version=16.1460.0.32, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.Wpf, Version=16.1460.0.32, Culture=neutral, PublicKeyToken=3d67ed1f87d44c896
syncfusion!http://schemas.syncfusion.com/wpf    5   	   -   5          � Title$  FormasDePago��5   	   $	��350��6   $	��800��6+    � 	Resources 5
       �� Color   	ColorMain  ColorSecond3  -Syncfusion.UI.Xaml.Grid.GridHeaderCellControl �� 
TargetType  headerStyle �� Property �� Value��%*  &	        &	 &     &	 N     �� 5   
   $ #0078d4��6   6*   �� 5   
   $ 	#FF0667B2��6   6,   �� 5   
   "  6   .�5      ��  � ��$
 Bold"�6+   �� 5       � ��$ 10(�6)   �� 5       � ��
 �����6+   �� 5       � �� �#3737376+   5      5      .��5      � -   5        $��GridMain��5      
������� � RowDefinitions 5   
   �� 5      $��80 �6   �� 5      $��1* �6   �� 5      $��40 �6   .+�5   
   � $��0��6   $��2��6    � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5       $	��120 �6#   �� 5!      $	��230 �6#   .+�5%      �  5&      �� 5'      �� 5(      .+�5+      ��  �� Orientation$ 
Horizontal=�6   .+�5,      �� $��Formas de pago: ��6    $��Center=�68   $��10 0 0 0q�6S   $��DemiBold"�6e   3  -Syncfusion.Windows.Tools.Controls.ComboBoxAdv  5-      -   5        $��
CBFormapag��5-   -   $��Center=�6?   $	��160��6Z   +  %Syncfusion.Windows.Shared.SkinStorage	   VisualStyle$	 Metro��6f   5.      �� 50      $��1��6   $ 
Horizontal=�6*   .+�51      �� $��Medios de Pago: ��6    $��Center=�68   $��10 0 0 0q�6S   $��DemiBold"�6e     52      -   5        $��CBpagos��52   -   $��Center=�6<   $	��200��6W   $	 Metro��6c   �� 54      -   5        $��BtnAdd��54      $��30��6+   $��25��66   $��10 0 0 0q�6B   
������
 � Cursor$

 Hand��6�   ��6l   Z� .��55      �� $��imagenes\plus.png��6    $��5q�6;   56      �� 57      -   5        $��BtnDel��57      $��30��6+   $��25��66   $��10 0 0 0q�6B   
������$

 Hand��6�   ��6l   Z� .��58      �� $��imagenes\bin2.png��6    $��5q�6;   59      5:      5<      �� 5?      $��1��6   $��Saldo por Pagar:��6(   $��Center=�6@   $��13(�6[   $
��Bold"�6i   $��Right=�6{   $��1,1,5,1q�6�   �� 5@      
���   �$��3��6(   ._�5A      �� -   5         ��Name$ TxtTotalRecaudo��5A   C   |�.  $��0��6,   $��25(�65   
������$	��210��6t   $��35��6�   
������ $��Right=�6�   $��5,5,5,5q�6�   5B      5C      � 5E   
   $��1��6   $��5,5,5,5q�6   $��2��6-   .+�5F      (  "Syncfusion.UI.Xaml.Grid.SfDataGrid  -   5        $��dataGrid��5F   $   $��1��65      EnableDataVirtualization . (  "Syncfusion.UI.Xaml.Grid.SfGridBase   AutoGenerateColumns .  $��12(�6�      SelectionMode$ Single=�6�      HeaderStyle# [ 6�      Columns 5H      ,  &Syncfusion.UI.Xaml.Grid.GridTextColumn  ,  &Syncfusion.UI.Xaml.Grid.GridColumnBase   
HeaderText$ Codigo��60      MappingName$ cod_pag��6D      Width$	 140Q�6Z     5I      $ 
Forma Pago��60   $ nom_pag��6H   $	 300Q�6^   /  )Syncfusion.UI.Xaml.Grid.GridNumericColumn  5J      $ Valor A Pagar��63      AllowEditing . $ valor��6b      NumberDecimalSeparator$ .��6v      NumberDecimalDigits$ 2��6�      NumberGroupSeparator$ ,��6�      NumberGroupSizes �      $	 200Q�6�     5K      $ Fpago��60   $ cod_tpag��6C   $ 60Q�6Z   5M      5N      � 5P   
   $��2��6   $��2��6    5Q      �� 5R      $	��100 �6#   �� 5S      $	��100 �6#   �� 5T      $	��300 �6#   �� 5U      $��80 �6#   �� 5V      $	��100 �6#   .+�5X      �� -	   5         ��Name$ 	BtnGrabar��5X   J   $��	F5=Grabar��6   $��5,5,5,5q�6)   $��0��6:   #��[  6u   #��[  6�   
�������6�   �� 5Y      -
   5        $��Cancelar��5Y      $��5,5,5,5q�6(   $��1��69   #��[  6]   #��[  6�   
�������6�   �� 5[      $��2��6   $��Center=�6)   $��Center=�6D   $ 
Horizontal=�6a   .+�5\      �� $��F8=Cancela Saldo��6   $��10(�64   $��Center=�6B   $��Center=�6]   #��[  6z   $��5q�6�   5]      �� 5_      $��Total Pago:��6   $��3��6+   $��Right=�6;   $��Center=�6W   $
��Bold"�6r   �� 5`      -   5         ��Name$ TxtTotalPagado��5`      $��0��60   $��4��69   $��Right=�6I   $��Center=�6e   $��15(�6�   5a      5b      5d      