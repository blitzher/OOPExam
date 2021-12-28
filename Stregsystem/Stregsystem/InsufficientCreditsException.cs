using System;

namespace Stregsystem
{
    class InsufficientCreditsException : Exception
    {
        public User Actor;
        public Product PurchasedProduct;

        public InsufficientCreditsException(User actor, Product purchasedProduct) :
            base (string.Format("User {0} has inssuficient credits to purchase {1}", actor, purchasedProduct))
            
        {
            Actor = actor;
            PurchasedProduct = purchasedProduct;
        }
    }
}
