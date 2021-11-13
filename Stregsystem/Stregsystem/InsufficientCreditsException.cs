using System;

namespace Stregsystem
{
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(User Actor, Product PurchasedProduct) :
            base (string.Format("User {0} has inssuficient credits to purchase {1}", Actor, PurchasedProduct))
            
        {
            
        }
    }
}
