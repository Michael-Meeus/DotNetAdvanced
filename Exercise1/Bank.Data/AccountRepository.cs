using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Bank.Data.DomainClasses;
using Bank.Data.DomainClasses.Enums;
using Bank.Data.Interfaces;

namespace Bank.Data
{
    public class AccountRepository : IAccountRepository
    {
        protected IConnectionFactory connectionFactory;

        public AccountRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Add(Account newAccount)
        {
            if (newAccount.CustomerId == 0 || !(newAccount.Id == 0))
            {
                throw new ArgumentException();
            }

            SqlCommand insertCommand = new SqlCommand();
            string insertStatement =
                 "INSERT Accounts (AccountNumber, Balance, AccountType, CustomerId) " +
                 "VALUES (@AccountNumber, @Balance, @AccountType, @CustomerId)";

            insertCommand.Parameters.AddWithValue(
                "@AccountNumber", newAccount.AccountNumber);
            insertCommand.Parameters.AddWithValue(
                "@Balance", newAccount.Balance);
            insertCommand.Parameters.AddWithValue(
                "@AccountType", newAccount.AccountType);
            insertCommand.Parameters.AddWithValue(
                "@CustomerId", newAccount.CustomerId);
            insertCommand.CommandText = insertStatement;
            newAccount.Id = 1;

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

        public void Update(Account existingAccount)
        {
            if (existingAccount.Id == 0 || existingAccount.CustomerId == 0)
            {
                throw new ArgumentException();
            }
            SqlCommand updateCommand = new SqlCommand();
            string updateStatement =

                "UPDATE Accounts SET AccountNumber = @AccountNumber, " +
                "Balance = @Balance, " +
                "AccountType = @AccountType, " +
                "CustomerId = @CustomerId " +
                "WHERE AccountNumber = @AccountNumber OR Balance = @Balance OR AccountType = @AccountType OR CustomerId = @CustomerId";
            updateCommand.Parameters.AddWithValue(
                "@AccountNumber", existingAccount.AccountNumber);
            updateCommand.Parameters.AddWithValue(
                "@Balance", existingAccount.Balance);
            updateCommand.Parameters.AddWithValue(
                "@AccountType", existingAccount.AccountType);
            updateCommand.Parameters.AddWithValue(
                "@CustomerId", existingAccount.CustomerId);
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

        public void TransferMoney(int fromAccountId, int toAccountId, decimal amount)
        {
            SqlCommand withdrawCommand = new SqlCommand();
            withdrawCommand.Parameters.AddWithValue(
            "@FromId", fromAccountId);
            withdrawCommand.Parameters.AddWithValue(
                "@FromAmount", -amount);
            string withdrawStatement =
                "UPDATE Accounts SET Balance += @FromAmount " +
                "WHERE Id = @FromId";

            withdrawCommand.CommandText = withdrawStatement;
            SqlCommand depositCommand = new SqlCommand();
            depositCommand.Parameters.AddWithValue("@ToId", toAccountId);
            depositCommand.Parameters.AddWithValue("@ToAmount", amount);
            string depositStatement =
                "UPDATE Accounts SET Balance = Balance + @ToAmount " +
                "WHERE Id = @ToId";

            depositCommand.CommandText = depositStatement;
            using (SqlConnection bankConnection = connectionFactory.CreateSqlConnection())
            {
                try
                {
                    bankConnection.Open();
                    SqlTransaction bankTransaction = bankConnection.BeginTransaction();
                    withdrawCommand.Connection = bankConnection;
                    withdrawCommand.Transaction = bankTransaction;
                    depositCommand.Connection = bankConnection;
                    depositCommand.Transaction = bankTransaction;

                    int numberOfRowsAffected = withdrawCommand.ExecuteNonQuery();
                    if (numberOfRowsAffected > 0)
                    {
                        numberOfRowsAffected = depositCommand.ExecuteNonQuery();
                        if (numberOfRowsAffected > 0)
                        {
                            bankTransaction.Commit();
                            return;
                        }
                        else
                        {
                            bankTransaction.Rollback();
                            return;
                        }
                    }
                    else
                    {
                        bankTransaction.Rollback();
                        return;
                    }
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

        public IList<Account> GetAllAccountsOfCustomer(int customerId)
        {
            SqlCommand selectCommand = new SqlCommand();
            string selectStatement =
                "SELECT Id, AccountNumber, Balance, AccountType, CustomerId " +
                "FROM Accounts ORDER BY AccountNumber";
            selectCommand.CommandText = selectStatement;

            using (SqlConnection bankConnection = connectionFactory.CreateSqlConnection())
            {
                bankConnection.Open();
                selectCommand.Connection = bankConnection;
                SqlDataReader reader =
                selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                List<Account> accountList = new List<Account>();
                while (reader.Read())
                {

                    Account account = new Account();
                    account.Id = Int32.Parse(reader["Id"].ToString());
                    if (reader["AccountNumber"].ToString() == string.Empty)
                    {
                        account.AccountNumber = null;
                    }
                    else
                    {
                        account.AccountNumber = reader["AccountNumber"].ToString();
                    }
                    account.Balance = Decimal.Parse(reader["Balance"].ToString());
                    account.AccountType = (AccountType)Int32.Parse(reader["AccountType"].ToString());
                    account.CustomerId = Int32.Parse(reader["CustomerId"].ToString());
                    if (customerId == account.CustomerId)
                    {
                        accountList.Add(account);
                    }

                }
                reader.Close();
                return accountList;
            }
        }
    }
}
