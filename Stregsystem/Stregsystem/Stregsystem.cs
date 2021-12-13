using System;
using System.Collections.Generic;

namespace Stregsystem
{
    class UserBalanceNotification : EventArgs
    {
        public decimal Balance;
        public string Username;
    }

    partial class Stregsystem
    {
        public List<Product> Products = new List<Product>();
        public List<User> Users = new List<User>();
        public List<Transaction> Transactions = new List<Transaction>();
        public Logger SystemLogger;

        public Stregsystem()
        {
            DatabaseLoader loader = new DatabaseLoader();
            SystemLogger = Logger.GetLogger("Stregsystem");
            Users = loader.LoadUsers();
            Products = loader.LoadProducts();
            SystemLogger.Log("Initialised System and loaded users and products");
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(product, user);
            Transactions.Add(transaction);

            return transaction;
        }

        public void AddCreditsToAccount(User user, decimal amount)
        {
            user.AddBalance(amount);
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            /* Each transaction itself takes care of logging */
            transaction.Execute();

            if (transaction.Actor.Balance <= 100)
            {
                EventHandler<UserBalanceNotification> handler = UserBalanceWarning;
                handler?.Invoke(this, new UserBalanceNotification()
                {
                    Balance = transaction.Actor.Balance,
                    Username = transaction.Actor.UserName
                });

                Logger.GetLogger("Transactions")
                    .Warn($"Low balance on account: " +
                    $"{transaction.Actor.UserName}");
            }

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

        public event EventHandler<UserBalanceNotification> UserBalanceWarning;
    }




}
