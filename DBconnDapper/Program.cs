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
            // ---------------SELECT---------------
            var sqlSelect = "select * from customers2";

            var customers = connection.Query<Customer>(sqlSelect);

            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }

            // ---------------INSERT---------------
            /*            var sqlInsert = @"INSERT INTO customers2 (first_name, last_name, email, street, city, state, age) VALUES(@first_name, @last_name, @email, @street, @city, @state, @age)";

                        // single insert
                        *//*            connection.Execute(sqlInsert, new
                                    {
                                        first_name = "Algis",
                                        last_name = "Glamoris",
                                        email = "alg.glam@yahoooo.com",
                                        street = "5 Ciauduliu",
                                        city = "Didelis",
                                        state = "Alytus",
                                        age = 100
                                    });*//*
                        var fakeCustomers = GeneratePersonsYield(10);

                        //multiple inserts from IEnumerable (List)

                        connection.Execute(sqlInsert, fakeCustomers); // automatically identifies, that given type is a list of objects*/

            // ---------------DELETE---------------
            /*            var sqlDelete = "DELETE FROM customers2 where customer_id = @customer_id";

                        var rowsAffected = connection.Execute(sqlDelete, new { Customer_Id = 20 });

                        Console.WriteLine(rowsAffected);*/
        }

        public static IEnumerable<Customer> GeneratePersonsYield(int customerCount)
        {
            for (var i = 0; i < customerCount; i++)
            {
                yield return new Customer
                {
                    First_Name = Faker.Name.First(),
                    Last_Name = Faker.Name.Last(),
                    Email = Faker.Internet.Email(),
                    Street = Faker.Address.StreetAddress(),
                    City = Faker.Address.City(),
                    State = Faker.Address.UsState(),
                    Age = Faker.RandomNumber.Next(1, 100)
                };
            }
        }
    }
}