using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Stregsystem
{
    class DatabaseLoader
    {
        public DatabaseLoader()
        {
        }


        public List<Product> LoadProducts()
        {
            FileInfo products_csv = new FileInfo("products.csv");
            StreamReader reader = new StreamReader(products_csv.OpenRead());
            List<Product> products = new List<Product>();

            // We essentially skip the first line, since it's the header
            string line = reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {
                string[] arguments = line.Split(';');
                for (int i = 0; i<arguments.Length; i++)
                {
                    arguments[i] = arguments[i].Trim('"');
                }

                DateTime deactiveDate = Convert.ToDateTime(
                    (arguments[4] == "") ? null : arguments[4]);

                bool active = (deactiveDate == DateTime.MinValue) ?
                    (arguments[3] == "1") : DateTime.Now < deactiveDate;

                products.Add(new Product()
                {
                    Id = int.Parse(arguments[0]),
                    Name = Regex.Replace(arguments[1].Trim('"'), "<.*?>", String.Empty),
                    Price = decimal.Parse(arguments[2]),
                    Active = active,
                });

                
            }

            return products;
        }

        public List<User> LoadUsers()
        {
            List<User> users = new List<User>();

            FileInfo users_csv = new FileInfo("users.csv");
            StreamReader reader = new StreamReader(users_csv.OpenRead());
            List<Product> products = new List<Product>();

            // We essentially skip the first line, since it's the header
            string line = reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {
                string[] arguments = line.Split(',');
                for (int i = 0; i < arguments.Length; i++)
                {
                    arguments[i] = arguments[i].Trim('"');
                }

                User user = 
                new User(arguments[3], arguments[5])
                {
                    Id = int.Parse(arguments[0]),
                    FirstNames = arguments[1].Split(" "),
                    LastName = arguments[2],
                };

                // Add initial balance & add to list
                user.AddBalance(decimal.Parse(arguments[4]));
                users.Add(user);
                
            }
            return users;

        }
    }
}
