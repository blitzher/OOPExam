using System;

namespace Stregsystem
{
    abstract class Transaction
    {
        /* Use static count, such that all new transactions
         * can get their unique transaction Id */
        private static int TotalTransactionCount = 0;

        private int _id;
        public int Id { get { return _id; } }
        public User Actor;
        public DateTime Date;
        public decimal Amount;

        public Transaction(User actor)
        {
            _id = TotalTransactionCount++;
            Actor = actor;
            Date = DateTime.Now;
            Amount = 0;
        }

        public Transaction(User actor, decimal amount) : this(actor)
        {
            Amount = amount;
        }


        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Id, Actor, Amount, Date);
        }

        public abstract void Execute();

    }
}
