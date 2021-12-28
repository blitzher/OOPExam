using System;

namespace Stregsystem
{
    class OutOfSeasonException : Exception
    {
        public User Actor;
        public Product PurchasedProduct;

        public OutOfSeasonException(User actor, Product purchasedProduct) :
            
            base(string.Format("User {0} cannot purchase product {1} because it is out of season", actor, purchasedProduct))
        {
            Actor = actor;
            PurchasedProduct = purchasedProduct;
        }
    }




}
