namespace Stregsystem
{
    class BuyTransaction : Transaction
    {
        Product PurchasedProduct;
        public BuyTransaction(Product product, User actor) : base(actor)
        {
            PurchasedProduct = product;
        }

        public override void Execute()
        {
            if (Actor.Balance - Amount < 0 && !PurchasedProduct.CanBeBoughtOnCredit)
            {
                throw new InsufficientCreditsException(Actor, PurchasedProduct);
            }

            if (PurchasedProduct is SeasonalProduct && !((SeasonalProduct)PurchasedProduct).Active)
            {
                throw new OutOfSeasonException(Actor, PurchasedProduct);
            }

            Logger.GetLogger("Transactions").Log(ToString());
            Actor.RemoveBalance(PurchasedProduct.Price);
        }

        public override string ToString()
        {
            return string.Format("Purchase {0} for user {1} {2}DKK at {3} of item {4}",
                                    Id, Actor, Amount, Date, PurchasedProduct);
        }

    }



}
