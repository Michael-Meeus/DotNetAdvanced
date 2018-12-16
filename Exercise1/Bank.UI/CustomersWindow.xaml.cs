using System;
using System.Windows;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;
using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Collections.Generic;
using Bank.Data;

namespace Bank.UI
{
    public partial class CustomersWindow : Window
    {
        private ICustomerRepository currentRepository;
        private IWindowDialogService service;

        public CustomersWindow(ICustomerRepository customerRepository, 
            ICityRepository cityRepository, 
            IWindowDialogService windowDialogService)
        {
            InitializeComponent();
            currentRepository = customerRepository;
            CustomersDataGrid.DataContext = currentRepository.GetAll();
            CityComboBoxColumn.ItemsSource = cityRepository.GetAll();
            service = windowDialogService;

        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Customer newCustomer = new Customer();
            CustomersDataGrid.CanUserAddRows = true;
            //CustomersDataGrid.Items.Add(newCustomer);
            newCustomer.ZipCode = 9999;
            currentRepository.Add(newCustomer);
            CustomersDataGrid.ItemsSource = currentRepository.GetAll();
            //CustomersDataGrid.CanUserAddRows = false;
        }

        private void SaveCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            List<Customer> allCustomersList = (List<Customer>)currentRepository.GetAll();
            CustomersDataGrid.CanUserAddRows = true;
            int cust = allCustomersList.Count;
            int items = CustomersDataGrid.Items.Count;
            Customer selectedCustomer = (Customer)CustomersDataGrid.SelectedValue;
            if (!(CustomersDataGrid.SelectedValue == null))
            {
                if (selectedCustomer.CustomerId == 0)
                {
                Customer newCustomer = (Customer)CustomersDataGrid.SelectedValue;
                currentRepository.Add(newCustomer);
                }
            }
            else
            {
                    Customer customer = (Customer)CustomersDataGrid.SelectedValue;
                    currentRepository.Update(customer);
            }

            //if (CustomersDataGrid.SelectedValue.GetType().FullName == "MS.Internal.NamedObject" || 
            //    !(allCustomersList.Contains((Customer)CustomersDataGrid.SelectedValue)))
            //{
            //    //if (!(CustomersDataGrid.SelectedValue == null))
            //    //{
            //        Customer newCustomer = (Customer)CustomersDataGrid.SelectedValue;
            //    currentRepository.Add(newCustomer);


            //    //}
            //} else
            //{
            //    Customer customer = (Customer)CustomersDataGrid.SelectedValue;
            //    currentRepository.Update(customer);
            //}

            CustomersDataGrid.DataContext = currentRepository;
            CustomersDataGrid.CanUserAddRows = false;
        }

        private void ShowAccountsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(CustomersDataGrid.SelectedValue == null) && (!(CustomersDataGrid.SelectedValue.GetType().ToString() == "MS.Internal.NamedObject")))
            {

                Customer customer = (Customer)CustomersDataGrid.SelectedValue;
                service.ShowAccountDialogForCustomer(customer);
            }
        }
    }
}
