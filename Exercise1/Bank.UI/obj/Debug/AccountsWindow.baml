   M S B A M L   `   `   `  ����D  @Bank.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null`>clr-namespace:Bank.Data.DomainClasses.Enums;assembly=Bank.DataBank.Data.DomainClasses.Enums  5   	    Bank.UI   Bank.UI.AccountsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Penums>clr-namespace:Bank.Data.DomainClasses.Enums;assembly=Bank.Data     5   	     � Title$  AccountsWindow��5   	   $
��auto��6    $
��auto��6.    � 	Resources 5	       S� 
MethodName S� 
ObjectType6   0Bank.Data.DomainClasses.Enums.EnumerationManager   AccountTypeDataProvider S� MethodParameters M� TypeName��%x   &	        S� 5
   
   $ 	GetValues��6   "  64    5      M� 5      $ enums:AccountType��6   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��4* �6   �� 5      $��60 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��500 �6   �� 5      $��* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ AccountsDataGrid��5   /   	   AutoGenerateColumns	 .  $��Center=�6a   $
��auto��6~   $	��Top=�6�   $
��auto��6�   
   CanUserAddRows
 .  $��1��6�   $��1��6�   ��6I   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Account Number��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *AccountNumber+4  .System.Windows.Controls.DataGridComboBoxColumn  5      $ AccountType��6)   -   5        AccountTypeComboBox��   ItemsSource 5   [   ��  �� Source# [     SelectedItemBinding 6�   �� *AccountType+ �� Mode$ TwoWay=�  5       $ Balance��6%      
IsReadOnly .  66   �� *	Balance+$ OneWay=�6_   5"      �� 5#   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��2��6K   $��1��6X   .+�5$      �� -   5         ��Name$ AddAccountButton��5$      $��Add Account��6/   $��10q�6E   $��10q�6Q   6^   �� 5%      -   5        $ SaveAccountButton��5%      $
��Save��60   $��10q�6?   $��10q�6K   6X   �� 5&      -   5        $ TransferButton��5&      $��Transfer Amount��6-   $��10q�6G   $��10q�6S   6`   5'      5(      5)      