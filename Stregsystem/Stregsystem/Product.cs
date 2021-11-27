using System;

namespace Stregsystem
{
    class Product
    {
        public int Id;
        public string Name;
        public decimal Price;
        public bool Active { get; set; }
        public bool CanBeBoughtOnCredit;
        public DateTime ExpirationDate = DateTime.MaxValue;

        public Product()
        {

        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Id, Name, Price);
        }
    }

}
