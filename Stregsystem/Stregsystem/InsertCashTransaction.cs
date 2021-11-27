namespace Stregsystem
{
    class InsertCashTransaction : Transaction
    {
        
        
        public InsertCashTransaction(User actor) : base(actor)
        {

        }

        public override string ToString()
        {
            return string.Format("Deposit {0} for user {1} {2}DKK at {3}", Id, Actor, Amount, Date);
        }

        public override void Execute()
        {
            Logger.GetLogger("Transactions").Log(ToString());
            
        }
    }





}
