using System;

namespace Stregsystem
{
    class SeasonalProduct : Product
    {
        public DateTime SeasonStartTime;
        public DateTime SeasonEndTime;

        public new bool Active
        {
            get
            {
                TimeSpan dt = SeasonEndTime - DateTime.Now;
                return dt.TotalSeconds > 0;
            }
        }
    }

  

}
