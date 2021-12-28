using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class LowBalanceEventArgs : EventArgs
    {
        public decimal Balance { get; private set; }
        public User User { get; private set; }

        public LowBalanceEventArgs(User user, decimal balance)
        {
            User = user;
            Balance = balance;
        }
    }
}
