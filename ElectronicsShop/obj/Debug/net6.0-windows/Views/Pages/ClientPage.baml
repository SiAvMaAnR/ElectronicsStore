   M S B A M L   `   `   `  ����J  FElectronicsShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:ElectronicsShop.Views.PagesElectronicsShop.Views.Pages  5      ,    &ElectronicsShop.Views.Pages.ClientPage   5      W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:ElectronicsShop.Views.Pages     5        N� Title$  
ClientPage��5	       � 	Resources 5      *  $System.Windows.Controls.DataGridCell �� 
TargetType   Cell �� Property �� Value  ColumnDG  
TestButton��%�  &	        &	 �     &	     �� 5   
   "  6   �5      �� 5       �@��TextAlignment$ Center=�6<   �� 5       � ��$ 20(�6-   �� 5       � ��$ Arial)�6/   5      �� 5   
   " ��6   .�5      ��  �@��	MaxLength$ 10��6*   5      �� 5   
   " ��6   �5      �� 5       � ��$ 40(�6-   �� 5       � ��$ 40(�6-   5      5      .��5       �  � ColumnDefinitions 5!   
   �� 5"      $	��80* �6   �� 5#      $	��20* �6   .+�5'   
   � $��0��6   .+�5(      &   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��6L   ��6   �� *ClientDataTable.DefaultView+   ColumnHeaderStyle 5)      �� 5*      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5+      ��  � ��$ Center=�6G   6X   �� 5,       � ��$	 5 0q�64   6B   �� 5-       � ��$ 20(�65   6B   5.         Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn	   	CellStyle#	 [  6)   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 70 6K      Header$ Id��6V      
IsReadOnly$
 True��6   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6b   �� *
ClientId+6     52      #	 [  6)   $	
 20* 6K   $ Имя��6W    6d   �� *	FirstName+   EditingElementStyle 53      �� 54      " ��6$   .�55   "   ��  �@��	MaxLength$ 40��6>   56      58        59      #	 [  6)   $	
 20* 6K   $ Фамилия��6W    6h   �� *
LastName+ 5:      �� 5;      " ��6$   .�5<   "   ��  �@��	MaxLength$ 40��6>   5=      5?        5@      #	 [  6)   $	
 20* 6K   $ Отчество��6W    6i   �� *
MiddleName+ 5A      �� 5B      " ��6$   .�5C   "   ��  �@��	MaxLength$ 40��6>   5D      5F        5G      #	 [  6)   $	
 15* 6K   $ Возраст��6W    6h   �� *Age+ 5H      �� 5I      " ��6$   .�5J   "   ��  �@��	MaxLength$ 40��6>   5K      5M        5N      #	 [  6)   $	
 25* 6K   $ № Телефона��6W    6k   �� *PhoneNumber+5Q      5R      � 5U   
   $��1��6    � RowDefinitions 5V      �� 5W      �� 5X      �� 5Y      �� 5Z      �� 5[      .+�5^      � $��0��6   .+�5_      �� -   5        $��Truncate��5_      #��[ 6,   6Q   5`      � 5b      $��1��6   .+�5c      �� -   5        $��Create��5c      #��[ 6*   6N   5d      � 5f      $��2��6   .+�5g      �� -   5        $
��Drop��5g      #��[ 6(   6L   5h      5i      5j      5k      