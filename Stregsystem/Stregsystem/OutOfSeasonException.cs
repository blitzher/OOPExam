using System;

namespace Stregsystem
{
    class OutOfSeasonException : Exception
    {
        public OutOfSeasonException(User Actor, Product PurchasedProduct) :
            base(string.Format("User {0} cannot purchase product {1} because it is out of season", Actor, PurchasedProduct))
        {

        }
    }




}
