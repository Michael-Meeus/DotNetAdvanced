using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;

namespace Bank.Data
{
    public class CityRepository : ICityRepository
    {
        protected IConnectionFactory connectionFactory;
        public CityRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IList<City> GetAll()
        {
            SqlCommand selectCommand = new SqlCommand();
            string selectStatement =
                "SELECT ZipCode, Name " +
                "FROM Cities ORDER BY Name";
            selectCommand.CommandText = selectStatement;

            using (SqlConnection bankConnection = connectionFactory.CreateSqlConnection())
            {
                bankConnection.Open();
                selectCommand.Connection = bankConnection;
                SqlDataReader reader =
                selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                List<City> cityList = new List<City>();
                while (reader.Read())
                {
                    City city = new City();
                    
                    if (reader["Name"].ToString() == string.Empty)
                    {
                        city.Name = null;
                    }
                    else
                    {
                        city.Name = reader["Name"].ToString();
                    }
                    
                    city.ZipCode = Int32.Parse(reader["ZipCode"].ToString());
                    cityList.Add(city);
                }
                reader.Close();
                return cityList;
            }
        }
    }
}