using System;
using System.Collections.Generic;

namespace Stregsystem
{
    partial class Program
    {
        public List<Product> Products = new List<Product>();
        public List<User> Users = new List<User>();
        public List<Transaction> Transactions = new List<Transaction>();  

        static void Main(string[] args)
        {
            
        }

        public void BuyProduct(User user, Product product)
        {

        }

        public void AddCreditsToAccount(User user, double amount)
        {

        }

        public void ExecuteTransaction(Transaction transaction)
        {
            /* Each transaction itself takes care of logging */
            transaction.Execute();
            
        }

        public Product GetProductById(int id)
        {
            /* Could index by Id, but in case Id's aren't 
             * necessarily sequential, loop and find */
            foreach (Product product in Products)
            {
                if (product.Id == id)
                    return product;
            }
            throw new ArgumentOutOfRangeException($"No product of Id ({id}) found!");
        }

        public List<User> GetUsers(Func<User, bool> predicate)
        {
            List<User> predicateUsers = new List<User>();
            foreach (User user in Users)
            {
                if (predicate(user))
                    predicateUsers.Add(user);
            }
            return predicateUsers;
        }

        public User GetUserByUsername(string userName)
        {
            foreach (User user in Users)
            {
                if (user.UserName == userName)
                    return user;
            }

            throw new ArgumentOutOfRangeException($"No user with UserName ({userName}) found!");
        }

        public List<Transaction> GetTransactions(User user, int amount)
        {
            List<Transaction> userTransctions = new List<Transaction>();

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Actor.Id == user.Id)
                {
                    userTransctions.Add(transaction);
                }

                if (userTransctions.Count >= amount)
                    break;
            }
            return userTransctions;
        }

        public List<Product> ActiveProducts()
        {
            List<Product> activeProducts = new List<Product>();
            foreach (Product product in Products)
            {
                if (product.Active)
                    activeProducts.Add(product);
            }
            return activeProducts;
            
        }
    }

    


}
