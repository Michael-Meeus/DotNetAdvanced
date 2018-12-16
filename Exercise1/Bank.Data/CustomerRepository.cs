using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;

namespace Bank.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        protected IConnectionFactory connectionFactory;

        public CustomerRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IList<Customer> GetAll()
        {
            SqlCommand selectCommand = new SqlCommand();
            string selectStatement =
                "SELECT CustomerId, Name, FirstName, Address, CellPhone, ZipCode " +
                "FROM Customers ORDER BY Name";
            selectCommand.CommandText = selectStatement;

            using (SqlConnection bankConnection = connectionFactory.CreateSqlConnection())
            {
                bankConnection.Open();
                selectCommand.Connection = bankConnection;
                SqlDataReader reader =
                selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                List<Customer> customerList = new List<Customer>();
                while (reader.Read())
                {
                    Customer customer = new Customer();
                    customer.CustomerId = Int32.Parse(reader["CustomerId"].ToString());
                    if (reader["Name"].ToString() == string.Empty)
                    {
                        customer.Name = null;
                    }
                    else
                    {
                        customer.Name = reader["Name"].ToString();
                    }
                    if (reader["FirstName"].ToString() == string.Empty)
                    {
                        customer.FirstName = null;
                    }
                    else
                    {
                        customer.FirstName = reader["FirstName"].ToString();
                    }
                    if (reader["Address"].ToString() == string.Empty)
                    {
                        customer.Address = null;
                    } else
                    {
                        customer.Address = reader["Address"].ToString();
                    }
                    if (reader["CellPhone"].ToString() == string.Empty)
                    {
                        customer.CellPhone = null;
                    }
                    else
                    {
                        customer.CellPhone = reader["CellPhone"].ToString();
                    }
                    customer.ZipCode = Int32.Parse(reader["ZipCode"].ToString());
                    customerList.Add(customer);
                }
                reader.Close();
                return customerList;
            }
        }

        public void Update(Customer existingCustomer)
        {
            if (existingCustomer.CustomerId == 0 || existingCustomer.ZipCode == 0)
            {
                throw new ArgumentException();
            }
            SqlCommand updateCommand = new SqlCommand();
            string updateStatement = 
                "UPDATE Customers SET Name = @Name, " +
                "FirstName = @FirstName, " +
                "Address = @Address, " +
                "CellPhone = @CellPhone, " +
                "ZipCode = @ZipCode " +
                "Where Name = @Name OR FirstName = @FirstName OR Address = @Address OR CellPhone = @CellPhone OR ZipCode = @ZipCode";
            updateCommand.Parameters.AddWithValue(
                "@Name", existingCustomer.Name);
            updateCommand.Parameters.AddWithValue(
                "@FirstName", existingCustomer.FirstName);
            updateCommand.Parameters.AddWithValue(
                "@Address", existingCustomer.Address);
            updateCommand.Parameters.AddWithValue(
                "@CellPhone", existingCustomer.CellPhone);
            updateCommand.Parameters.AddWithValue(
                "@ZipCode", existingCustomer.ZipCode);
            updateCommand.CommandText = updateStatement;

            using (SqlConnection bankConnection = connectionFactory.CreateSqlConnection())
            {
                try
                {
                    bankConnection.Open();
                    updateCommand.Connection = bankConnection;
                    int count = updateCommand.ExecuteNonQuery();
                    MessageBox.Show(count + " rows updated");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL Server error number " + ex.Number +
                        ": " + ex.Message, ex.GetType().ToString());
                }
                finally
                {
                    bankConnection.Close();
                }
            }
        }

        public void Add(Customer newCustomer)
        {
            if (newCustomer.CustomerId > 0 || newCustomer.ZipCode == 0)
            {
                throw new ArgumentException();
            }
            
            SqlCommand insertCommand = new SqlCommand();
            string insertStatement =
                 "INSERT Customers (Name, FirstName, Address, CellPhone, ZipCode) " +
                 "VALUES (@Name, @FirstName, @Address, @CellPhone, @ZipCode)";
           
            insertCommand.Parameters.AddWithValue(
                "@Name", newCustomer.Name);
            insertCommand.Parameters.AddWithValue(
                "@FirstName", newCustomer.FirstName);
            insertCommand.Parameters.AddWithValue(
                "@Address", newCustomer.Address);
            insertCommand.Parameters.AddWithValue(
                "@CellPhone", newCustomer.CellPhone);
            insertCommand.Parameters.AddWithValue(
                "@ZipCode", newCustomer.ZipCode);
            insertCommand.CommandText = insertStatement;
            newCustomer.CustomerId = 1;

            using (SqlConnection bankConnection = connectionFactory.CreateSqlConnection())
            {
                try
                {
                    bankConnection.Open();
                    insertCommand.Connection = bankConnection;
                    int count = insertCommand.ExecuteNonQuery();
                    MessageBox.Show(count + " rows inserted");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL Server error number " + ex.Number +
                        ": " + ex.Message, ex.GetType().ToString());
                }
                finally
                {
                    bankConnection.Close();
                }
            }
        }
    }
}