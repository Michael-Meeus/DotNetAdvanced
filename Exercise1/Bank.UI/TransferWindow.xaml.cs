using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;

namespace Bank.UI
{
    public partial class TransferWindow : Window
    {
        private IAccountRepository currentAccountRep;
        private int fromAccountId;
        private decimal fromAccountBalance;
        private Account currentFromAccount;

        public TransferWindow(Account fromAccount,
            IList<Account> allAccountsOfCustomer,
            IAccountRepository accountRepository)
        {
            InitializeComponent();
            FromAccountTextBlock.Text = fromAccount.AccountNumber;
            ToAccountComboBox.ItemsSource = allAccountsOfCustomer;
            currentAccountRep = accountRepository;
            currentFromAccount = fromAccount;
            fromAccountId = fromAccount.Id;
            fromAccountBalance = fromAccount.Balance;
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            if (Decimal.Parse(AmountTextBox.Text) > currentFromAccount.Balance || Decimal.Parse(AmountTextBox.Text) < 0)
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = currentFromAccount.Balance.ToString();
            }
            else
            {
                int toAccountId = (Int32)ToAccountComboBox.SelectedValue;

                
                currentAccountRep.TransferMoney(fromAccountId, toAccountId, Decimal.Parse(AmountTextBox.Text));
                currentFromAccount.Balance = currentFromAccount.Balance - Decimal.Parse(AmountTextBox.Text);
                Account toAccount = (Account)ToAccountComboBox.SelectedItem;
                toAccount.Balance += Decimal.Parse(AmountTextBox.Text);
                //fromAccountBalance += -Decimal.Parse(AmountTextBox.Text);
                //Account currentToAccount = 
                //currentAccountRep.Update(currentFromAccount);
                //currentAccountRep.Update(currentToAccount);
                //toAccount.Balance += Decimal.Parse(AmountTextBox.Text);
            }
        }
    }
}
