   M S B A M L   `   `   `  ����B  >CDb.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:CDb.WPF.VistaModelosCDb.WPF.VistaModelos  5   	   /clr-namespace:CDb.WPF.VistasCDb.WPF.Vistas  5   	       CDb.WPF.Vistas.VPrincipal   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6modelos"clr-namespace:CDb.WPF.VistaModelos     5   	   /vistasclr-namespace:CDb.WPF.Vistas     5   	   $	��502��5   )   $
��1152��66     � WindowState$  	Maximized=�6C    � WindowStartupLocation$ CenterScreen=�6[    � Title 6	   �� *TituloVentana+.��5      �  � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      �� 5      $
��Auto �6   .+�5   
   ]� $	��Top=�6   .+�5      o� .@�5      m� $��_Archivo��6   @�5      m� 5      $��_Abrir��6#    m� Command 63   �� *AbrirArchivo+m� 5      $��_Guardar��6#    65   �� *GuardarArchivo+m� 5      $��Guardar _Como��6#    6:   �� *GuardarArchivoComo+�� 5      m� 5      $��_Salir��6#    63   �� *
Cancelar+5      m� 5      $��_Editar��6   .@�5      m� $��	_Deshacer��6   $
 Undo��62   m� 5      $��_Rehacer��6   $
 Redo��61   m� 5      $��_Cortar��6   $	 Cut��60   m� 5      $��_Copiar��6   $
 Copy��60   m� 5      $��_Pegar��6   $ Paste��6/   m� 5      $��_Seleccionar Todo��6   $ 	SelectAll��6:   m� 5       $��_Buscar��6   $ Search��60   5!      5"      5#      �� 5%   
   $��1��6    �� TabStripPlacement$
 Left=�6"   .@�5&      �� $��Texto Fuente��6   .��5'      ]� .+�5(      �� -   5         ��Name$ 	tbxFuente��5(       �� TextWrapping$
 Wrap=�5)      " �� VerticalScrollBarVisibility$ Visible=�62   	 �� AcceptsReturn	 . ��5(   1   �� *TextoFuente+
 �� UpdateSourceTrigger$
 PropertyChanged=�5)   X   5+      5,      �� 5-      $��Análisis Léxico��6   {�6/   �� *CompilacionExitosa+ �� Mode$ OneWay=���5.      �� *AnalisisLexico+$ OneWay=��� 5/      $��Análisis Sintáctico��6   {�63   �� *CompilacionExitosa+$ OneWay=���50      �� *AnalisisSintactico+$ OneWay=��� 51      $��Árbol��6      
vistaArbol#��[  52      {�51   %   �� *CompilacionExitosa+$ OneWay=���53      �� *
NodoRaiz+$ OneWay=�54      �� 56   
   $��2��6   @�57      �� 58       �� Padding$ 0q�6`   ��6   �� *TextoFuente.Length+ �� StringFormat$ Caracteres: {0:G}��6`   �� 59      �� 5:      $ 0q�6Z   
���  ��$
��Bold"�5;   -   ��5:      �� *UltimoError+$ Último Error : {0}��y�5<      �� *UltimoError+  conversorVisibility �� 	Converter# [ 5>      5?      5@      