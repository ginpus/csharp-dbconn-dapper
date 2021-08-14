using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;

namespace DBconnDapper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder();

            connectionStringBuilder.Server = "localhost";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.UserID = "test";
            connectionStringBuilder.Password = "test";
            connectionStringBuilder.Database = "noteapp";

            var connectionString = connectionStringBuilder.GetConnectionString(true);

            Console.WriteLine(connectionString);

            using var connection = new MySqlConnection(connectionString); // object represents connectivity

            //connection.Open(); //not neccessary with dapper

            var sql = "select * from customers2";

            var customers = connection.Query<Customer>(sql);

            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }
    }
}