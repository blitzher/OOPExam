using System;

namespace Stregsystem
{
    class SeasonalProduct : Product
    {
        /* Never specified when the season start time should be used */
        public DateTime SeasonStartTime;

        public DateTime SeasonEndTime;

        private bool _active;
        public new bool Active
        {
            get
            {
                if (SeasonEndTime != DateTime.MaxValue)
                    return DateTime.Now < ExpirationDate;
                return _active;
            }
        }
    }
}
