namespace Stregsystem
{
    class Product
    {
        public int Id;
        public string Name;
        public decimal Price;
        public bool Active;
        public bool CanBeBoughtOnCredit;

        public Product()
        {

        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Id, Name, Price);
        }
    }

}
