using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DBconn
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var connectionString = "Server=localhost:3306;Uid=test;" + "Pwd=test;Database=noteapp";
            //same as above
            var connectionStringBuilder = new MySqlConnectionStringBuilder();

            connectionStringBuilder.Server = "localhost";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.UserID = "test";
            connectionStringBuilder.Password = "test";
            connectionStringBuilder.Database = "noteapp";

            var connectionString = connectionStringBuilder.GetConnectionString(true);

            Console.WriteLine(connectionString);

            using var connection = new MySqlConnection(connectionString); // object represents connectivity

            connection.Open();

            var fakeCustomers = GeneratePersons(10);

            foreach (var fakeCustomer in fakeCustomers)
            {
                var query = @$"INSERT INTO customers2 (first_name, last_name, email, street, city, state, age)
            VALUES(@first_name, @last_name, @email, @street, @city, @state, @age)";

                using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@first_name", fakeCustomer.FirstName);
                command.Parameters.AddWithValue("@last_name", fakeCustomer.LastName);
                command.Parameters.AddWithValue("@email", fakeCustomer.Email);
                command.Parameters.AddWithValue("@street", fakeCustomer.Street);
                command.Parameters.AddWithValue("@city", fakeCustomer.City);
                command.Parameters.AddWithValue("@state", fakeCustomer.State);
                command.Parameters.AddWithValue("@age", fakeCustomer.Age);

                command.ExecuteNonQuery();

                command.Parameters.Clear(); // cleans out all parameters for the next itteration of insert
            }
            connection.Close();

            //---------------------------------------------------------
            /*            // creation of table
            var query = @"CREATE TABLE Customers2 (
                customer_id mediumint(8) unsigned NOT NULL auto_increment,
                first_name varchar(255) default NULL,
                last_name varchar(255) default NULL,
                email varchar(255) default NULL,
                street varchar(255) default NULL,
                city varchar(255),
                state varchar(50) default NULL,
                age mediumint default NULL,
                PRIMARY KEY (customer_id))
                AUTO_INCREMENT = 1;";
            using var command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();*/
            //------------------------------------------------------------
            /*            //selecting all items
                        var querySelect = "select * from customers";

                        using var command = new MySqlCommand(querySelect, connection);

                        var reader = command.ExecuteReader();

                        var customers = new List<Customer>();

                        while (reader.Read()) // marker, returns tru or false (for all entries in table)
                        {
                            customers.Add(new Customer
                            {
                                CustomerId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                Street = reader.GetString(4),
                                City = reader.GetString(5),
                                State = reader.GetString(6),
                                Age = reader.GetInt32(7)
                            }
                            );
                        }

                        foreach (var customer in customers)
                        {
                            Console.WriteLine(customer);
                            Console.WriteLine();
                        }*/
            //--------------------------------------------------
        }

        public static IEnumerable<Customer> GeneratePersons(int customerCount)
        {
            var listOfCustomers = new List<Customer> { };
            for (var i = 0; i < customerCount; i++)
            {
                var customer = new Customer
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Email = Faker.Internet.Email(),
                    Street = Faker.Address.StreetAddress(),
                    City = Faker.Address.City(),
                    State = Faker.Address.UsState(),
                    Age = Faker.RandomNumber.Next(1, 100)
                };
                //Console.WriteLine(customer);
                listOfCustomers.Add(customer);
            }
            return listOfCustomers;
        }

        public static IEnumerable<Customer> GeneratePersonsYield(int customerCount)
        {
            for (var i = 0; i < customerCount; i++)
            {
                yield return new Customer
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
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