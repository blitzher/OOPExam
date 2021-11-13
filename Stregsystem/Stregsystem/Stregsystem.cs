using System;
using System.Collections.Generic;

namespace Stregsystem
{
    class Stregsystem
    {
        public Dictionary<int, User> Users;
        public Dictionary<int, Product> Products;
        public Dictionary<int, Transaction> Transactions;

        public Stregsystem()
        {
            Users = new Dictionary<int, User>();
            Products = new Dictionary<int, Product>();
            Transactions = new Dictionary<int, Transaction>();
        }


        public void BuyProduct(User user, Product product)
        {
            Transaction transaction = new BuyTransaction(product, user);
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            try
            {
                transaction.Execute();
            } catch (Exception e)
            {
                Logger.GetLogger("Transactions").Log(e.Message);
            }
        }

        public Product GetProductById(int Id)
        {
            return Products[Id];
        }


        public void AddCreditsToAccount(User user, decimal amount)
        {
            user.AddBalance(amount);
        }


    }

}
