   M S B A M L   `   `   `  ����B  >Bank.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Bank.UIBank.UI  5   	       Bank.UI.CustomersWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Bank.UI     5   	     � Title$  	Customers��5   	   $
��auto��6   $
��auto��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��4* �6   �� 5      $��60 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��500 �6   �� 5      $��* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��CustomersDataGrid��5   /      AutoGenerateColumns .  $��Center=�6`   $	��Top=�6}      CanUserAddRows .  $
��auto��5   *   $
��auto��67   $��1��6E   $��1��6R   ��5   H   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Name+  5      $ 	Firstname��6%    68   �� *	FirstName+  5      $ Address��6%    66   �� *	Address+4  .System.Windows.Controls.DataGridComboBoxColumn  5      $
 City��6)   -   5        CityComboBoxColumn��   SelectedValuePath$ ZipCode��5   |   	   DisplayMemberPath$
	 Name��6�   
   SelectedValueBinding
 6S   �� *	ZipCode+6�     5      $ 	Cellphone��6%    68   �� *	CellPhone+5       �� 5!   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��2��6K   $��1��6X   .+�5"      �� -   5        $��ShowAccountsButton��5"      $��Show Accounts��6/   $��10q�6h   $��10q�6t   �� 5#      -   5        $��SaveCustomerButton��5#      $
��Save��6/   $��10q�6_   $��10q�6k   �� 5$      -   5        $��AddCustomerButton��5$      $��Add Customer��6.   $��10q�6e   $��10q�6q   5%      5&      5'      