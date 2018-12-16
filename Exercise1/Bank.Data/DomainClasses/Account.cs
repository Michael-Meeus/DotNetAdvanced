using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Bank.Data.DomainClasses.Enums;

namespace Bank.Data.DomainClasses
{
    public class Account : INotifyPropertyChanged
    {

        public int Id { get; set; }

        public string AccountNumber { get; set; }

        private decimal pBalance;
        public decimal Balance
        {
            get
            {
                return pBalance;
            }
            set
            {
                if (value != pBalance)
                {
                    pBalance = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public AccountType AccountType { get; set; }

        public int CustomerId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
