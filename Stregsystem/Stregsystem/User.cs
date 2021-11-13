using System;

namespace Stregsystem
{

    class User : IComparable<User>
    {

        public int Id;
        public string[] FirstNames;
        public string LastName;
        public string UserName;
        public string Email;
        private decimal _balance;
        public decimal Balance { get { return (_balance); } }

        public User()
        {

        }

        public void AddBalance(decimal amount)
        {
            _balance += amount;
        }

        public void RemoveBalance(decimal amount)
        {
            AddBalance(-amount);
        }

        public override string ToString()
        {
            string FullFirstName = string.Join(" ", FirstNames);

            return string.Format("{0} {1} {2}", FullFirstName, LastName, Email);

        }

        public override bool Equals(object other)
        {
            
            if (other is User) {
                /* Since every Id should be unique, it is
                 * sufficient to compare against others Id */
                return ((User)other).Id == Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public int CompareTo(User other)
        {
            return Id - other.Id;
        }
    }
            

}
