using System;
using System.Collections.Generic;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;
using System.Windows;
using Bank.Data;

namespace Bank.UI
{
    public partial class AccountsWindow : Window
    {
        private IAccountRepository currentAccountRep;
        private Customer currentCustomer;
        private IWindowDialogService service;


        public AccountsWindow(Customer customer,
            IAccountRepository accountRepository,
            IWindowDialogService windowDialogService)
        {
            InitializeComponent();
            this.Title = customer.FirstName.ToString() + " " + customer.Name.ToString();
            currentAccountRep = accountRepository;
            currentCustomer = customer;
            service = windowDialogService;
            AccountsDataGrid.DataContext = currentAccountRep.GetAllAccountsOfCustomer(customer.CustomerId);

        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {

            Account newAccount = new Account();
            AccountsDataGrid.CanUserAddRows = true;
            newAccount.CustomerId = currentCustomer.CustomerId;
            currentAccountRep.Add(newAccount);
        }

        private void SaveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            List<Account> allAccountsList = (List<Account>)currentAccountRep.GetAllAccountsOfCustomer(currentCustomer.CustomerId);
            AccountsDataGrid.CanUserAddRows = true;
            int acc = allAccountsList.Count;
            int items = AccountsDataGrid.Items.Count;
            object test = AccountsDataGrid.SelectedItems;
            Account selectedAccount = (Account)AccountsDataGrid.SelectedValue;

            if (!(AccountsDataGrid.SelectedValue == null))  
            {
                if (selectedAccount.Id == 0)
                {
                    Account newAccount = (Account)AccountsDataGrid.SelectedValue;
                    currentAccountRep.Add(newAccount);
                }
                else
                {
                    Account account = (Account)AccountsDataGrid.SelectedValue;
                    currentAccountRep.Update(account);
                }
            }
            AccountsDataGrid.DataContext = currentAccountRep;
            AccountsDataGrid.CanUserAddRows = false;
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(AccountsDataGrid.SelectedValue == null))
            {
                service.ShowTransferDialog((Account)AccountsDataGrid.SelectedValue, currentAccountRep.GetAllAccountsOfCustomer(currentCustomer.CustomerId));
                
            }
        }
    }
}
